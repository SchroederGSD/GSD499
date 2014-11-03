using UnityEngine;
using System.Collections;

public class scr_EnemyAI : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	private scr_EnemyMovement scrEnemyMovement = null;
	private scr_EnemyAnimation scrEnemyAnimation = null;
	//private scr_EnemySight scrEnemySight = null;
	private scr_EnemyHealth scrEnemyHealth = null;
	private NavMeshAgent navAgent = null;

	private Vector3 vecStartPosition;
	private scr_GameControl scrGameControl = null;

	public Vector3 vecPlayerSightReset = new Vector3 (1000f, 1000f, 1000f);
	public Vector3 vecLastPlayerSighting;

	public bool blnPlayerInSight = false;
	public bool blnAttackMode = false;
	public bool blnIsBanished = false;
	public bool blnPlayerIsAlive = true;

	private float fltRespawnTimer = 0f;
	private float fltBanishTime = 15f;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		scrEnemyMovement = GetComponent<scr_EnemyMovement>();
		scrEnemyAnimation = GetComponent<scr_EnemyAnimation>();
		//scrEnemySight = GetComponent<scr_EnemySight>();
		scrEnemyHealth = GetComponent<scr_EnemyHealth>();
		navAgent = GetComponent<NavMeshAgent>();

		vecStartPosition = transform.position;
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();

		vecLastPlayerSighting = scrGameControl.vecResetPosition;
	}
	//******************************************************************************
	// Update Method
	//******************************************************************************
	void Update()
	{
		if (blnIsBanished)
			Respawn();
	}
	//*************************************************************************
	//	Deactivate Ghost Method - Makes ghost inactive
	//*************************************************************************
	public void DeactivateGhost()
	{
		GetComponent<CapsuleCollider>().enabled = false;
		blnPlayerInSight = false;
		if (navAgent.enabled)
			navAgent.Stop();
	}
	//*************************************************************************
	//	Activate Ghost Method - Makes ghost active
	//*************************************************************************
	public void ActivateGhost()
	{
		GetComponent<CapsuleCollider>().enabled = true;
		blnAttackMode = false;
		scrEnemyHealth.ResetHealth();
	}
	//******************************************************************************
	//  Respawn Method - Controls when enemy respawns
	//******************************************************************************
	void Respawn()
	{
		fltRespawnTimer += Time.deltaTime;
		if (fltRespawnTimer >= fltBanishTime)
		{
			fltRespawnTimer = 0f;
			blnIsBanished = false;
			scrEnemyAnimation.ResetMaterials();
			transform.position = vecStartPosition;
			scrEnemyMovement.SetWaypointIndex(0);
			ActivateGhost();
		}
	}
}
