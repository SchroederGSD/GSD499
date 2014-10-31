using UnityEngine;
using System.Collections;

public class scr_StartScreen : MonoBehaviour
{
	private scr_StartSceneControl scrStartSceneControl;

	void Awake()
	{
		scrStartSceneControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_StartSceneControl>();
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width / 2f - 60f, Screen.height / 2f - 20f, 120f, 40f), "START GAME"))
		{
			scrStartSceneControl.blnStartGame = true;
			this.GetComponent<scr_StartScreen>().enabled = false;
		}
	}
}
