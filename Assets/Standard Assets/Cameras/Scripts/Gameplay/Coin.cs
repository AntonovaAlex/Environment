using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	
	public AudioSource collectSound;

	public GameObject counter;
	private CoinCounter getCount;

	public void Start()
	{
		getCount = counter.GetComponent<CoinCounter>();
	}

	public void OnTriggerEnter()
	{
		collectSound.Play();
		getCount.RemoveItem();
		Destroy(gameObject);
	}
}
