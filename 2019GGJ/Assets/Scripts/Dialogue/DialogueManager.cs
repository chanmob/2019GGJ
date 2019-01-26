﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	[Range(0f,1f)]
	public float typeDelay;

	public Text dialogueText;
	public GameObject dialogueTrigger;
	public GameObject player;

	private Queue<string> sentences;
	private string dialogue;
	private string beforeSentence;
	private bool dialogueEnd;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0)&&dialogueEnd)
		{
			StartCoroutine(FadeText(true));
			dialogueEnd = false;
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{
		dialogueTrigger.SetActive(false);
		dialogueText.text = "";
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

		dialogue = sentences.Dequeue();
		beforeSentence = dialogue;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(dialogue)); //print dialogue typely

	}

	IEnumerator TypeSentence (string sentence)
	{

		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typeDelay);
		}
		yield return new WaitForSeconds(0.5f);
		dialogueText.text += "\n\n";
		DisplayNextSentence();

	}

	void EndDialogue()
	{
		dialogueEnd = true;
		player.SetActive(true);
		StartCoroutine(FadePlayer(false));
	}

	IEnumerator FadeText(bool fadeAway)
	{
		if (fadeAway)
		{
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				dialogueText.color = new Color(i, i, i, i); //까맣게 투명하게
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i <= 1.1f; i += Time.deltaTime)
			{
				dialogueText.color = new Color(i, i, i, i); // 하얗게 not투명하게
				yield return null;
			}
		}
	}

	IEnumerator FadePlayer(bool fadeAway)
	{
		if (fadeAway)
		{
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				player.GetComponent<SpriteRenderer>().color = new Color(i, i, i, i); //까맣게 투명하게
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i <= 1.1f; i += Time.deltaTime)
			{
				player.GetComponent<SpriteRenderer>().color = new Color(i, i, i, i); // 하얗게 not투명하게
				yield return null;
			}
		}
	}
}
