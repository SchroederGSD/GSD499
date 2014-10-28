using UnityEngine;
using System.Collections;

public class scr_EndGame : MonoBehaviour
{
	void OnTriggerEnter()
	{
		Application.LoadLevel("Test_Scene_Maze");
	}
}
