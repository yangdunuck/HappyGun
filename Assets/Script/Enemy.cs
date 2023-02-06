using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem Effect;
    public int knockback_Power;
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Rigidbody2D rigid;
    private Transform player;
    public GameManager manager;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    int health;
    [SerializeField]
    private GameObject explosionPrefab;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(Time.timeScale != 0) {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Vector3 knockback = (transform.position - collision.transform.position).normalized;
            if (knockback == Vector3.zero)
                transform.position += Vector3.right * Random.Range(-1f, 1f);
            rigid.AddForce(knockback * knockback_Power, ForceMode2D.Impulse);
            Invoke("Stop_knockback",0.5f);
        }
        if (collision.CompareTag("PlayerBullet"))
        {
            OnDamage(collision);
        }
    }
    void OnDamage(Collider2D bullet)
    {
        spriteRenderer.color = new Color32(255,173,173,255);
        health -= bullet.GetComponent<PlayerBullet>().DMG;
        if (health <= 0)
        {
            Ondie();
        }
        Invoke("ResetSprite",0.1f);
    }
    void ResetSprite()
    {
        spriteRenderer.color = new Color32(255,59,59,255);
    }
    void Stop_knockback()
    {
        rigid.velocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    public void Ondie()
    {
       

        manager.curEnemys--;

        GameObject effct = Instantiate(explosionPrefab);
        effct.transform.position = transform.position;
        Destroy(gameObject);
    }

    




}
