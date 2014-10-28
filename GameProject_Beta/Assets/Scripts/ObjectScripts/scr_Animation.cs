using UnityEngine;
using System.Collections;

public class scr_Animation : MonoBehaviour
{
	public string strOpen;
	public string strClose;
	
	public void OpenGate()
	{
		animation.Play(strOpen);
	}
	public void CloseGate()
	{
		animation.Play(strClose);
	}
}
