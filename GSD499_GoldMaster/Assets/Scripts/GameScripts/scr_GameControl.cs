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
	private float fltBatteryLife = -99f;
	private float fltBatteryRecharge = 25f;
	private float fltStartDelayTime = 5f;
	private float fltStartTimer = 0f;

	private bool blnPlayerIsActive = false;
	private bool blnOpenGates = false;
	private bool blnOutOfLives = false;
	private bool blnPlayGame = false;
	private bool blnPlayDialogue = true;
	private bool blnGameOver = false;

	private scr_Animation[] scrAnimation = null;
	private GameObject objStartGateCloser = null;
	private scr_Dialogue scrDialogue = null;
	//private scr_ScreenFadeInOut scrScreenFadeInOut = null;
	private AudioSource audioGame;
	
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		vecStartingPosition = GameObject.FindGameObjectWithTag(Tags.player).transform.position;
		qtnStartingRotation = GameObject.FindGameObjectWithTag(Tags.player).transform.rotation;
		objStartGateCloser = GameObject.Find("StartGateCloser");
		scrDialogue = GetComponent<scr_Dialogue>();
		//scrScreenFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<scr_ScreenFadeInOut>();
		audioGame = GetComponent<AudioSource>();
	}
	//******************************************************************************
	// Update Method
	//******************************************************************************
	void Update()
	{
		if (blnGameOver)
			StopMusic();
		else if (blnPlayGame)
			PlayGame();
		else if (blnPlayDialogue)
			PlayDialogue();
		else if (scrDialogue.GetDialogueHasEnded())
		{
			blnPlayerIsActive = true;
			blnPlayGame = true;
		}
	}
	//******************************************************************************
	//	Play Dialogue Method
	//******************************************************************************
	void PlayDialogue()
	{
		if (fltStartTimer > fltStartDelayTime)
		{
			scrDialogue.StartDialogue();
			blnPlayDialogue = false;
		}
		else
			fltStartTimer += Time.deltaTime;
	}
	//******************************************************************************
	//	Play Game Method
	//******************************************************************************
	void PlayGame()
	{
		if (blnOutOfLives)
		{
			blnPlayerIsActive = false;
			blnGameOver = true;
		}
		if (blnOpenGates)
		{
			OpenGates("obj_EndGate");
			blnOpenGates = false;
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

		if (intNumOfLives == 0)
		{
			blnOutOfLives = true;
		}
	}
	//******************************************************************************
	//	Drain Battery Method
	//******************************************************************************
	public void DrainBattery(float amount)
	{
		fltBatteryLife -= amount;

		if (fltBatteryLife < 0)
			fltBatteryLife = 0;
	}
	//******************************************************************************
	// Check Flashlight Has Power Method
	//******************************************************************************
	public bool CheckFlashlightHasPower()
	{
		if (fltBatteryLife > 0)
			return true;
		else
			return false;
	}
	//******************************************************************************
	//	Increase Battery Life Method
	//******************************************************************************
	public void IncreaseBatteryLife()
	{
		if (fltBatteryLife == -99)
			fltBatteryLife = 100f;
		else
			fltBatteryLife += fltBatteryRecharge;
	}
	//******************************************************************************
	//	Activate Player Method
	//******************************************************************************
	public void ActivatePlayer()
	{
		if (!blnOutOfLives)
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
	//	End Game Method
	//******************************************************************************
	public void EndGame()
	{
		blnGameOver = true;
		blnPlayerIsActive = false;
	}
	//******************************************************************************
	//	Stop Music Method
	//******************************************************************************
	private void StopMusic()
	{
		if (audioGame.volume > 0.5f)
			audioGame.volume = Mathf.Lerp(audioGame.volume, 0f, 1.5f * Time.deltaTime);
		else
			audioGame.volume = 0f;
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
	public bool GetPlayerIsOutOfLives()
	{	return blnOutOfLives;			}
}
