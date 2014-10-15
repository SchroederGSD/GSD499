using UnityEngine;
using System.Collections;

public class scr_EnemyAI : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public Transform[] patrolWaypoints;
	
	private NavMeshAgent navAgent;

	private const float fltChaseSpeed = 10f;
	private const float fltPartolSpeed = 5f;

	private float fltSpeed = 0f;
	private float fltPatrolWaitTime = 0.5f;
	private float fltPatrolTimer = 0f;
	private int intWaypointIndex = 0;

	private bool blnAttackMode = false;
	private scr_EnemySight scrEnemySight;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
		fltSpeed = fltPartolSpeed;
		scrEnemySight = GetComponent<scr_EnemySight>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		if (scrEnemySight.blnPlayerInSight)
			blnAttackMode = true;
		else
			blnAttackMode = false;
		Patrol();
	}
	//*************************************************************************
	//	Patrol Method - Enemy moves between waypoints
	//*************************************************************************
	void Patrol()
	{
		Vector3 vecTemp = new Vector3 (1000f, 1000f, 1000f);

		navAgent.speed = fltSpeed;

		if(navAgent.destination == vecTemp || navAgent.remainingDistance < navAgent.stoppingDistance)
		{
			fltPatrolTimer += Time.deltaTime;

			if(fltPatrolTimer >= fltPatrolWaitTime)
			{
				if(intWaypointIndex == patrolWaypoints.Length - 1)
					intWaypointIndex = 0;
				else
					intWaypointIndex++;

				fltPatrolTimer = 0f;
			}
		}
		else
			fltPatrolTimer = 0f;

		navAgent.destination = patrolWaypoints[intWaypointIndex].position;
		transform.LookAt(transform.position + navAgent.desiredVelocity);
	}
	//*************************************************************************
	//	Banish Ghost Method - Stops movement and makes ghost inactive
	//*************************************************************************
	public void BanishGhost()
	{
		navAgent.Stop();
		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<SphereCollider>().enabled = false;
		GetComponentInChildren<Light>().enabled = false;
	}
	//*************************************************************************
	//	Respawn Ghost Method - Makes ghost active
	//*************************************************************************
	public void RespawnGhost()
	{
		GetComponent<CapsuleCollider>().enabled = true;
		GetComponent<SphereCollider>().enabled = true;
		GetComponentInChildren<Light>().enabled = true;
	}
	//*************************************************************************
	// Setters and Getters
	//*************************************************************************
	public void SetWaypointIndex(int index)
	{	intWaypointIndex = index;	}

	public bool GetCurrentMode()
	{	return blnAttackMode;		}
}
