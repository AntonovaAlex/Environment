using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest001Take : MonoBehaviour
{
	public float theDistance;
	//public GameObject actionDisplay;
	public GameObject actionText;
	public GameObject uiQuest;
	public GameObject thePlayer;
	public GameObject noticeCam;

	public int num;

	void Update()
	{
		theDistance = PlayerCasting.distanceFromTarget;
	}

	void OnMouseOver()
	{
		if (theDistance <= num)
		{
			//actionDisplay.SetActive(true);
			actionText.SetActive(true);
		}

		if (Input.GetButtonDown("Submit"))
		{
			if (theDistance <= num)
			{
				//actionDisplay.SetActive(false);
				actionText.SetActive(false);
				uiQuest.SetActive(true);
				noticeCam.SetActive(true);
				thePlayer.SetActive(false);
			}
		}
	}

	void OnMouseExit()
	{
		//actionDisplay.SetActive(false);
		actionText.SetActive(false);
	}

	//void onTriggerStay()
	//{
	//	actionText.SetActive(true);
	//}
}
