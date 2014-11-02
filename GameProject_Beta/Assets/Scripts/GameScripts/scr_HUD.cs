using UnityEngine;
using System.Collections;

public class scr_HUD : MonoBehaviour
{
	//***********************************
	//	Variables
	//***********************************
	public GUIStyle guiStyle = new GUIStyle();
	public Texture txtLives;
	public Texture txtDiamonds;
	public Texture txtPowerBar; //BT
	public Texture txtBattery; //BT
	public Camera miniMapCam; //BT

	private scr_GameControl scrGameControl;
	private int intNumOfLives;
	private int intCollectibleCount;
	
	void Awake()
	{
		scrGameControl = GetComponent<scr_GameControl>();
	}
	//******************************************************************************
	//	On Gui Method - 
	//******************************************************************************
	void OnGUI()
	{

		//GUI.Label (new Rect (10f, 10f, 100f, 25f), "Lives", guiStyle);
		intNumOfLives = scrGameControl.GetNumOfLives();
		intCollectibleCount = scrGameControl.GetCollectibleCount();

		GUI.DrawTexture(new Rect(50f, 10f, 146f, 75f), txtLives);
		GUI.DrawTexture(new Rect(Screen.width - 205f, 10f, 155f, 75f), txtDiamonds); 
		GUI.DrawTexture (new Rect (Screen.width - 158f, Screen.height - 62f, 100f, 30f), txtPowerBar); //BT
		GUI.DrawTexture (new Rect (Screen.width - 160f, Screen.height - 65f, 120f, 40f), txtBattery); //BT
		GUI.Label(new Rect(150f, 38f, 100f, 50f), intNumOfLives.ToString(), guiStyle);
		GUI.Label(new Rect(Screen.width - 190f, 35f, 100f, 50f), intCollectibleCount.ToString() + " / 7", guiStyle);
	}
}
