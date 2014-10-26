using UnityEngine;
using System.Collections;

public class scr_PlayerControls : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltMovSpeed = 5f;
	private float fltRotSpeed = 100f;
	private float fltGravity = -9.81f;
	private float fltRayLength = 6f;
	private float fltDeadTimer = 0f;
	private float fltRespawnDelay = 3f;
	private float fltOpacity = 0f;

	private bool blnFlashlightOn = false;
	private bool blnControlActive = true;
	private bool blnHasFlashlight = false;
	private bool blnDead = false;
	private bool blnFaderShow = false;

	private CharacterController cControler;
	private GameObject objFlashlight;
	private GameObject objFader;

	private scr_FlashlightControl scrFlashlightControl;
	private scr_GameControl scrGameControl;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		cControler = GetComponent<CharacterController>();
		objFlashlight = GameObject.Find("obj_Flashlight");
		objFader = GameObject.FindGameObjectWithTag (Tags.fader);
		scrFlashlightControl = objFlashlight.GetComponent<scr_FlashlightControl>();
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		if (blnControlActive)
		{
			RotatePlayer();
			MovePlayer();
		}
		if (blnDead)
		{
			RespawnDelay();
			FaderShowHide();
		}
	}
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update()
	{
		if (blnControlActive && blnHasFlashlight)
			ActivateFlashlight();
		CheckFlashlightHit();
	}
	//*************************************************************************
	//	Rotate Player Method - rotates the player
	//*************************************************************************
	void RotatePlayer()
	{
		float fltLeftRight = 0f;
		float fltDeltaTime = Time.deltaTime;

		fltLeftRight = Input.GetAxis("Horizontal") * fltRotSpeed * fltDeltaTime;

		transform.Rotate(0f, fltLeftRight, 0f);
	}
	//*************************************************************************
	//	Move Player Method - moves the player based on rotation
	//*************************************************************************
	void MovePlayer()
	{
		float fltForward = 0f;
		Vector3 vecMovement;

		float fltDeltaTime = Time.deltaTime;

		fltForward = Input.GetAxis("Vertical") * fltMovSpeed * fltDeltaTime;

		vecMovement = new Vector3(0f, fltGravity * fltDeltaTime, fltForward);
		vecMovement = transform.rotation * vecMovement;

		cControler.Move(vecMovement);
	}
	//*************************************************************************
	//	Check Flashlight Hit Method - performs a raycast and checks if hits enemy
	//*************************************************************************
	void CheckFlashlightHit()
	{
		RaycastHit hit;
		GameObject goEnemy;
		Vector3 position = objFlashlight.transform.position;

		if (scrFlashlightControl.blnFlashlightActive)
		{
			if (Physics.Raycast(position, transform.forward, out hit, fltRayLength))
			{
				if (hit.transform.tag == Tags.enemy && !hit.collider.isTrigger)
				{
					goEnemy = hit.transform.gameObject;
					goEnemy.GetComponent<scr_EnemyHealth>().TakeDamage(100f);
				}
			}
		}
	}
	//*************************************************************************
	//	Activate Flashlight Method - plays animations and turns flashlight on/off
	//*************************************************************************
	void ActivateFlashlight()
	{
		Light[] lightComponents = objFlashlight.GetComponentsInChildren<Light>();

		if (Input.GetKeyDown("f"))
		{
			if (blnFlashlightOn)
			{
				objFlashlight.GetComponent<Animation>().Play("anim_FlashlightLower");
				blnFlashlightOn = false;
			}
			else
			{
				objFlashlight.GetComponent<Animation>().Play("anim_FlashlightRaise");
				blnFlashlightOn = true;
			}
		}
		if (scrFlashlightControl.blnFlashlightActive)
		{
			foreach (Light lightComp in lightComponents)
				lightComp.enabled = true;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = true;
		}
		else
		{
			foreach (Light lightComp in lightComponents)
				lightComp.enabled = false;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
		}
	}
	//*************************************************************************
	//	On Trigger Enter Method
	//*************************************************************************
	void OnTriggerEnter(Collider other)
	{
		GameObject obj = other.transform.gameObject;

		if (obj.tag == Tags.item)
		{
			blnHasFlashlight = true;
			scrGameControl.RemoveObject(obj);
			scrGameControl.OpenGates("obj_StartGate");
		}
		else if (obj.tag == Tags.collectible)
		{
			scrGameControl.FoundCollectible();
			scrGameControl.RemoveObject(obj);
		}
	}
	//*************************************************************************
	//	Kill Player Method
	//*************************************************************************
	public void KillPlayer()
	{
		scrGameControl.OpenGates("obj_StartGate");
		blnControlActive = false;
		blnDead = true;
		blnFaderShow = true;
	}
	//*************************************************************************
	// Respawn Delay Method 
	//*************************************************************************
	void RespawnDelay()
	{
		fltDeadTimer += Time.deltaTime;
		if (fltDeadTimer >= fltRespawnDelay)
		{
			blnControlActive = true;
			blnDead = false;
			fltDeadTimer = 0f;
		}
		else if (fltDeadTimer >= (fltRespawnDelay / 2))
		{
			transform.position = scrGameControl.vecStartingPosition;
			transform.rotation = scrGameControl.qtnStartingRotation;
			blnFaderShow = false;
		}
	}
	//*************************************************************************
	//	Fader Show Hide Method
	//*************************************************************************
	void FaderShowHide()
	{
		MeshRenderer meshFader = objFader.GetComponent<MeshRenderer>();
		float fltAlpha = meshFader.material.color.a;

		if (blnFaderShow)
			fltOpacity = 1f;
		else
			fltOpacity = 0f;
		fltAlpha = Mathf.Lerp(fltAlpha, fltOpacity, 1f);

		meshFader.material.color = new Color(0f, 0f, 0f, fltAlpha);

	}
}
