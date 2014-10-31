using UnityEngine;
using System.Collections;


public class scr_CreditsScreen : MonoBehaviour {

	public GUIStyle guiStyle = null;

	private Rect rectGroup = new Rect(Screen.width * 0.5f - 250f, Screen.height, 500f, 500f);
	private float fltCurrTop = Screen.height;
	private float fltScrollSpeed = 20f;
	

	private string strLeft01 = "Lead Game Designer\n" +
							   "Lead Game Programmer";

	private string strRight01 = "Brittany Thompson\n" +
								"Kyle Schroeder";

	private string strLeft02 = "Artist/Composer";
	private string strRight02 = "Title";

	private string strMiddleTitle01 = "DEVELOPERS";
	private string strMiddleTitle02 = "MUSIC";
	private string strMiddleTitle03 = "3D ARTISTS";
	private string strMiddleTitle04 = "SPECIAL THANKS";

	private string strMiddle03 = "ArtTI\nnolgraphic";
	private string strMiddle04 = "Patrick McDougle\n" +
								 "For All the Advice and Input\n\n" +
								 "To the Unity Team\n" +
								 "For Making This All Possible";
	
	// Update is called once per frame
	void Update ()
	{
		fltCurrTop -= fltScrollSpeed * Time.deltaTime;
		rectGroup = new Rect(Screen.width * 0.5f - 250f, fltCurrTop, 500f, 500f);

		if (fltCurrTop <= -520f)
			Application.LoadLevel("Scene_StartMenu");
	}

	void OnGUI()
	{
		GUI.BeginGroup(rectGroup);

		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 0f, 150f, 1f), strMiddleTitle01, guiStyle);
		guiStyle.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 160f, 37f, 150f, 1f), strLeft01, guiStyle);
		guiStyle.alignment = TextAnchor.UpperLeft;
		GUI.Label(new Rect(rectGroup.width * 0.5f + 10f, 37f, 150f, 1f), strRight01, guiStyle);

		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 112f, 150f, 1f), strMiddleTitle02, guiStyle);
		guiStyle.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 160f, 149f, 150f, 1f), strLeft02, guiStyle);
		guiStyle.alignment = TextAnchor.UpperLeft;
		GUI.Label(new Rect(rectGroup.width * 0.5f + 10f, 149f, 150f, 1f), strRight02, guiStyle);

		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 199f, 150f, 1f), strMiddleTitle03, guiStyle);
		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 236f, 150f, 1f), strMiddle03, guiStyle);

		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 311f, 150f, 1f), strMiddleTitle04, guiStyle);
		guiStyle.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 348f, 150f, 1f), strMiddle04, guiStyle);

		GUI.EndGroup();
	}
}
