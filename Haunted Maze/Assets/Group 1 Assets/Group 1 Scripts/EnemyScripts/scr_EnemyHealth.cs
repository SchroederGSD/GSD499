using UnityEngine;
using System.Collections;

public class scr_EnemyHealth : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltHealth = 100f;
	private float fltTimer = 0f;
	private float fltBanishTime = 15f;
	private bool blnBanished = false;

	private Vector3 vecStartPostion = new Vector3(100f, 500f, 100f);
	private scr_EnemyAI scrEnemyAI;

	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		vecStartPostion = transform.position;
		scrEnemyAI = GetComponent<scr_EnemyAI>();
	}
	//******************************************************************************
	// Update Method
	//******************************************************************************
	void Update()
	{
		if (blnBanished)
			Respawn();
	}
	//******************************************************************************
	//  Respawn Method - Controls when enemy respawns
	//******************************************************************************
	void Respawn()
	{
		fltTimer += Time.deltaTime;
		if (fltTimer >= fltBanishTime)
		{
			fltTimer = 0f;
			blnBanished = false;
			ResetPosition();
		}
	}
	//******************************************************************************
	//  Reset Position Method - Resets enemy back to starting position
	//******************************************************************************
	void ResetPosition()
	{
		scrEnemyAI.SetWaypointIndex(0);
		transform.position = vecStartPostion;
		scrEnemyAI.enabled = true;
	}
	//******************************************************************************
	// Take Damage Method - Reduces enemy's health
	//******************************************************************************
	public void TakeDamage(float amount)
	{
		fltHealth -= amount;

		if (fltHealth <= 0f)
		{
			blnBanished = true;
			scrEnemyAI.enabled = false;
		}
	}
	//*************************************************************************
	// Setters and Getters
	//*************************************************************************
	public bool GetIsBanished()
	{	return blnBanished;		}
}
