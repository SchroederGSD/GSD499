using UnityEngine;
using System.Collections;

public class scr_EnemySight : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public bool blnPlayerInSight = false;
	public Vector3 vecLastPlayerSighting;

	private float fltFieldOfView = 100f;
	private SphereCollider colSphere;
	private scr_GameControl scrGameControl;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		colSphere = GetComponent<SphereCollider>();
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();
		vecLastPlayerSighting = scrGameControl.vecRestPosition;
	}
	//*************************************************************************
	//	On Trigger Stay Method - Player might be within sight
	//*************************************************************************
	void OnTriggerStay(Collider other)
	{
		Vector3 vecPlayerDirection;
		float fltAngle = 0f;
		RaycastHit hit;

		if(other.gameObject.tag == Tags.player)
		{
			blnPlayerInSight = false;

			vecPlayerDirection = other.transform.position - transform.position;
			fltAngle = Vector3.Angle(vecPlayerDirection, transform.forward);

			if (fltAngle < fltFieldOfView / 2f)
			{
				if (Physics.Raycast(transform.position + transform.up, vecPlayerDirection.normalized, out hit, colSphere.radius))
				{
					if (hit.collider.gameObject.tag == Tags.player)
					{
						blnPlayerInSight = true;
						vecLastPlayerSighting = hit.collider.gameObject.transform.position;
					}
				}
			}
		}
	}
	//*************************************************************************
	//	On Trigger Exit Method - Player becomes out of sight
	//*************************************************************************
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == Tags.player)
		{
			blnPlayerInSight = false;
		}
	}
}
