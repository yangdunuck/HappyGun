using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jinu_Geshaki : MonoBehaviour
{
    public Text HiScore;
    void Awake()
    {
        int hiScore;
        if (!PlayerPrefs.HasKey("HiScore"))
            hiScore = 0;
        else
            hiScore = PlayerPrefs.GetInt("HiScore");
        HiScore.text = (Mathf.FloorToInt(hiScore) / 60).ToString("D2") + ":" + (Mathf.FloorToInt(hiScore) % 60).ToString("D2");
    }
}
