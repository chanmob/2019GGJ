using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	private Queue<string> sentences;
	private bool coroutineEnd = true;
	private string dialogue;
	private string beforeSentence;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		nameText.text = dialogue.name;

		sentences.Clear();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count==0)
		{
			EndDialogue();
			return;
		}

		if (coroutineEnd)
		{
			dialogue = sentences.Dequeue();
			beforeSentence = dialogue;
			StopAllCoroutines();
			StartCoroutine(TypeSentence(dialogue)); //print dialogue typely
		}
		else
		{
			StopAllCoroutines();
			dialogueText.text = beforeSentence; //print dialogue directly
			coroutineEnd = true;
		}

	}

	IEnumerator TypeSentence (string sentence)
	{
		coroutineEnd = false;
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		coroutineEnd = true;

	}

	void EndDialogue()
	{
		Debug.Log("End Dialogue");
	}
}
