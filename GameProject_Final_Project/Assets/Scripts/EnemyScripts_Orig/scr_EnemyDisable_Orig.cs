using UnityEngine;
using System.Collections;

public class scr_EnemyDisable_Orig : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
/*
	private SphereCollider colSphere;
	private float fltSightDisableTime = 0.75f;
	private float fltDisableTimer = 0f;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		colSphere = GetComponent<SphereCollider>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate()
	{
		WallDistanceCheck();
		if (!colSphere.enabled)
		{
			fltDisableTimer += Time.deltaTime;

			if (fltDisableTimer >= fltSightDisableTime)
				colSphere.enabled = true;
		}
	}
	//*************************************************************************
	//	Wall Distance Check Method - Prevents enemy from detecting through walls
	//*************************************************************************
	void WallDistanceCheck()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 0.5f))
		{
			if (hit.collider.gameObject.tag == Tags.level)
			{
				colSphere.enabled = false;
				fltDisableTimer = 0f;
			}
		}
	}
*/
}
