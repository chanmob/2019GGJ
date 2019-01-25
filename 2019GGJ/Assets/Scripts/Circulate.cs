using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circulate : MonoBehaviour {

	public float angularSpeed;
	public Image img;
	public GameObject gameover;
	public GameObject settingBtn;
	private float rotationAngle;


	private void FixedUpdate()
	{
		rotationAngle = transform.eulerAngles.z;

		if (rotationAngle > 265)
		{
			Debug.Log("player die");
			StartCoroutine(FadeImage(false));
		}
		else
		{
			transform.Rotate(0, 0, Time.deltaTime + angularSpeed);
		}
	}
	

	IEnumerator FadeImage(bool fadeAway)
	{
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
			Time.timeScale = 0;
			settingBtn.SetActive(false);
			gameover.SetActive(true);
		}
	}
}
