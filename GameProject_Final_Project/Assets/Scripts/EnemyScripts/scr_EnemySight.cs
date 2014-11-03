using UnityEngine;
using System.Collections;

public class scr_EnemySight : MonoBehaviour {
	//***********************************
	//	Variables
	//***********************************
	public bool blnCanSee = true;

	private float fltFieldOfView = 100f;
	private float fltSighDistance = 12f;
	private float fltSightDisableTime = 0.75f;
	private float fltDisableTimer = 0f;
	
	private scr_EnemyAI scrEnemyAI = null;
	
	private Transform Player;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		scrEnemyAI = GetComponent<scr_EnemyAI>();
		Player = GameObject.FindGameObjectWithTag(Tags.player).transform;
	}
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update()
	{
		if (!scrEnemyAI.blnIsBanished)
		{
			WallDistanceCheck();

			if (blnCanSee)
				FindDistanceToPlayer();
			else
				RestoreSight();
		}
	}
	//*************************************************************************
	//	Find Distance To Player Method - Sees if player is within maximum sight range
	//*************************************************************************
	void FindDistanceToPlayer()
	{
		float fltDistance = 0f;

		fltDistance = Vector3.Distance(transform.position, Player.position);

		if (fltDistance < fltSighDistance)
			CheckLineOfSight();
		else
			scrEnemyAI.blnPlayerInSight = false;
	}
	//*************************************************************************
	//	Check Line Of Sight Method - Checks is player is in sight
	//*************************************************************************
	void CheckLineOfSight()
	{
		Vector3 vecPlayerDirection;
		float fltAngle = 0f;
		RaycastHit hit;

		vecPlayerDirection = Player.position - transform.position;
		fltAngle = Vector3.Angle(vecPlayerDirection, transform.forward);
		
		if (fltAngle < fltFieldOfView / 2f)
		{
			if (Physics.Raycast(transform.position + transform.up, vecPlayerDirection.normalized, out hit, fltSighDistance))
			{
				if (hit.collider.gameObject.tag == Tags.player)
				{
					scrEnemyAI.blnPlayerInSight = true;
					scrEnemyAI.blnAttackMode = true;
					scrEnemyAI.vecLastPlayerSighting = hit.collider.gameObject.transform.position;
				}
				else
					scrEnemyAI.blnPlayerInSight = false;
			}
		}
	}
	//*************************************************************************
	//	Wall Distance Check Method - Prevents enemy from detecting through walls
	//*************************************************************************
	void WallDistanceCheck()
	{
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 0.5f))
		{
			if (hit.collider.gameObject.tag == Tags.level)
			{
				blnCanSee = false;
				fltDisableTimer = 0f;
			}
		}
	}
	//*************************************************************************
	//	Restore Sight Method - Allows enemy to see after passing through wall
	//*************************************************************************
	void RestoreSight()
	{
		fltDisableTimer += Time.deltaTime;
		
		if (fltDisableTimer >= fltSightDisableTime)
			blnCanSee = true;
	}
}
