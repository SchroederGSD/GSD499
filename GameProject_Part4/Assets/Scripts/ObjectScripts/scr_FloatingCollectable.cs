using UnityEngine;
using System.Collections;

public class scr_FloatingCollectable : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltStartPosY = 0f;
	private float desiredPosY = 0f;
	private float fltDeltaHeight = 0.25f;
	private float fltRotationSpeed = 20f;

	//*************************************************************************
	//	Awake Method
	//*************************************************************************
	void Awake()
	{
		fltStartPosY = transform.position.y;
		desiredPosY = fltStartPosY + fltDeltaHeight;
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
		float curPosX = transform.position.x;
		float curPosY = transform.position.y;
		float curPosZ = transform.position.z;

		float newPosY = curPosY + (fltDeltaHeight * Time.deltaTime);
		float newRot = fltRotationSpeed * Time.deltaTime;

		transform.position = new Vector3(curPosX, newPosY, curPosZ);
		transform.Rotate(0f, 0f, newRot);

		if (Mathf.Abs(curPosY - desiredPosY) < 0.01f)
		{
			fltDeltaHeight *= -1f;
			desiredPosY = fltStartPosY + fltDeltaHeight;
		}
	}
}
