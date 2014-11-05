using UnityEngine;
using System.Collections;

public class scr_MenuScreen : MonoBehaviour
{
	private scr_StartSceneControl scrStartSceneControl;

	public Texture txtTitle;

	void Awake()
	{
		scrStartSceneControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_StartSceneControl>();
	}

	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width * 0.5f - 272f, Screen.height * 0.5f - 150f, 550f, 100f), txtTitle);

		if (GUI.Button(new Rect(Screen.width / 2f - 60f, Screen.height / 2f - 20f, 120f, 40f), "START GAME"))
		{
			scrStartSceneControl.blnStartGame = true;
			GetComponent<scr_MenuScreen>().enabled = false;
		}

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) 
		{
			scrStartSceneControl.blnStartGame = true;
			GetComponent<scr_MenuScreen>().enabled = false;
		}
	}
}
