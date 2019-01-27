using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public void AdjustSound(Slider slider)
    {
        AudioListener.volume = slider.value;
    }
}
