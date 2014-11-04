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
	private float fltBatteryLife = 100f;
	private float fltBatteryRecharge = 25f;

	private bool blnPlayerIsActive = true;
	private bool blnOpenGates = false;
	private bool blnOutOfLives = false;

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
		if (blnOpenGates)
		{
			OpenGates("obj_EndGate");
			blnOpenGates = false;
		}

		if (blnOutOfLives)
		{
			blnPlayerIsActive = false;
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

		if (intCollectiblesFound == 7)
			blnOpenGates = true;
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
	//	Lost Life Method - Decreases the number of lives by one
	//******************************************************************************
	public void LostLife()
	{
		intNumOfLives--;

		if (intNumOfLives <= 0)
			blnOutOfLives = true;
	}
	//******************************************************************************
	//	Drain Battery Method
	//******************************************************************************
	public void DrainBattery(float amount)
	{
		fltBatteryLife -= amount;

		if (fltBatteryLife < 0)
			fltBatteryLife = 0;
		print (fltBatteryLife);
	}
	//******************************************************************************
	//	
	//******************************************************************************
	public void IncreaseBatteryLife()
	{
		fltBatteryLife += fltBatteryRecharge;
	}
	//******************************************************************************
	//	Activate Player Method
	//******************************************************************************
	public void ActivatePlayer()
	{
		blnPlayerIsActive = true;
	}
	//******************************************************************************
	//	Deactivate Player Method
	//******************************************************************************
	public void DeactivatePlayer()
	{
		blnPlayerIsActive = false;
	}
	//******************************************************************************
	//	Getter Methods
	//******************************************************************************
	public int GetCollectibleCount()
	{	return intCollectiblesFound;	}
	public int GetNumOfLives()
	{	return intNumOfLives;			}
	public float GetBatteryLife()
	{	return fltBatteryLife;			}
	public bool GetPlayerIsActive()
	{	return blnPlayerIsActive;		}
}
