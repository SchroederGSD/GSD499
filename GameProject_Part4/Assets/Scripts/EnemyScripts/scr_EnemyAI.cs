using UnityEngine;
using System.Collections;

public class scr_EnemyAI : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public Transform[] patrolWaypoints;
	
	private NavMeshAgent navAgent;

	private const float fltChaseSpeed = 5f;
	private const float fltPartolSpeed = 2f;

	private int intWaypointIndex = 0;
	private float fltSpeed = 0f;
	private float fltPatrolWaitTime = 0.5f;
	private float fltPatrolTimer = 0f;
	private Vector3 vecPatrolDestination;

	private bool blnAttackMode = false;
	private scr_EnemySight scrEnemySight;
	private scr_GameControl scrGameControl;
	private scr_PlayerControls scrPlayerControls;
	private AudioSource audioGhost;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.enabled = false;
		scrEnemySight = GetComponent<scr_EnemySight>();
		scrPlayerControls = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<scr_PlayerControls>();
		vecPatrolDestination = patrolWaypoints[0].position;
		audioGhost = GetComponent<AudioSource>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		if (scrEnemySight.blnPlayerInSight || blnAttackMode)
		{
			fltSpeed = fltChaseSpeed;
			Chase();
		}
		else
		{
			blnAttackMode = false;
			fltSpeed = fltPartolSpeed;
			Patrol();
		}
	}
	//*************************************************************************
	//	Patrol Method - Enemy moves between waypoints
	//*************************************************************************
	void Patrol()
	{
		Vector3 vecTemp = new Vector3 (1000f, 1000f, 1000f);
		float fltDistanceRemaining = 0f;

		navAgent.enabled = false;
		fltDistanceRemaining = Vector3.Distance(transform.position, vecPatrolDestination);

		if (vecPatrolDestination == vecTemp || fltDistanceRemaining < navAgent.stoppingDistance)
		{
			fltPatrolTimer += Time.deltaTime;

			if (fltPatrolTimer >= fltPatrolWaitTime)
			{
				fltPatrolTimer = 0f;
				if (intWaypointIndex == patrolWaypoints.Length - 1)
					intWaypointIndex = 0;
				else
					intWaypointIndex++;
			}
		}
		else
		{
			fltPatrolTimer = 0f;
			transform.LookAt(vecPatrolDestination);
			transform.position += transform.forward * Time.deltaTime * fltSpeed;
		}

		vecPatrolDestination = patrolWaypoints[intWaypointIndex].position;
	}
	//*************************************************************************
	//	Chase Method - Enemy
	//*************************************************************************
	void Chase()
	{
		//Vector3 vecTemp = new Vector3 (1000f, 1000f, 1000f);
		navAgent.enabled = true;
		navAgent.speed = fltSpeed;

		if (!blnAttackMode)
		{
			audioGhost.Play();
			blnAttackMode = true;
		}

		if(true) // player is not dead)
		{
			if (scrEnemySight.blnPlayerInSight)
			{
				navAgent.destination = scrEnemySight.vecLastPlayerSighting;
				transform.LookAt(transform.position + navAgent.desiredVelocity);
			}
			else if (navAgent.remainingDistance < navAgent.stoppingDistance)
			{
				scrPlayerControls.KillPlayer();
				blnAttackMode = false;
			}
		}
	}
	//*************************************************************************
	//	Banish Ghost Method - Stops movement and makes ghost inactive
	//*************************************************************************
	public void BanishGhost()
	{
		if (navAgent.enabled)
			navAgent.Stop();
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<SphereCollider>().enabled = false;
		GetComponentInChildren<Light>().enabled = false;
		blnAttackMode = false;
	}
	//*************************************************************************
	//	Respawn Ghost Method - Makes ghost active
	//*************************************************************************
	public void RespawnGhost()
	{
		navAgent.enabled = false;
		GetComponent<CapsuleCollider>().enabled = true;
		GetComponent<SphereCollider>().enabled = true;
		GetComponentInChildren<Light>().enabled = true;
		scrEnemySight.blnPlayerInSight = false;
	}
	//*************************************************************************
	// Setters and Getters
	//*************************************************************************
	public void SetWaypointIndex(int index)
	{	intWaypointIndex = index;	}
	
}
