using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class click : MonoBehaviour
{
    public void SceneChange()
    {
        Debug.Log("asdf");
        SceneManager.LoadScene(2);
    }
}
