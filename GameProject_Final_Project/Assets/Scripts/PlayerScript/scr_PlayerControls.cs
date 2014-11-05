using UnityEngine;
using System.Collections;

public class scr_PlayerControls : MonoBehaviour
{
	//***********************************
	//	Variables
	//***********************************
	private float fltMovSpeed = 5f;
	private float fltRotSpeed = 100f;
	private float fltGravity = -9.81f;
	private float fltRayLength = 8f;
	private float fltDeadTimer = 0f;
	private float fltRespawnDelay = 3f;
	private float fltFlashlightDamage = 75f;

	private bool blnFlashlightOn = false;
	private bool blnHasFlashlight = false;
	private bool blnDead = false;

	private CharacterController cControler;
	private GameObject objFlashlight;

	private scr_FlashlightControl scrFlashlightControl = null;
	private scr_GameControl scrGameControl = null;
	private scr_ScreenFadeInOut scrFader = null;

	private AudioSource audioFlashlight;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		cControler = GetComponent<CharacterController>();
		objFlashlight = GameObject.Find("obj_Flashlight");
		scrFlashlightControl = objFlashlight.GetComponent<scr_FlashlightControl>();
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();
		scrFader = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<scr_ScreenFadeInOut>();
		audioFlashlight = GetComponent<AudioSource>();

	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		if (scrGameControl.GetPlayerIsActive())
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
		if (scrGameControl.GetPlayerIsActive() && blnHasFlashlight)
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

		if (scrFlashlightControl.blnFlashlightActive && scrGameControl.CheckFlashlightHasPower())
		{
			scrGameControl.DrainBattery(Time.deltaTime * 1.5f);

			if (Physics.Raycast(position, transform.forward, out hit, fltRayLength))
			{
				if (hit.transform.tag == Tags.enemy)
				{
					goEnemy = hit.transform.gameObject;
					goEnemy.GetComponent<scr_EnemyHealth>().TakeDamage(fltFlashlightDamage * Time.deltaTime);
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

		if (Input.GetKeyDown("f") && !scrFlashlightControl.blnAnimationPlaying)
		{
			if (blnFlashlightOn)
				objFlashlight.GetComponent<Animation>().Play("anim_FlashlightLower");
			else
				objFlashlight.GetComponent<Animation>().Play("anim_FlashlightRaise");
		}

		if (scrFlashlightControl.blnFlashlightActive && !blnFlashlightOn)
		{
			audioFlashlight.Play();
			blnFlashlightOn = true;
			if (scrGameControl.CheckFlashlightHasPower())
			{
				foreach (Light lightComp in lightComponents)
					lightComp.enabled = true;
				objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = true;
			}
		}
		else if (!scrFlashlightControl.blnFlashlightActive && blnFlashlightOn)
		{
			audioFlashlight.Play();
			blnFlashlightOn = false;
			foreach (Light lightComp in lightComponents)
				lightComp.enabled = false;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
		}
		else if (!scrGameControl.CheckFlashlightHasPower())
		{
			foreach (Light lightComp in lightComponents)
				lightComp.enabled = false;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
		}
	}
	//*************************************************************************
	//	Kill Player Method
	//*************************************************************************
	public void KillPlayer()
	{
		scrGameControl.DeactivatePlayer();
		scrGameControl.LostLife();
		blnDead = true;
	}
	//*************************************************************************
	// Respawn Delay Method 
	//*************************************************************************
	void RespawnDelay()
	{
		fltDeadTimer += Time.deltaTime;
		if (fltDeadTimer >= fltRespawnDelay)
		{
			scrGameControl.ActivatePlayer();
			blnDead = false;
			fltDeadTimer = 0f;
			scrGameControl.OpenGates("obj_StartGate");
		}
		else if (fltDeadTimer >= (fltRespawnDelay / 2))
		{
			transform.position = scrGameControl.vecStartingPosition;
			transform.rotation = scrGameControl.qtnStartingRotation;
		}
	}
	//*************************************************************************
	//	Fader Show Hide Method
	//*************************************************************************
	void FaderShowHide()
	{
		if (scrGameControl.GetPlayerIsOutOfLives())
			scrFader.EndScene("Scene_GameOver");
		else
			scrFader.PlayerDeath();
	}

	public void SetHasFlashlight(bool bln)
	{	blnHasFlashlight = bln;	}
}
