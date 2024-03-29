﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

	[Range(0f,1f)]
	public float typeDelay;
	public float tutorialEndTime = 5;

	public Text dialogueText;
	public Text guideText;
	public GameObject dialogueTrigger;
	public GameObject player;
	public Image img;
	public string nextTutorial;

	private Queue<string> sentences;
	private string dialogue;
	private string beforeSentence;

	public bool dialogueEnd;
	public bool monsterSpawn;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0)&&dialogueEnd)
		{
			StartCoroutine(FadeText(dialogueText, true));
			StartCoroutine(FadeText(guideText, false));
			StartCoroutine(LoadNextTutorial(tutorialEndTime, nextTutorial));
			monsterSpawn = true;
			dialogueEnd = false;
		}

		if(player.GetComponent<Player>().death)
		{
			StopAllCoroutines();
			StartCoroutine(LoadNextTutorial(tutorialEndTime, nextTutorial));
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

	IEnumerator FadeText(Text text, bool fadeAway)
	{
		if (fadeAway)
		{
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				text.color = new Color(i, i, i, i); //까맣게 투명하게
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i <= 1.1f; i += Time.deltaTime)
			{
				text.color = new Color(i, i, i, i); // 하얗게 not투명하게
				yield return null;
			}
		}
	}

	IEnumerator FadePlayer(bool fadeAway)
	{
		var playerSR = player.GetComponentsInChildren<SpriteRenderer>();

		if (fadeAway)
		{
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				for(int j =0; j < playerSR.Length; j++)
				{
					playerSR[j].color = new Color(i, i, i, i);
				}
				//player.GetComponent<SpriteRenderer>().color = new Color(i, i, i, i); //까맣게 투명하게
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i <= 1.1f; i += Time.deltaTime)
			{
				for (int j = 0; j < playerSR.Length; j++)
				{
					playerSR[j].color = new Color(i, i, i, i);
				}
				//player.GetComponent<SpriteRenderer>().color = new Color(i, i, i, i); // 하얗게 not투명하게
				yield return null;
			}
		}
	}

	IEnumerator LoadNextTutorial(float time, string tutorialNumber)
	{
		yield return new WaitForSecondsRealtime(time);
		img.gameObject.SetActive(true);
		for (float i = 0; i <= 1.1f; i += Time.deltaTime)
		{
			img.color = new Color(0, 0, 0, i);
			yield return null;
		}

		SceneManager.LoadScene(tutorialNumber);
	}
}
