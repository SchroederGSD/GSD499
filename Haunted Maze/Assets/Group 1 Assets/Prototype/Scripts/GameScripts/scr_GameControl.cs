using UnityEngine;
using System.Collections;

public class scr_GameControl : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	public Vector3 vecLastPlayerSighting = new Vector3(1000f, 1000f, 1000f);

	private Vector3 vecStartingPosition = new Vector3(100f, 500f, 100f);
	private int intItemsCollect = 0;
	
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		vecStartingPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
