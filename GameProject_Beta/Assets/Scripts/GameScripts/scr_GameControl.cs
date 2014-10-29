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
	private int intNumOfLives = 3;
	private scr_Animation[] scrAnimation = null;
	private GameObject objStartGateCloser = null;
	
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		vecStartingPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
		qtnStartingRotation = GameObject.FindGameObjectWithTag(Tags.player).transform.rotation;
		objStartGateCloser = GameObject.Find("StartGateCloser");
	}

	void Update()
	{
		if (intCollectiblesFound == 7)
		{
			OpenGates("obj_EndGate");
			intCollectiblesFound = 0;
		}
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
	//******************************************************************************
	//	Open Gates Method
	//******************************************************************************
	public void OpenGates(string strGateName)
	{
		if (strGateName == "obj_StartGate")
			objStartGateCloser.SetActive(true);

		scrAnimation = GameObject.Find(strGateName).GetComponentsInChildren<scr_Animation>();
		foreach (scr_Animation anim in scrAnimation)
			anim.OpenGate();
	}
	//******************************************************************************
	//	Getter Methods
	//******************************************************************************
	public int GetCollectibleCount()
	{	return intCollectiblesFound;	}
	public int GetNumOfLives()
	{	return intNumOfLives;			}
}
