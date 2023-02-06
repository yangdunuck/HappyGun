using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    public Player player;
    public int DMG;
    public ParticleSystem effect;
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("borderBullet"))
            Destroy(gameObject);
        else if (collision.CompareTag("Enemy"))
        {
            //if(player.bulletGauge < 100)
            //player.bulletGauge++;
            Destroy(gameObject);
            

        }
    }
}
