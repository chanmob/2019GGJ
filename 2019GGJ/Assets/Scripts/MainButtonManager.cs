using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour {

	public GameObject saveDataPanel;
	public GameObject settingPanel;
	public GameObject creditsPanel;
	public GameObject exitPanel;

	public Image img;

	public void PressGameStartBtn()
	{
		saveDataPanel.SetActive(true);
	}

	public void PressSettingsBtn()
	{
		settingPanel.SetActive(true);
	}

	public void PressCreditstBtn()
	{
		creditsPanel.SetActive(true);
	}

	public void PressExitBtn()
	{
		exitPanel.SetActive(true);
	}

	public void PressExitGameStartBtn()
	{
		saveDataPanel.SetActive(false);
	}

	public void PressExitSettingsBtn()
	{
		settingPanel.SetActive(false);
	}

	public void PressExitCreditsBtn()
	{
		creditsPanel.SetActive(false);
	}

	public void PressYesBtn()
	{
		Application.Quit();
	}

	public void PressNoBtn()
	{
		exitPanel.SetActive(false);
	}

	public void PressNewGameBtn()
	{
		img.gameObject.SetActive(true);
		StartCoroutine(FadeImage(false));
	}

	IEnumerator FadeImage(bool fadeAway)
	{
		Debug.Log("작동1");
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
			Debug.Log("작동2");
			SceneManager.LoadScene("Tutorial");
		}
	}

}
