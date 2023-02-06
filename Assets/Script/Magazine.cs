using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField]
    Transform magazinePosition;
    Rigidbody2D rigid;
    bool Spin = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Spin)
            transform.Rotate(Vector3.back * Time.deltaTime * 100);
        else
        {
            if (magazinePosition != null)
            {
                transform.position = magazinePosition.transform.position;
                transform.rotation = magazinePosition.rotation;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void Get_magazine()
    {
        rigid.gravityScale = 0;
        Spin = false;
    }
    public void Take_apart_magazine()
    {
        rigid.gravityScale = 2;
        Spin = true;
        Invoke("disableObject",1.9f);
    }
    void disableObject()
    {
        gameObject.SetActive(false);
    }
}