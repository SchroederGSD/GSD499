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
	public Texture txtBattery;
	public Texture txtDarkGreen;
	public Texture txtLightGreen;
	public Texture txtYellow;
	public Texture txtRed;
	private Texture txtCurrMeter;

	private scr_GameControl scrGameControl;

	private const int MeterLength = 125;
	private const int MeterStart = 206;
	private int intCurrLength = 0;
	private int intMeterAdjust = 0;
	private int intNumOfLives = 0;
	private int intCollectibleCount = 0;
	private float fltPercentLeft = 100f;

	//******************************************************************************
	//	Awake Method
	//******************************************************************************
	void Awake()
	{
		scrGameControl = GetComponent<scr_GameControl>();
		intCurrLength = MeterLength;
		intMeterAdjust = MeterStart;
	}
	//******************************************************************************
	//	Update Method
	//******************************************************************************
	void Update()
	{
		intNumOfLives = scrGameControl.GetNumOfLives();
		intCollectibleCount = scrGameControl.GetCollectibleCount();
		fltPercentLeft = scrGameControl.GetBatteryLife();

		if (fltPercentLeft > 0)
			FindMeterLength();
	}
	//******************************************************************************
	//	On Gui Method
	//******************************************************************************
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(50f, 10f, 146f, 75f), txtLives);
		GUI.DrawTexture(new Rect(Screen.width - 205f, 10f, 155f, 75f), txtDiamonds);
		GUI.DrawTexture (new Rect (Screen.width - 231f, Screen.height - 74f, 160f, 60f), txtBattery);
		GUI.DrawTexture (new Rect (Screen.width - intMeterAdjust, Screen.height - 66f, intCurrLength, 44f), txtCurrMeter);
		GUI.Label(new Rect(150f, 38f, 100f, 50f), intNumOfLives.ToString(), guiStyle);
		GUI.Label(new Rect(Screen.width - 190f, 35f, 100f, 50f), intCollectibleCount.ToString() + " / 7", guiStyle);
	}

	void FindMeterLength()
	{
		float fltTemp = 0f;
		
		if (fltPercentLeft > 75f)
			txtCurrMeter = txtDarkGreen;
		else if (fltPercentLeft <= 75f && fltPercentLeft > 50f)
			txtCurrMeter = txtLightGreen;
		else if (fltPercentLeft <= 50f && fltPercentLeft > 25f)
			txtCurrMeter = txtYellow;
		else if (fltPercentLeft <= 25f)
			txtCurrMeter = txtRed;
		
		fltTemp = MeterLength * fltPercentLeft * 0.01f;
		intCurrLength = Mathf.FloorToInt(fltTemp);
		intMeterAdjust = MeterStart - (MeterLength - intCurrLength);
	}
}
