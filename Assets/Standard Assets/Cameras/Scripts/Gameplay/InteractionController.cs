using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject pressEnterText;
	public bool triggering = false;



	public AudioSource collectSound;

	//public GameObject counter;
	//private CoinCounter getCount;

	public void Start()
	{
		//getCount = counter.GetComponent<CoinCounter>();
	}

	//void Update()
	//{
	//	if (triggering == true)
	//	{
	//		pressEnterText.SetActive(true);
	//	}

	//	if (triggering == false)
	//	{
	//		pressEnterText.SetActive(false);
	//	}
	//}

	public void OnTriggerEnter()
	{
		//collectSound.Play();
		//getCount.RemoveItem();
		//Destroy(gameObject);
		
		triggering = true;
	}

	public void onTriggerExit()
	{
		triggering = false;
	}
}