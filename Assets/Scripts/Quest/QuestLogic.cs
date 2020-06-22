using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogic : MonoBehaviour
{
	public GameObject quest01;
	public GameObject quest02;
	public GameObject quest03;

	public GlobalScore money;

	public int progress = 0;

	public bool begin = false;

	public GameObject endTrig;
	

	public CoinCounter method;

	void Start()
	{
		//money = GetComponent<GlobalScore>();
		Sub();
	}

	public void Questing()
	{
		if (progress == 0 && quest01 != null && begin == true)
		{
			progress++;
			quest01.SetActive(false);
			quest02.SetActive(true);
		}

		if (money.currentScore == 26)
		{
			progress++;
			quest02.SetActive(false);
		}

		if (progress == 2)
		{
			quest03.SetActive(true);
			endTrig.SetActive(true);
		}

		//if (progress >= 2 && begin == false)
		//{
		//	mark.SetActive(false);
		//}

	}

	public void Sub()
	{
		method.onCount += Questing; //Value does not fall within the expected range
	}
}
