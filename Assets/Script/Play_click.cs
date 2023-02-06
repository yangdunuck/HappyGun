using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play_click : MonoBehaviour
{
    public void SceneChange()
    {
        Debug.Log("asdf");
        SceneManager.LoadScene(1);
    }
}
