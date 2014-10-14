using UnityEngine;
using System.Collections;

public class scr_EnemyAI : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public Transform[] patrolWaypoints;

	private NavMeshAgent navAgent;

	private float fltPatrolWaitTime = 0.5f;
	private float fltPatrolTimer = 0f;
	private int intWaypointIndex = 0;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		Patrol();
	}
	//*************************************************************************
	//	Patrol Method - Enemy moves between waypoints
	//*************************************************************************
	void Patrol()
	{
		Vector3 vecTemp = new Vector3 (1000f, 1000f, 1000f);

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
}
