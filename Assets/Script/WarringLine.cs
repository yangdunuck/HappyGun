using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarringLine : MonoBehaviour
{
    public GameManager manager;
    public GameObject Laser;
    void Awake()
    {
        Invoke("ReplaceLaser",1);
    }
    void ReplaceLaser()
    {
        manager.SfxPlay(GameManager.Sfx.Laser);
        Instantiate(Laser, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
