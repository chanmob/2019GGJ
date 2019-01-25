using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonManager : MonoBehaviour {

	public GameObject saveDataPanel;
	public GameObject settingPanel;
	public GameObject creditsPanel;
	public GameObject exitPanel;

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
}
