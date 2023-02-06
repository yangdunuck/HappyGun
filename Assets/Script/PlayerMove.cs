using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    public Text Score;
    public int health;
    public Text OverMasage;
    public Text retryMasage;
    public Button HomeMasage;
    public Image image;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isTouchTop;
    public bool isTouchBottom;


    float score;
    float moveX, moveY;

    [Header("이동속도 조절")]
    [SerializeField]
    [Range(1f, 30f)] float moveSpeed = 20f;

    void Update()
    {
        score += Time.deltaTime;
        moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        if ((isTouchRight && moveX > 0) || (isTouchLeft && moveX < 0))
           moveX = 0;
        moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        if ((isTouchTop && moveY > 0) || (isTouchBottom && moveY < 0))
           moveY = 0;

        transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);
    }

    
    public void gameOver()
    {
        Time.timeScale = 0;
        image.gameObject.SetActive(true);
        OverMasage.gameObject.SetActive(true);
        HomeMasage.gameObject.SetActive(true);
        retryMasage.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        Score.text = (Mathf.FloorToInt(score) / 60).ToString("D2") + ":" + (Mathf.FloorToInt(score) % 60).ToString ("D2");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet") 
        {
            gameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "border")
        {
            switch (collision.gameObject.name) {
                case "Left":
                    isTouchLeft = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
            }
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "border")
        {
            switch (collision.gameObject.name)
            {
                case "Left":
                    isTouchLeft = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;

            }
        }
    }
}
