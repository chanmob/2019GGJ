using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {


	public Dialogue dialogue;

	void FixedUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("첫번째 터치");
			TriggerDialogue();
		}
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
