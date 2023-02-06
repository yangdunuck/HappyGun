using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Newtonsoft.Json.Serialization;
using UnityEngine.SubsystemsImplementation;

public class RankManager : MonoBehaviour
{
    public int BestRound;
    void Awake()
    {
        var obj = FindObjectsOfType<RankManager>();
        if (obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        if (!PlayerPrefs.HasKey("HiScore"))
            BestRound = 0;
        else
            BestRound = PlayerPrefs.GetInt("HiScore");
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.S))
        {
            BestRound = 0;
            PlayerPrefs.DeleteAll();
        }    
    }
    public void SetHiScore()
    {
        PlayerPrefs.SetInt("HiScore",BestRound);
        PlayerPrefs.Save();
    }
}
