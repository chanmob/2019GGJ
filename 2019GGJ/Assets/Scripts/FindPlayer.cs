using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour {

	public MonsterSound monsterScript;
	public DialogueManager dialogueScript;
	
	// Update is called once per frame
	void Update () {
		
		if(dialogueScript.dialogueEnd)
		{
			monsterScript.player = GameObject.FindGameObjectWithTag("Player");
		}
	}
}
