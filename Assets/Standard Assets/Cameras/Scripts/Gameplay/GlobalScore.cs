using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalScore : MonoBehaviour	
{
	public GameObject scoreBox;
	public int currentScore = 0;

	GlobalScore score;
	CoinCounter method;

	public GameObject coinCount;

	void Start()
	{
		method = coinCount.GetComponent<CoinCounter>();
		Sub();
	}

	public void UpdateScore()
	{
		currentScore += 1;
		scoreBox.GetComponent<Text>().text = "" + currentScore;
	}
	public void Sub()
	{
		method.onCount += UpdateScore; //Value does not fall within the expected range
	}

}
