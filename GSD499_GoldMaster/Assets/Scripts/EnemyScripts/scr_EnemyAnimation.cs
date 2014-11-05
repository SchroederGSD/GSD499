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

	private scr_EnemyAI scrEnemyAI = null;
	private Light enemyLight = null;
	private MeshRenderer meshEnemy = null;
	private AudioSource audioGhost = null;

	private bool blnAttackMode = false;
	private float fltBanishRate = 5f;
	private float fltDefaultOpacity = 1f;
	//******************************************************************************
	// Awake Method
	//******************************************************************************
	void Awake()
	{
		scrEnemyAI = GetComponent<scr_EnemyAI>();
		enemyLight = GetComponentInChildren<Light>();
		meshEnemy = GetComponentInChildren<MeshRenderer>();
		audioGhost = GetComponent<AudioSource>();
		fltDefaultOpacity = meshEnemy.material.color.a;
	}
	
	//******************************************************************************
	// Update Method
	//******************************************************************************
	void Update ()
	{
		if (blnAttackMode != scrEnemyAI.blnAttackMode)
			ChangeMode();
		if (scrEnemyAI.blnIsBanished)
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
			enemyLight.color = clrPatrolLight;
			if (matPatrol != null)
				meshEnemy.material = matPatrol;
		}
		else
		{
			blnAttackMode = true;
			enemyLight.color = clrAttackLight;
			if (matAttack != null)
				meshEnemy.material = matAttack;
			audioGhost.Play();
		}
	}
	//******************************************************************************
	// Banish Ghost Method
	//******************************************************************************
	void BanishGhost()
	{
		if (meshEnemy.material.color.a >= 0.05f)
		{
			meshEnemy.material.color = Color.Lerp(meshEnemy.material.color, Color.clear, fltBanishRate * Time.deltaTime);
			enemyLight.color = Color.Lerp(enemyLight.color, Color.clear, fltBanishRate * Time.deltaTime);
		}
		else
		{
			meshEnemy.material.color = Color.clear;
			enemyLight.color = Color.clear;
		}
	}
	//******************************************************************************
	// Reset Materials Method
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
