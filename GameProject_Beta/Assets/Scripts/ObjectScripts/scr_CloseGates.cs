using UnityEngine;
using System.Collections;

public class scr_CloseGates : MonoBehaviour {

	GameObject objStartGate = null;
	scr_Animation[] scrAnimation = null;

	void Awake()
	{
		objStartGate = GameObject.Find("obj_StartGate");
	}
	void OnTriggerEnter()
	{
		scrAnimation = objStartGate.GetComponentsInChildren<scr_Animation>();
		foreach (scr_Animation anim in scrAnimation)
		{
			anim.CloseGate();
		}
		gameObject.SetActive(false);
	}
}
