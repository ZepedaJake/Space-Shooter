using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ramBoss : enemyBase {

    public GameObject missile;
    public GameObject selfBody;
    public float fireTimer = 7;
    public bool recharging = false;
    public bool dir;
    public float rechargeTimer = 3;
    public Material bossBodyMaterial;

    public ramBoss()
    {
        health = 2000;
        exp = 800;
        money = 800;
        enemyDamage = 55;
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
        fireTimer = Random.Range(12, 17);

        bossBodyMaterial.color = new Color(75 / 255f, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
       //move left and right
        if (gameObject.transform.position.x <= globalData.leftEdge+5)
        {
            dir = false;
        }
        else if (gameObject.transform.position.x >= globalData.rightEdge-5)
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

        //after ramming. resummon after x seconds
        if(recharging)
        {
            //after 3 seconds begin reappearing
            
            rechargeTimer -= Time.deltaTime * Time.timeScale;
            if(rechargeTimer<=0)
            {
                //fade in
                selfBody.SetActive(true);
                bossBodyMaterial.color += new Color(0, 0, 0, .01f);
            }

            //after alpha is full, start moving
            if(bossBodyMaterial.color.a >= 1)
            {
                recharging = false;
                speed = 25;
            }
            
            
        }

    }

    void Fire()
    {
        fireTimer -= Time.deltaTime * Time.timeScale;
        if (fireTimer <= 0)
        {
            //make body invisible, then unable to be hit
            bossBodyMaterial.color = new Color(75/255f, 0, 0, 0);
            selfBody.SetActive(false);
            //fire missile body
            GameObject missileTemp = (GameObject)Instantiate(missile, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
            missileTemp.SendMessage("SetDamage", enemyDamage);
            fireTimer = Random.Range(12, 17);
            speed = 0;
            //prepare to reappear
            rechargeTimer = 3;
            recharging = true;
        }
    }
}
