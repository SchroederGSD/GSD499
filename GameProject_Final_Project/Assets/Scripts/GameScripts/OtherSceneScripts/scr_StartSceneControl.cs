using UnityEngine;
using System.Collections;

public class scr_StartSceneControl : MonoBehaviour
{
	public bool blnStartGame = false;

	private scr_ScreenFadeInOut scrScreenFadeInOut;

	void Awake()
	{
		scrScreenFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<scr_ScreenFadeInOut>();
	}
	void Update ()
	{
		if(blnStartGame)
			scrScreenFadeInOut.EndScene("Scene_Level01");
	}
}
