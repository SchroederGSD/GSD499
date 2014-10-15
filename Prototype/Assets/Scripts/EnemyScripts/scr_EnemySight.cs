using UnityEngine;
using System.Collections;

public class scr_EnemySight : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public bool blnPlayerInSight = false;
	private float fltFieldOfView = 100f;
	private SphereCollider colSphere;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		colSphere = GetComponent<SphereCollider>();
	}
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update ()
	{
	
	}
	//*************************************************************************
	//	On Trigger Stay Method
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
					}
				}
			}
		}
	}
	//*************************************************************************
	//	On Trigger Stay Method
	//*************************************************************************
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == Tags.player)
		{
			blnPlayerInSight = false;
		}
	}
}
