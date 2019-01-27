using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {


	[Range(0f, 1f)]
	public float typeDelay;

	private int deathCount;
	private int fireflyCount;
	[SerializeField]
	private AudioSource audiosource;

	public Text endingName;
	public Text endingText;
	public Image img;
	public GameObject point;

	[TextArea]
	public string[] happyEndingText;
	[TextArea]
	public string[] normalEndingText;
	[TextArea]
	public string[] badEndingText;

	private string[] endingStory;

	private int index;


	[SerializeField]
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
					Debug.Log("end"); StartCoroutine(FadeImage(false));
					return;
				}
				StartCoroutine(TypeSentence(endingStory[index]));
			}
		}

		if(isTyping)
		{
			point.SetActive(false);
		}
		else
		{
			point.SetActive(true);
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
		isTyping = false;
	}

	IEnumerator FadeImage(bool fadeAway)
	{
		img.gameObject.SetActive(true);
		if (fadeAway)
		{
			for (float i = 1; i >= 0; i -= Time.deltaTime)
			{
				img.color = new Color(0, 0, 0, i);
				yield return null;
			}
		}
		else
		{
			for (float i = 0; i <= 1.1f; i += Time.deltaTime)
			{
				img.color = new Color(0, 0, 0, i);
				yield return null;
			}

			SceneManager.LoadScene("Main");
		}
	}
}
