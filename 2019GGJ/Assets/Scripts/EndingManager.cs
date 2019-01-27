using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour {


	[Range(0f, 1f)]
	public float typeDelay;

	private int deathCount;
	private int fireflyCount;
	[SerializeField]
	private AudioSource audiosource;

	public Text endingName;
	public Text endingText;

	[TextArea]
	public string[] happyEndingText;
	[TextArea]
	public string[] normalEndingText;
	[TextArea]
	public string[] badEndingText;

	private string[] endingStory;

	private int index;

	private bool isTyping;

	public AudioClip endingBGM;
	public AudioClip happyEndingBGM;

	void Awake()
	{
		audiosource = GetComponent<AudioSource>();

		if (PlayerPrefs.HasKey("DEATH"))
		{
			deathCount = PlayerPrefs.GetInt("DEATH");
		}

		if (PlayerPrefs.HasKey("FIREFLY"))
		{
			fireflyCount = PlayerPrefs.GetInt("FIREFLY");
		}

		if(deathCount == 0 && fireflyCount == 3) //Happy Ending
		{
			audiosource.clip = happyEndingBGM;
			audiosource.Play();
			endingName.text = "Happy Ending";
			endingStory = happyEndingText;
			StartCoroutine(TypeSentence(endingStory[index]));

		}
		else if (deathCount >= 10 || fireflyCount == 0) //Bad Ending
		{
			audiosource.clip = endingBGM;
			audiosource.Play();
			endingName.text = "Sad Ending";
			endingStory = badEndingText;
			StartCoroutine(TypeSentence(endingStory[index]));
		}
		else  //Normal Ending
		{
			audiosource.clip = endingBGM;
			audiosource.Play();
			endingName.text = "Normal Ending";
			endingStory = normalEndingText;
			StartCoroutine(TypeSentence(endingStory[index]));
		}
	}

	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			if (isTyping)
			{
				isTyping = false;
				StopAllCoroutines();
				endingText.text = endingStory[index];
			}
			else
			{
				index++;
				if(index >= endingStory.Length)
				{
					return;
				}
				StartCoroutine(TypeSentence(endingStory[index]));
			}
		}
	}

	IEnumerator TypeSentence(string sentence)
	{
		endingText.text = "";
		isTyping = true;

		foreach (char letter in sentence.ToCharArray())
		{
			endingText.text += letter;
			yield return new WaitForSeconds(typeDelay);
		}

		yield return new WaitForSeconds(0.5f);
		isTyping = false;
	}
}
