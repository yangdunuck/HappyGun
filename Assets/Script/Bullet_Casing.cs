using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Casing : MonoBehaviour
{
    Rigidbody2D rigid;
    public Transform pos;
    [SerializeField]
    float power;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector3 dir = (pos.position - transform.position).normalized;
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 100);    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("borderBullet"))
        {
            Destroy(gameObject);
        }    
    }
}
