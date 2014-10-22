using UnityEngine;
using System.Collections;

public class scr_PumpkinLight : MonoBehaviour {

	//***********************************
	//	Variable
	//***********************************
	private Light lightPumpkin;
	private float fltTimer = 0f;
	private float fltRate = 0.1f;

	//*************************************************************************
	//	Awake Method - Awake is called when the script instance is being loaded
	//*************************************************************************
	void Awake()
	{
		lightPumpkin = GetComponentInChildren<Light>();
	}
	
	//*************************************************************************
	//	Update Method
	//*************************************************************************
	void Update ()
	{
		if (fltTimer >= fltRate)
		{
			Flicker();
			fltTimer = 0f;
		}
		else
			fltTimer += Time.deltaTime;

	}

	void Flicker()
	{
		float fltRandom = 0f;
		float fltIntensity = 0f;

		do{
			fltRandom = Random.Range(1.0f,2.0f);
			fltIntensity = fltRandom * 4f;
		} while (Mathf.Abs(lightPumpkin.intensity - fltIntensity) > 0.5f && Mathf.Abs(lightPumpkin.intensity - fltIntensity) < 1.25f);

		lightPumpkin.intensity = fltIntensity;
	}
}
