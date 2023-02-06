using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nobgm : MonoBehaviour
{
    public void DeleteBGM()
    {
        gameObject.SetActive(false);
    }
    public void BGM()
    {
        gameObject.SetActive(false);
    }
    /*public void gameOver()
    {
        GameObject[] Obj = GameObject.FindGameObjectsWithTag("Player");
        if (Obj.Length <= 1)
        {
            Destroy(this.gameObject);
        }
    }*/


}
