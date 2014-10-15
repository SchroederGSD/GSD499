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

	private scr_EnemyAI scrEnemyAI;
	private scr_EnemyHealth scrEnemyHealth;
	private bool blnAttackMode = false;
	private float fltBanishRate = 2f;
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		scrEnemyAI = GetComponent<scr_EnemyAI>();
		scrEnemyHealth = GetComponent<scr_EnemyHealth>();
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
				GetComponentInChildren<MeshRenderer>().material = matPatrol;
		}
		else
		{
			blnAttackMode = true;
			GetComponentInChildren<Light>().color = clrAttackLight;
			if (matAttack != null)
				GetComponentInChildren<MeshRenderer>().material = matAttack;
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

		fltOpacity = GetComponentInChildren<MeshRenderer>().material.color.a;
		fltRed = GetComponentInChildren<MeshRenderer>().material.color.r;
		fltGreen = GetComponentInChildren<MeshRenderer>().material.color.g;
		fltBlue = GetComponentInChildren<MeshRenderer>().material.color.b;

		fltOpacity = Mathf.Lerp(fltOpacity, 0f, fltBanishRate);
		GetComponentInChildren<MeshRenderer>().material.color = new Color(fltRed, fltGreen, fltBlue,fltOpacity);
	}
}
