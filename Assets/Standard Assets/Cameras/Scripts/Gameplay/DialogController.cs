using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{


	public delegate void MethodContainer();

	public event MethodContainer nearby;
	public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void nearNPC()
	{
		nearby();
	}
}
