using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour {

	private int deathCount;
	private int fireflyCount;

	public Text endingName;
	public Text endingText;
	public string happyEndingText;
	public string normalEndingText;
	public string badEndingText;

	void Awake()
	{
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
			endingName.text = "Happy Ending";
			endingText.text = happyEndingText;
		}
		else if (deathCount >= 10 || fireflyCount == 0) //Bad Ending
		{
			endingName.text = "Bad Ending";
			endingText.text = badEndingText;
		}
		else  //Normal Ending
		{
			endingName.text = "Normal Ending";
			endingText.text = normalEndingText;
		}
	}

	void Update () {
		
	}
}
