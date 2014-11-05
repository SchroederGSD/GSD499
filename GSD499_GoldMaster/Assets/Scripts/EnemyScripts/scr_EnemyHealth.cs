using UnityEngine;
using System.Collections;

public class scr_EnemyHealth : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	private float fltHealth = 100f;

	private scr_EnemyAI scrEnemyAI;

	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		scrEnemyAI = GetComponent<scr_EnemyAI>();
	}
	//******************************************************************************
	// Take Damage Method - Reduces enemy's health
	//******************************************************************************
	public void TakeDamage(float amount)
	{
		fltHealth -= amount;
		
		if (fltHealth <= 0f)
		{
			scrEnemyAI.blnIsBanished = true;
			scrEnemyAI.DeactivateGhost();
		}
	}
	//******************************************************************************
	//	Reset Health Method - Sets the enemy's health back to full
	//******************************************************************************
	public void ResetHealth()
	{
		fltHealth = 100f;
	}
}
