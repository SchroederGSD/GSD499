using UnityEngine;
using System.Collections;

public class scr_EnemyAnimation : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	public Color clrPatrolLight = new Color(73f/255f, 247f/255f, 1f, 1f);
	public Color clrAttackLight = new Color(1f, 0f, 26f/255f, 1f);
	public Material matPatrol;
	public Material matAttack;

	private MeshRenderer meshEnemy;
	private scr_EnemyAI scrEnemyAI;
	private scr_EnemyHealth scrEnemyHealth;
	private bool blnAttackMode = false;
	private float fltBanishRate = 0.05f;
	private float fltDefaultOpacity = 1f;
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		scrEnemyAI = GetComponent<scr_EnemyAI>();
		scrEnemyHealth = GetComponent<scr_EnemyHealth>();
		meshEnemy = GetComponentInChildren<MeshRenderer>();
		fltDefaultOpacity = meshEnemy.material.color.a;
	}
	
	//******************************************************************************
	// Update Method
	//******************************************************************************
	void Update ()
	{
		if (blnAttackMode != scrEnemyAI.GetCurrentMode())
			ChangeMode();
		if (scrEnemyHealth.GetIsBanished())
			BanishGhost();
	}
	//******************************************************************************
	// Change Status Method - Changes appearance of enemy based on current mode
	//******************************************************************************
	void ChangeMode()
	{
		if (blnAttackMode)
		{
			blnAttackMode = false;
			GetComponentInChildren<Light>().color = clrPatrolLight;
			if (matPatrol != null)
				meshEnemy.material = matPatrol;
		}
		else
		{
			blnAttackMode = true;
			GetComponentInChildren<Light>().color = clrAttackLight;
			if (matAttack != null)
				meshEnemy.material = matAttack;
		}
	}
	//******************************************************************************
	// Banish Ghost Method
	//******************************************************************************
	void BanishGhost()
	{
		float fltOpacity = 0f;
		float fltRed = 0f;
		float fltGreen = 0f;
		float fltBlue = 0f;

		fltOpacity = meshEnemy.material.color.a;
		fltRed = meshEnemy.material.color.r;
		fltGreen = meshEnemy.material.color.g;
		fltBlue = meshEnemy.material.color.b;

		fltOpacity = Mathf.Lerp(fltOpacity, 0f, fltBanishRate);
		meshEnemy.material.color = new Color(fltRed, fltGreen, fltBlue,fltOpacity);
	}
	//******************************************************************************
	// Banish Ghost Method
	//******************************************************************************
	public void ResetMaterials()
	{
		float fltOpacity = 0f;
		float fltRed = 0f;
		float fltGreen = 0f;
		float fltBlue = 0f;

		fltOpacity = fltDefaultOpacity;
		fltRed = matPatrol.color.r;
		fltGreen = matPatrol.color.g;
		fltBlue = matPatrol.color.b;
		matPatrol.color = new Color(fltRed, fltGreen, fltBlue,fltOpacity);

		fltOpacity = fltDefaultOpacity;
		fltRed = matAttack.color.r;
		fltGreen = matAttack.color.g;
		fltBlue = matAttack.color.b;
		matAttack.color = new Color(fltRed, fltGreen, fltBlue,fltOpacity);

		meshEnemy.material = matPatrol;
		GetComponentInChildren<Light>().color = clrPatrolLight;
	}
}
