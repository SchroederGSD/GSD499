using UnityEngine;
using System.Collections;

public class scr_ScreenFadeInOut : MonoBehaviour
{
	public float fltFadeSpeed = 1.5f;

	private bool blnSceneStarting = true;

	void Awake()
	{
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}

	void Update()
	{
		if(blnSceneStarting)
			StartScene();
	}

	void FadeToClear()
	{
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fltFadeSpeed * Time.deltaTime);
	}

	void FadeToBlack()
	{
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fltFadeSpeed * Time.deltaTime);
	}

	void StartScene()
	{
		FadeToClear();

		if(guiTexture.color.a <= 0.05f)
		{
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			blnSceneStarting = false;
		}
	}

	public void EndScene(string strLevelName)
	{
		guiTexture.enabled = true;
		FadeToBlack();

		if(guiTexture.color.a >= 0.95f)
		{
			Application.LoadLevel(strLevelName);
		}
	}
}
