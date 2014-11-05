using UnityEngine;
using System.Collections;

public class scr_EndGame : MonoBehaviour
{
	private scr_GameControl scrGameControl = null;
	private scr_ScreenFadeInOut scrScreenFadeInOut = null;

	void Awake()
	{
		scrGameControl = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<scr_GameControl>();
		scrScreenFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<scr_ScreenFadeInOut>();
	}

	void OnTriggerStay()
	{
		scrGameControl.EndGame();
		scrScreenFadeInOut.EndScene("Scene_Credits");
	}
}
