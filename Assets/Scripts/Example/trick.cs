using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trick : MonoBehaviour
{
	public GameObject pressKey;
	public GameObject icon;
	public AudioSource collectSound;

	public DialogueTrigger trig;
	public DialogueManager man;
	bool stay = false;

	public KnightController controller;
	public GameObject noticeCam;

	//public GameObject nextStep;

	public QuestLogic logic;

	public IconManager avatar;

	//public int id;

	//public AudioSource collectSound;

	//public GameObject counter;
	//private CoinCounter getCount;

	public void Start()
	{
		//getCount = counter.GetComponent<CoinCounter>();
		//controller = GetComponent<KnightController>();
	}

	public void OnTriggerEnter()
	{
		//collectSound.Play();
		pressKey.SetActive(true);
		
		//getCount.RemoveItem();
		//Destroy(gameObject);

	}

	public void OnTriggerStay()
	{
		

		if (Input.GetButtonDown("Fire2"))
		{

			//actionDisplay.SetActive(false);


			icon.SetActive(true);
			collectSound.Play();
			pressKey.SetActive(false);
			trig.TriggerDialogue();
			stay = true;
			controller.enabled = false;
			noticeCam.SetActive(true);

			avatar.SetIcon();

		}
		//if (Input.GetButtonDown("Fire2") && stay == true)
		//{
		//man.DisplayNextSentence();
		//}
		Change();

	}

	public void OnTriggerExit()
	{
		//collectSound.Play();
		pressKey.SetActive(false);
		//getCount.RemoveItem();
		//Destroy(gameObject);
		//Change();

	}

	public void Change()
	{
		if (man.done == true)
		{
			icon.SetActive(false);
			//collectSound.Play();
			//manga.SetActive(false);
			stay = false;
			controller.enabled = true;
			noticeCam.SetActive(false);
			//nextStep.SetActive(true);
			man.done = false;
			logic.begin = true;
			logic.Questing();
			//gameObject.SetActive(false);
			avatar.RemoveIcon();
			//Destroy(GameObject);

		}
	}
}
