using UnityEngine;
using System.Collections;

public class scr_EndGame : MonoBehaviour
{
	void OnTriggerEnter()
	{
		Application.LoadLevel("Scene_Credits");
	}
}
