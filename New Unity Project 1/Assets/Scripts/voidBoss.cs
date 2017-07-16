using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBoss : enemyBase {

    public GameObject missile;
    public GameObject portal;
    GameObject portalInstance;
    public float fireTimer = 3;
    public float teleportTimer = 15;
    public bool dir;
    public bool sink = false;
    public bool blink = true;
    float sinkTimer;

    public voidBoss()
    {
        health = 5000;
        exp = 1000;
        money = 900;
        enemyDamage = 30;
        speed = 10;
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
        Teleport();
        //transform.LookAt(globalData.player.transform);
        
        if (gameObject.transform.position.x <= globalData.leftEdge+10)
        {
            dir = false;
        }
        else if (gameObject.transform.position.x >= globalData.rightEdge-10)
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

        //when above ground, fire seeker missiles
        if(transform.position.y > -1)
        {
            Fire();
        }
        
        //if portal is open and boss is out, sink boss
        if(portalInstance.GetComponent<voidBossPortal>().ready && sink)
        {
            sinkTimer += Time.deltaTime * Time.timeScale;
            //transform.Translate(Vector3.up * Time.deltaTime * Time.timeScale * -2);           
            if(transform.position.y > -20)
            {
                transform.position = Vector3.Lerp(transform.position, (transform.position - (transform.up)), sinkTimer * .1f);
                blink = true;
            }
            else if(blink)
            {
                //after sinking, move to random location
                
                float randomX = Random.Range(globalData.leftEdge + 10, globalData.rightEdge - 10);
                float randomZ = Random.Range(globalData.topEdge - 15, globalData.bottomEdge + 30);
                transform.position = new Vector3(randomX, -20, randomZ);
                blink = false;
                
            }
        }
        else if(portalInstance.GetComponent<voidBossPortal>().ready && !sink)
        {
            sinkTimer += Time.deltaTime * Time.timeScale;
            //transform.Translate(Vector3.up * Time.deltaTime * Time.timeScale * -2);           
            if (transform.position.y <=0 )
            {
                transform.position = Vector3.Lerp(transform.position, (transform.position + (transform.up)), sinkTimer * .1f);               
            }
            
            
            
        }
    }
    void Teleport()
    {
        //portal increase size beneath, boss sink into portal, portal disappear
        teleportTimer -= Time.deltaTime * Time.timeScale;
        if(teleportTimer <= 0)
        {
            //spawn portal underneath self
            Debug.Log("Void Boss Teleport");
            portalInstance = (GameObject)Instantiate(portal, new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Quaternion(0, 0, 0, 0));
            teleportTimer = 15;
            sinkTimer = 0;
            sink = !sink;
        }
    }
    void Fire()
    {
        fireTimer -= Time.deltaTime * Time.timeScale;
        if (fireTimer <= 0)
        {           
            GameObject missileTemp = (GameObject)Instantiate(missile, gameObject.transform.position, new Quaternion(0, 1, -1, 0));
            missileTemp.SendMessage("SetDamage", enemyDamage);
            fireTimer = 4;
        }
    }
}
