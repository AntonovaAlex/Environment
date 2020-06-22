using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

	public GameObject mark;
	public GameObject moneyIcon;
	public GameObject score;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerStay()
	{

		if (Input.GetButtonDown("Fire2"))
		{
			mark.SetActive(false);
			moneyIcon.SetActive(false);
			score.SetActive(false);

		}

	}
}
