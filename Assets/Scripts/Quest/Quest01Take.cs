using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest01Take : MonoBehaviour
{
	public GameObject textEnter;
	public GameObject dialogeUI;
	
	public GameObject noticeCam;

	public DialogueTrigger trig;
	public DialogueManager man;
	bool stay = false;
	int count = 1;
	//bool cont = false;


	public void Start()
	{
		//getCount = counter.GetComponent<CoinCounter>();
	}

	public void OnTriggerEnter()
	{
		textEnter.SetActive(true);
	}

	public void OnTriggerStay()
	{
		if (Input.GetButtonDown("Submit") && stay == false && count == 1 )
		{
			dialogeUI.SetActive(true);
			noticeCam.SetActive(true);
			//collectSound.Play();
			textEnter.SetActive(false);

			trig.TriggerDialogue();
			stay = true;
			count++;
			Debug.Log("count = " + count);

		}

		if (Input.GetButtonDown("Fire2") && stay == true)
		{
			man.DisplayNextSentence();
		}
	}

	public void OnTriggerExit()
	{
		//collectSound.Play();
		textEnter.SetActive(false);
		//getCount.RemoveItem();
		//Destroy(gameObject);
	}

	
}
