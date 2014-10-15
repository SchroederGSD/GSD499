﻿using UnityEngine;
using System.Collections;

public class scr_PlayerControls : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltMovSpeed = 5f;
	private float fltRotSpeed = 100f;
	private float fltGravity = -9.81f;
	private float fltRayLength = 5f;
	private Vector3 vecRotation = new Vector3(0f, 0f, 1f);

	private bool blnFlashlightOn = false;
	private bool blnControlActive = true;

	private CharacterController cControler;
	private GameObject objFlashlight;

	private scr_FlashlightControl scrFlashlightControl;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		cControler = GetComponent<CharacterController>();
		objFlashlight = GameObject.Find("obj_Flashlight");
		scrFlashlightControl = objFlashlight.GetComponent<scr_FlashlightControl>();
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
			CheckFlashlightHit();
		}
	}
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update()
	{
		if (blnControlActive)
			ActivateFlashlight();
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
		LineRenderer line = GetComponent<LineRenderer>();
		Vector3 position = objFlashlight.transform.position;

		if (scrFlashlightControl.blnFlashlightActive)
		{
			if (Physics.Raycast(position, transform.forward, out hit, fltRayLength))
			{
				if (hit.transform.tag == Tags.enemy)
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
			objFlashlight.GetComponentInChildren<Light>().enabled = true;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = true;
		}
		else
		{
			objFlashlight.GetComponentInChildren<Light>().enabled = false;
			objFlashlight.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
		}
	}
}