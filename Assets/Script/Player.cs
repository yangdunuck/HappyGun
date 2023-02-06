using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    private Gun currentGun;
    public ParticleSystem muzzleFlash;
    private AudioSource audioSource;
    public AudioClip fire_Sound;
    public GameObject WarringLine;
    public GameObject Laser_1;
    public RankManager rank;
    public Transform gun;
    public GameManager gameManager;
    //public int bulletGauge;
    public GameObject BGM;
    public Text BulletGauge;
    public int health;
    public Text OverMasage;
    public Text retryMasage;
    public Button HomeMasage;
    public Image image;
    public Image Over;
    public bool isTouchRight;
    public bool isTouchLeft;
    public bool isTouchTop;
    public bool isTouchBottom;
    public float score;
    bool isReLoading = false;
    float moveX, moveY;
    float curDelay;
    Vector3 mousePos;
    [SerializeField]
    float shootPower;
    [SerializeField]
    float bulletDelay;
    [SerializeField]
    GameObject playerBasicBullet;
    [SerializeField]
    ParticleSystem exp;
    [SerializeField]
    AudioSource epx;
    [SerializeField]
    AudioSource Dah;
    [SerializeField]
    float dashPower;
    [SerializeField]
    Text dashDelayUI;
    [SerializeField]
    GameObject dashIconBlack;
    float curDashDelay = 0;
    int magazine = 30;
    [SerializeField]
    Text MagazineUI;
    [SerializeField]
    AudioSource rop;
    [SerializeField]
    GameObject magazine_Objt;
    [Header("이동속도 조절")]
    [SerializeField]
    [Range(1f, 30f)] float moveSpeed = 20f;
    Magazine mgScript;
    [SerializeField]
    Transform Bullet_Casing_Pos;
    [SerializeField]
    Transform Bullet_Casing_Shoot_Pos;
    [SerializeField]
    GameObject Bullet_Casing_Pre;
    void Awake()
    {
        mgScript = magazine_Objt.GetComponent<Magazine>();
        mgScript.Take_apart_magazine();
        rigid = GetComponent<Rigidbody2D>();
        rank = GameObject.Find("RankManager").GetComponent<RankManager>();
        ReLoading();
    }
    /*void UltimateAttack()
    {
        if (bulletGauge < 100)
            return;
        if (Input.GetKey(KeyCode.Space))
        {
            WarringLine.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            WarringLine.gameObject.SetActive(false);
            Laser_1.gameObject.SetActive(true);
            bulletGauge = 0;
        }
    }*/
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ReLoading();
        Move();
        Dash();
        Basic_Shoot();
        //UltimateAttack();
        UpdateDashUI();
        curDelay += Time.deltaTime;
    }
    void UpdateDashUI()
    {
        if(curDashDelay > 0)
        {
            dashIconBlack.SetActive(true);
            dashDelayUI.text = Mathf.CeilToInt(curDashDelay).ToString();
            return;
        }
        dashIconBlack.SetActive(false);
    }
    void ReLoading()
    {
        if (isReLoading)
            return;
        isReLoading = true;
        Magazine mgScript = magazine_Objt.GetComponent<Magazine>();
        mgScript.Take_apart_magazine();
        bulletDelay = 10;
        magazine = 30;
        Invoke("ReLoad", 2);
        rop.Play();
    }
    void ReLoad()
    {
        magazine_Objt.SetActive(true);
        mgScript.Get_magazine();
        bulletDelay = 0.1f;
        MagazineUI.text = magazine + " | 30";
        isReLoading = false;
    }
    void Regesistor()
    {
        gameObject.layer = 3;
        Invoke("Weak",0.1f);
    }
    void Weak()
    {
        gameObject.layer = 0;
    }
    void Dash()
    {
        if (curDashDelay > 0)
        {
            curDashDelay -= Time.deltaTime;
            return;
        }
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        Dah.Play();
        Regesistor();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        rigid.AddForce((mousePos - transform.position).normalized * dashPower, ForceMode2D.Impulse);
        Invoke("Stop", 0.2f);
        curDashDelay = 5;
    }
    void Stop()
    {
        rigid.velocity = Vector3.zero;    
    }
    void Basic_Shoot()
    {
        if (curDelay < bulletDelay)
            return;
        if (!Input.GetMouseButton(0))
            return;
        if (magazine <= 0)
        {
            ReLoading();
            return;
        }
        magazine--;
        exp.Play();
        epx.Play();
        
        GameObject BasicBullet = Instantiate(playerBasicBullet);
        //Spawn Bullet Casing
        GameObject BulletC = Instantiate(Bullet_Casing_Pre);
        Bullet_Casing Casing_Script = BulletC.GetComponent<Bullet_Casing>();
        Casing_Script.pos = Bullet_Casing_Shoot_Pos;
        BulletC.transform.position = Bullet_Casing_Pos.position;

        //Shoot Bullet;
        Rigidbody2D bulletRB = BasicBullet.GetComponent<Rigidbody2D>();
        BasicBullet.GetComponent<PlayerBullet>().player = this;
        BasicBullet.transform.position = gun.position;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        var rot = mousePos - BasicBullet.transform.position;
        var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        BasicBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bulletRB.AddForce((mousePos - transform.position).normalized * shootPower, ForceMode2D.Impulse);

        //Update UI
        MagazineUI.text = magazine + " | 30";
        curDelay = 0;
    }

    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }

    void Move()
    {
        score += Time.deltaTime;

        moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        if ((isTouchRight && moveX > 0) || (isTouchLeft && moveX < 0))
            moveX = 0;
        moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
        if ((isTouchTop && moveY > 0) || (isTouchBottom && moveY < 0))
            moveY = 0;

        transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);

        /*mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = new Vector3(mousePos.x, mousePos.y, 0);
        if (dir.x < -12.85f || dir.x > 12.85f)
            dir.x = dir.x > 0 ? 12.85f : -12.85f;
        if (dir.y < -7 || dir.y > 7)
            dir.y = dir.y > 0 ? 7 : -7;
        transform.position = dir;*/
    }

    public void gameOver()
    {
        rank.SetHiScore();
        Time.timeScale = 0;
        image.gameObject.SetActive(true);
        Over.gameObject.SetActive(true);
        OverMasage.gameObject.SetActive(true);
        HomeMasage.gameObject.SetActive(true);
        retryMasage.gameObject.SetActive(true);
        BGM.SetActive(false);
        gameManager.game = "GameOver";
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameOver();
        }
        if (collision.gameObject.tag == "border")
        {
            rigid.velocity = Vector3.zero;
            switch (collision.gameObject.name)
            {
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
        else if (collision.CompareTag("Enemy"))
            gameOver();
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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   
}
