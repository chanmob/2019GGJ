using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour {

	public GameObject settingPanel;

	public void PressRestartBtn()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void PressMainBtn()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Main");
	}

	public void PressSettingBtn()
	{
		settingPanel.SetActive(true);
		Time.timeScale = 0;
	}

	public void PressExitSettingBtn()
	{
		settingPanel.SetActive(false);
		Time.timeScale = 1;
	}
}
