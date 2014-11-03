using UnityEngine;
using System.Collections;

public class scr_FloatingCollectable : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltRotationSpeed = 50f;
	private float fltVertSpeed = 0.25f;
	private float fltDeltaHeight = 0.5f;

	private float fltMaxHeight = 0f;
	private float fltMinHeight = 0f;

	private float fltDirection = -1f;

	//*************************************************************************
	//	Awake Method
	//*************************************************************************
	void Awake()
	{
		fltMaxHeight = transform.position.y + fltDeltaHeight;
		fltMinHeight = transform.position.y - fltDeltaHeight;
	}
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update ()
	{
		Floating();
	}
	//*************************************************************************
	//	Floating Method - Moves objects up and down, and spins them
	//*************************************************************************
	void Floating()
	{
		float fltPosX = transform.position.x;
		float fltPosY = transform.position.y;
		float fltPosZ = transform.position.z;
		float fltNewRot = 0f;

		if (fltPosY > fltMaxHeight)
			fltDirection = -1f;
		else if (fltPosY < fltMinHeight)
			fltDirection = 1f;

		fltPosY += fltVertSpeed * fltDirection * Time.deltaTime;
		fltNewRot = fltRotationSpeed * Time.deltaTime;

		transform.position = new Vector3(fltPosX, fltPosY, fltPosZ);
		transform.Rotate(0f, 0f, fltNewRot);
	}
}
