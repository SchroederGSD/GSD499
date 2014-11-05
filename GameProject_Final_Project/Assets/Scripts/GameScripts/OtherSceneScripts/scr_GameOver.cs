using UnityEngine;
using System.Collections;

public class scr_GameOver : MonoBehaviour
{
	public Texture txtGameOver;

	private float fltGameOverWaitTime = 5f;
	private float fltGameOverTimer = 0f;

	void Update ()
	{
		if (fltGameOverTimer >= fltGameOverWaitTime)
			Application.LoadLevel("Scene_StartMenu");
		else
			fltGameOverTimer += Time.deltaTime;
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width * 0.5f - 210f, Screen.height * 0.5f - 36f, 430f, 80f), txtGameOver);
	}
}
