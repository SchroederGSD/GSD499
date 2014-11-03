using UnityEngine;
using System.Collections;

public class scr_EnemyMovement : MonoBehaviour {

	//***********************************
	//	Variables
	//***********************************
	public Transform[] patrolWaypoints;
	
	private NavMeshAgent navAgent;
	
	private const float fltChaseSpeed = 5f;
	private const float fltPartolSpeed = 2f;
	private const float fltRotateSpeed = 180f;
	private const float fltDeadZone = 5f;
	
	private int intWaypointIndex = 0;
	private float fltSpeed = 0f;
	private float fltPatrolWaitTime = 1f;
	private float fltPatrolTimer = 0f;
	private float fltChaseWaitTime = 3f;
	private float fltOutOfSightTimer = 0f;

	private Vector3 vecPatrolDestination;

	private scr_EnemyAI scrEnemyAI = null;
	
	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.enabled = false;
		vecPatrolDestination = patrolWaypoints[0].position;
		scrEnemyAI = GetComponent<scr_EnemyAI>();
	}
	//*************************************************************************
	//	Fixed Update Method
	//*************************************************************************
	void FixedUpdate ()
	{
		if (!scrEnemyAI.blnIsBanished)
		{
			if (scrEnemyAI.blnPlayerInSight || scrEnemyAI.blnAttackMode)
				Chase();
			else
				Patrol();
		}
	}
	//*************************************************************************
	//	Patrol Method - Enemy moves between waypoints
	//*************************************************************************
	void Patrol()
	{
		float fltDistanceRemaining = 0f;
		
		navAgent.enabled = false;
		fltSpeed = fltPartolSpeed;

		fltDistanceRemaining = Vector3.Distance(transform.position, vecPatrolDestination);
		
		if (vecPatrolDestination == scrEnemyAI.vecPlayerSightReset || fltDistanceRemaining < navAgent.stoppingDistance)
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
			RotateGhost(transform.forward, vecPatrolDestination - transform.position, transform.up);
			transform.position += transform.forward * Time.deltaTime * fltSpeed;
		}
		
		vecPatrolDestination = patrolWaypoints[intWaypointIndex].position;
	}
	//*************************************************************************
	//	Chase Method - Enemy
	//*************************************************************************
	void Chase()
	{
		navAgent.enabled = true;
		navAgent.speed = fltChaseSpeed;
		
		if(scrEnemyAI.blnPlayerIsAlive)
		{
			if (scrEnemyAI.blnPlayerInSight)
			{
				navAgent.destination = scrEnemyAI.vecLastPlayerSighting;
				RotateGhost(transform.forward, navAgent.desiredVelocity, transform.up);
				fltOutOfSightTimer = 0f;
			}
			else if (navAgent.remainingDistance < navAgent.stoppingDistance)
			{
				if (fltOutOfSightTimer < fltChaseWaitTime)
					fltOutOfSightTimer += Time.deltaTime;
				else
					scrEnemyAI.blnAttackMode = false;
			}
		}
	}
	//*************************************************************************
	//	Rotate Ghost Method - Rotates the ghost to face its destination
	//*************************************************************************
	private void RotateGhost(Vector3 vecFrom, Vector3 vecTo, Vector3 vecUp)
	{
		float fltRotSpeed = fltRotateSpeed;
		float fltAngle = 0f;
		Vector3 normal;

		fltAngle = Vector3.Angle(vecFrom, vecTo);

		if (fltAngle < fltDeadZone)
			transform.LookAt(vecTo + transform.position);
		else
		{
			normal = Vector3.Cross(vecFrom, vecTo);
			fltRotSpeed *= Mathf.Sign(Vector3.Dot(normal, vecUp));
			transform.Rotate(0f, fltRotSpeed * Time.deltaTime, 0f);
		}
	}
	//*************************************************************************
	// Setters and Getters
	//*************************************************************************
	public void SetWaypointIndex(int index)
	{	intWaypointIndex = index;	}
}
