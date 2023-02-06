using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RankManager rankManager;
    public Text BestRound;
    public Text Round;
    public GameObject effectPrefab;
    public Transform effectGroup; 
    //public Text BulletGauge;
    public GameObject mainBGM;
    public GameObject player;
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;
    public float maxSpawnDelay = 30;
    public float curSpawnDelay;
    public GameObject warringline;
    public float delay;
    int count = 0;
    public float curDelay;
    public string game;
    public GameObject menuSet;
    public AudioSource bgm;
    public AudioSource[] sfxPlayer;
    public AudioClip[] sfxClip;
    public AudioSource btnsource;
    public enum Sfx {Laser, Bullet, gun}
    int sfxcursor;
    public int round;
    public int curEnemys;
    GameObject[] Enemys = new GameObject[1000];
    
    void Awake()
    {
        Time.timeScale = 1;
        delay = Random.Range(2f, 4f);
        rankManager = GameObject.Find("RankManager").GetComponent<RankManager>();
        BestRound.text = "Best Round : " + rankManager.BestRound;
    }
    void Update()
    {
        if (curEnemys == 0)
        {
            round++;
            curEnemys = round;
            RoundChange(round);
        }
        /*if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy(enemyType);
            enemyType = enemyType == 0 ? 1 : 0;
            curSpawnDelay = 0;
        }
        curSpawnDelay += Time.deltaTime;*/
        curDelay += Time.deltaTime;
        if (curDelay > delay)
        {
            count = 0;
            spawnLaser();
            curDelay = 0;
            delay = Random.Range(2f, 4f);
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetButtonDown("Cancel"))
        {

            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                if(game != "GameOver")
                {
                    Time.timeScale = 1;
                    mainBGM.SetActive(true);
                }
            }
            else
            {
                menuSet.SetActive(true);
                Time.timeScale = 0;
                mainBGM.SetActive(false);
            }
        }
        if(Time.timeScale != 0)
            ManageUI();
    }
    void ManageUI()
    {
        if (rankManager.BestRound <= round)
        {
            Round.color = BestRound.color;
            rankManager.BestRound = round;
            BestRound.text = "BestRound : " + round;
        }
        else
            Round.color = Color.white;
        Round.text = "Round : " + round;
        //BulletGauge.text = player.GetComponent<Player>().bulletGauge + "/100";
    }
    void spawnLaser()
    {
        if (count == 8)
            return;
        Vector3 spawnPoint = new Vector3(Random.Range(-14.5f, 15f), Random.Range(6f, -7f), 0);
        float spawnRotation = Random.Range(0f, 181f);
        GameObject line = Instantiate(warringline);
        WarringLine linelogic = line.GetComponent<WarringLine>();
        linelogic.manager = this;
        line.transform.position = spawnPoint;
        line.transform.Rotate(Vector3.forward * spawnRotation);
        count++;
        Invoke("spawnLaser", 0.07f);  

        return;
    }
    void RoundChange(int round)
    {
        for(int i = 1; i <= round; i++)
        {
            int enemyType = i % 5 == 0 ? 1 : 0;
            int ranPoint = Random.Range(0, 4);
            Enemys[i - 1] = Instantiate(enemyObjs[enemyType], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
            See_Player enemyLogic = Enemys[i - 1].GetComponent<See_Player>();
            Enemys[i - 1].GetComponent<Enemy>().manager = this;
            enemyLogic.target = player.transform;
        }
    }
    /*void SpawnEnemy(int ranEnemy)
    {
        int ranPoint = Random.Range(0,4);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        See_Player enemyLogic = enemy.GetComponent<See_Player>();
        enemyLogic.target = player.transform;
    }*/

    public void SfxPlay(Sfx type)
    {
        switch (type)
        {
            case Sfx.Laser:
                sfxPlayer[sfxcursor].clip = sfxClip[0];
                break;
            case Sfx.Bullet:
                sfxPlayer[sfxcursor].clip = sfxClip[1];
                break;
            case Sfx.gun:
                sfxPlayer[sfxcursor].clip = sfxClip[2];
                break;

        }

        sfxPlayer[sfxcursor].Play();
        sfxcursor = (sfxcursor + 1) % sfxPlayer.Length;
    }

    

    
}