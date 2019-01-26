using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int deathCount;
    public int fireflyCount;

	private void Start()
	{
		
	}

	void Awake ()
    {
        instance = this;
		PlayerPrefs.DeleteAll();

		if (PlayerPrefs.HasKey("DEATH"))
        {
            deathCount = PlayerPrefs.GetInt("DEATH");
        }

        if (PlayerPrefs.HasKey("FIREFLY"))
        {
            fireflyCount = PlayerPrefs.GetInt("FIREFLY");
        }
	}

    public void IncreaseDeath()
    {
        deathCount++;
        SaveCount();
    }

    public void IncreaseFireFly()
    {
        fireflyCount++;
        SaveCount();
    }

    public void SaveCount()
    {
        PlayerPrefs.SetInt("DEATH", deathCount);
        PlayerPrefs.SetInt("FIREFLY", fireflyCount);
    }
}
