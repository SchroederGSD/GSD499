using UnityEngine;
using System.Collections;

public class scr_CreditsScreen : MonoBehaviour {

	public GUIStyle styleTitle = null;
	public GUIStyle styleInfo = null;

	public Texture txtCredits;

	private Rect rectGroup = new Rect(Screen.width * 0.5f - 250f, 0f, 500f, 750f);
	private float fltCurrTop = Screen.height;
	private float fltScrollSpeed = 40f;
	

	private string strLeft01 = "Lead Game Designer\n" +
							   "Lead Game Programmer";

	private string strRight01 = "Brittany Thompson\n" +
								"Kyle Schroeder";

	private string strLeft02 = "Akuma Kira\n\n" + "Mike Koenig\n" + "Alluro95";
	private string strRight02 = "Drowned\n" + "Sleep\n" + "Click SoundFX\n" + "Boo SoundFX";
	
	private string strMiddleTitle01 = "DEVELOPERS";
	private string strMiddleTitle02 = "MUSIC";
	private string strMiddleTitle03 = "3D ARTISTS";
	private string strMiddleTitle04 = "SPECIAL THANKS";
	
	private string strMiddle03 = "3DRT\nArtTI\nnolgraphic\nUnityTechnologies\nUniversal Games\nVIS Games\nYuji Nagata\n";
	private string strMiddle04 = "To Patrick McDougle\n" +
								 "For All the Advice and Input\n\n" +
								 "To the Unity Team\n" +
								 "For Making This All Possible";
	
	// Update is called once per frame
	void Update ()
	{
		fltCurrTop -= fltScrollSpeed * Time.deltaTime;
		rectGroup = new Rect(Screen.width * 0.5f - 250f, fltCurrTop, 500f, 750f);

		if (fltCurrTop <= -770f)
			Application.LoadLevel("Scene_StartMenu");
	}

	void OnGUI()
	{
		GUI.BeginGroup(rectGroup);

		GUI.Label(new Rect(rectGroup.width * 0.5f - 112f, 0f, 230f, 50f), txtCredits);

		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 75f, 150f, 1f), strMiddleTitle01, styleTitle);
		styleInfo.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 160f, 110f, 150f, 1f), strLeft01, styleInfo);
		styleInfo.alignment = TextAnchor.UpperLeft;
		GUI.Label(new Rect(rectGroup.width * 0.5f + 10f, 110f, 150f, 1f), strRight01, styleInfo);

		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 185f, 150f, 1f), strMiddleTitle02, styleTitle);
		styleInfo.alignment = TextAnchor.UpperRight;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 160f, 220f, 150f, 1f), strLeft02, styleInfo);
		styleInfo.alignment = TextAnchor.UpperLeft;
		GUI.Label(new Rect(rectGroup.width * 0.5f + 10f, 220f, 150f, 1f), strRight02, styleInfo);

		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 340f, 150f, 1f), strMiddleTitle03, styleTitle);
		styleInfo.alignment = TextAnchor.UpperCenter;
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 375f, 150f, 1f), strMiddle03, styleInfo);

		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 562.5f, 150f, 1f), strMiddleTitle04, styleTitle);
		GUI.Label(new Rect(rectGroup.width * 0.5f - 75f, 597.5f, 150f, 1f), strMiddle04, styleInfo);

		GUI.EndGroup();
	}
}
