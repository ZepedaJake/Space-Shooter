using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commanderBoss : enemyBase {

    
    public float fireTimer = 30;
    public bool dir;
    
    public GameObject[] drones = new GameObject[5];
    public commanderBoss()
    {
        health = 6000;
        exp = 1100;
        money = 1000;
        enemyDamage = 50;
        speed = 25;
        maxSpeed = 10;
        spawnTimer = 9999;
        minSpawnTime = 9999;
    }
    // Use this for initialization
    void Start()
    {


        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
        smoke = gameObject.GetComponent<ParticleSystem>();



        money *= (int)(theLevelMaster.difficulty);
        health += theLevelMaster.difficulty * (int)(Mathf.Pow((float)(theLevelMaster.difficulty), 1.6f));

        enemyDamage *= (int)(theLevelMaster.difficulty);
        if (exp < exp * (int)(theLevelMaster.difficulty / 2))
        {
            exp *= (int)(theLevelMaster.difficulty / 2);
        }

        speedTemp = speed;

        
    }

    // Update is called once per frame
    void Update()
    {
        SummonDrones();
        if (gameObject.transform.position.x <= globalData.leftEdge)
        {
            dir = false;
        }
        else if (gameObject.transform.position.x >= globalData.rightEdge)
        {
            dir = true;
        }
        if (!dir)
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime * Time.timeScale);

        }
        else if (dir)
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime * Time.timeScale);

        }
        //on death, expand play area, and allow next boss countdown to begin.
        if (health <= 0)
        {
            Destroy(gameObject);
            theLevelMaster.spawnBoss = true;
            theLevelMaster.BossProgression();
            theLevelMaster.ExpandSpace();
        }

    }

    void SummonDrones()
    {
        fireTimer -= Time.deltaTime * Time.timeScale;
        if (fireTimer <= 0)
        {
            for(int x= 0; x<5;x++)
            {
                drones[x].gameObject.SetActive(true);                                            
            }            
            fireTimer = 30;
        }
    }
}
