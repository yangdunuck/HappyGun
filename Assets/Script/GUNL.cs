using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUNL : MonoBehaviour
{
    public GameObject gun;

    private void Update()
    {
        gamma();
    }

    void gamma()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;
        gun.gameObject.SetActive(true);
    }
}
