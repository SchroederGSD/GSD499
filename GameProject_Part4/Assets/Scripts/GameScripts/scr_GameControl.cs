using UnityEngine;
using System.Collections;

public class scr_GameControl : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	public Vector3 vecLastPlayerSighting = new Vector3(1000f, 1000f, 1000f);
	public Vector3 vecResetPosition = new Vector3(1000f, 1000f, 1000f);
	public Vector3 vecStartingPosition = new Vector3(100f, 500f, 100f);
	public Quaternion qtnStartingRotation;

	private int intCollectiblesFound = 0;
	
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		vecStartingPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
		qtnStartingRotation = GameObject.FindGameObjectWithTag(Tags.player).transform.rotation;
	}
	//******************************************************************************
	//	Remove Object Method - Removes object from scene
	//******************************************************************************
	public void RemoveObject(GameObject obj)
	{
		Destroy(obj);
	}
	//******************************************************************************
	//	Found Collectible Method - Increases number of collectibles found
	//******************************************************************************
	public void FoundCollectible()
	{
		intCollectiblesFound++;
	}
}
