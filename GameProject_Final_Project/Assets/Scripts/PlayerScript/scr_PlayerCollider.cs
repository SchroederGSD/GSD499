using UnityEngine;
using System.Collections;

public class scr_PlayerCollider : MonoBehaviour
{
	
	private scr_PlayerControls scrPlayerControls = null;
	private scr_GameControl scrGameControl = null;

	void Awake()
	{
		scrPlayerControls = GetComponent<scr_PlayerControls>();
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();
	}
	//*************************************************************************
	//	On Trigger Enter Method
	//*************************************************************************
	void OnTriggerEnter(Collider other)
	{
		GameObject obj = other.transform.gameObject;

		if (obj.tag == Tags.item)
		{
			scrPlayerControls.SetHasFlashlight(true);
			scrGameControl.RemoveObject(obj);
			scrGameControl.OpenGates("obj_StartGate");
		}
		else if (obj.tag == Tags.collectible)
		{
			scrGameControl.FoundCollectible();
			scrGameControl.RemoveObject(obj);
		}
		else if (obj.tag == Tags.enemy)
		{
			scrPlayerControls.KillPlayer();
		}
	}
}
