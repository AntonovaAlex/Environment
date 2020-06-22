using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
	private GameObject[] coinsPool;
	int coinsCount;

	public delegate void MethodContainer();
	public event MethodContainer onCount;

	void Start()
    {
		
	}

	public void GetItems()
	{
		coinsPool = GameObject.FindGameObjectsWithTag("Coin");
		coinsCount = coinsPool.Length;
		Debug.Log(coinsCount);
	}

	public void RemoveItem()
	{
		onCount(); //Object reference not set to an instance of an object
	} 
}
