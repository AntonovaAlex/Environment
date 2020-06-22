using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	public Text nameText;
	public Text dialogueText;

	


	public bool done = false;

	private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
		sentences = new Queue<string>();
		
	}

	public void StartDialogue (Dialogue dialogue)
	{
		

		Debug.Log("Starting concersation with " + dialogue.name);
		done = false;

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
		
	}
	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		//done = false; 

		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
		Debug.Log(sentence);
		Debug.Log(done);
	}

	public void EndDialogue()
	{
		done = true;
		Debug.Log("End of conversation");
		Debug.Log(done);
	}


}
