using UnityEngine;
using System.Collections;

public class scr_Dialogue : MonoBehaviour
{
	public Texture txtDialogueBox;
	public GUIStyle styleDialogue;
	public GUIStyle styleContinue;
	
	private float fltDialogueSpeed = 0.05f;
	private float fltDialogueTime = 0f;
	private bool blnFinishDialogue = false;
	private bool blnDialogueDone = true;
	private bool blnPlayDialogue = false;

	private int intDialogueGroup = 0;
	private int intDialogueLength = 0;
	private int intStringIndex = 0;
	
	private string strCurrentDialogue = "";
	private string strTemp = "";
	
	private string strDialogue01 = "Hello, little one.  Are you lost?  You must be lost if you are here.\n" +
								   "This is a place where only lost souls come.  If you wish to go home,\n" +
								   "you will need navigate through this maze and reach the end.";
	private string strDialogue02 = "However, it will not be as simple as you may think.  You will first\n" +
								   "need to find seven magical diamonds to open the gates at the end\n" +
								   "of the maze.";
	private string strDialogue03 = "Beware, this maze is haunted by ghosts, who will attack you if they\n" +
								   "see you.  If you are caught three times, you will forever be trapped\n" +
								   "in this world and become a wandering ghost yourself.";
	private string strDialogue04 = "Don't think me to be cruel for I have something for you.  Just over\n" +
								   "there, you will see a magical flashlight ('F' key) that will help light\n" +
								   "the way and banish the ghost for a short time.";
	private string strDialogue05 = "It has a limited battery life, so you will need to be careful about\n" +
								   "using it.  Well, I have talked long enough.  It is now time for you\n" +
								   "to be on your way.  Good luck. Hahahaha...\n";

	
	private void Update()
	{
		if (!blnDialogueDone)
			DialogueControl();
	}

	private void DialogueControl()
	{
		if (Input.GetKeyUp("enter") || Input.GetKeyDown("return"))
		{
			//print ("Enter was pressed");
			if (blnPlayDialogue)
				blnFinishDialogue = true;
			else if (!blnPlayDialogue)
			{
				strTemp = "";
				GetDialogue();
				blnPlayDialogue = true;
			}
		}
		if (blnPlayDialogue)
		{
			if (fltDialogueTime > fltDialogueSpeed)
			{
				TextWriter();
				fltDialogueTime = 0f;
			}
			else
				fltDialogueTime += Time.deltaTime;
		}
	}

	private void GetDialogue()
	{
		switch (intDialogueGroup)
		{
			case 1:
				strCurrentDialogue = strDialogue01;
				break;
			case 2:
				strCurrentDialogue = strDialogue02;
				break;
			case 3:
				strCurrentDialogue = strDialogue03;
				break;
			case 4:
				strCurrentDialogue = strDialogue04;
				break;
			case 5:
				strCurrentDialogue = strDialogue05;
				break;
			default:
				strCurrentDialogue = "";
				blnDialogueDone = true;
				break;
		}
	}

	private void TextWriter()
	{
		intDialogueLength = strCurrentDialogue.Length;

		if (blnFinishDialogue)
		{
			strTemp = strCurrentDialogue;
			intStringIndex = 0;
			intDialogueGroup++;
			blnPlayDialogue = false;
			blnFinishDialogue = false;
		}
		else if (intStringIndex < intDialogueLength)
		{
			strTemp += strCurrentDialogue[intStringIndex];
			intStringIndex++;
		}
		else
		{
			intStringIndex = 0;
			intDialogueGroup++;
			blnPlayDialogue = false;
		}
	}

	private void OnGUI()
	{
		if (!blnPlayDialogue)
			styleContinue.normal.textColor = Color.green;
		else
			styleContinue.normal.textColor = Color.white;

		if (!blnDialogueDone)
		{
			GUI.Label(new Rect(Screen.width * 0.5f - 274f, Screen.height - 140f, 550f, 130f), txtDialogueBox);
			GUI.Label(new Rect(Screen.width * 0.5f - 249f, Screen.height - 115f, 530f, 110f), strTemp, styleDialogue);
			GUI.Label(new Rect(Screen.width * 0.5f + 185f, Screen.height - 50f, 100f, 50f), "Press Enter", styleContinue);
		}
	}

	public void StartDialogue()
	{
		blnDialogueDone = false;
		blnPlayDialogue = true;
		intDialogueGroup = 1;
		GetDialogue();
	}

	public bool GetDialogueHasEnded()
	{	return blnDialogueDone;		}
}
