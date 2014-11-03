using UnityEngine;
using System.Collections;

public class scr_MiniMapCamera : MonoBehaviour
{
	private Transform Player = null;
	private float fltMaxX = 68f;
	private float fltMinX = 10f;
	private float fltMaxZ = 79f;
	private float fltMinZ = 10f;

	private float fltWidthAdjust = 1f;

	void Awake()
	{
		Player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		fltWidthAdjust = (float)Screen.height / (float)Screen.width;
		camera.rect = new Rect (0.01f, 0.01f, 0.35f * fltWidthAdjust, 0.35f);
	}
	void Update ()
	{
		MoveCamera();
	}

	void MoveCamera()
	{
		float fltPosX = Mathf.Clamp(Player.position.x, fltMinX, fltMaxX);
		float fltPosZ = Mathf.Clamp(Player.position.z, fltMinZ, fltMaxZ);
		transform.position = new Vector3(fltPosX, transform.position.y, fltPosZ);
	}
}
