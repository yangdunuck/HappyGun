using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontbgm : MonoBehaviour
{
    public nobgm mainMusic;
    private void Awake()
    {
        GameObject[] Obj = GameObject.FindGameObjectsWithTag("Music");


        if (Obj.Length > 1) 
        {
            Destroy(this.gameObject);
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        



    }

    
}
