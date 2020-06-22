using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trickNPC : MonoBehaviour
{
	public GameObject pressKey;
	public GameObject icon;
	public AudioSource collectSound;

	public DialogueTrigger trig;
	public DialogueManager man;
	bool stay = false;

	public KnightController controller;
	public GameObject noticeCam;

	public IconManager avatar;

	//public QuestLogic logic;

	public void Start()
	{
		
	}

	public void OnTriggerEnter()
	{
		pressKey.SetActive(true);
	}

	public void OnTriggerStay()
	{


		if (Input.GetButtonDown("Fire2"))
		{
			icon.SetActive(true);
			//collectSound.Play();
			pressKey.SetActive(false);
			trig.TriggerDialogue();
			stay = true;
			controller.enabled = false;
			noticeCam.SetActive(true);
			avatar.SetIcon();

		}
		Change();

	}

	public void OnTriggerExit()
	{
		pressKey.SetActive(false);
	}

	public void Change()
	{
		if (man.done == true)
		{
			icon.SetActive(false);
			stay = false;
			controller.enabled = true;
			noticeCam.SetActive(false);
			man.done = false;
			avatar.RemoveIcon();
			//logic.begin = true;
			//logic.Questing();
		}
	}
}
