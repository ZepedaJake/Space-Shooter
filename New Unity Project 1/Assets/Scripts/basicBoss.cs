using UnityEngine;
using System.Collections;

public class basicBoss : enemyBase {

    public GameObject missile;
    public float fireTimer = 5;
    public bool dir;
    
    public basicBoss()
    {
        health = 5000;
        exp = 400;
        money = 700;
        enemyDamage = 32;
        speed = 25;
        maxSpeed = 10;
        spawnTimer = 9999;
        minSpawnTime = 9999;
    }
	// Use this for initialization
	void Start () {
       

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
	void Update () {
        Fire();
        if (gameObject.transform.position.x <= globalData.leftEdge)
        {
            dir = false;
        }
        else if(gameObject.transform.position.x >= globalData.rightEdge)
        {
            dir = true;
        }
        if (!dir)
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime * Time.timeScale);
            
        }
        else if( dir )
        {
            gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime * Time.timeScale);
            
        }
        if(health <= 0)
        {
            Destroy(gameObject);
            theLevelMaster.ExpandSpace();
        }
        
    }

    void Fire()
    {
        fireTimer -= Time.deltaTime * Time.timeScale;
        if(fireTimer <= 0)
        {
            GameObject missileTemp = (GameObject)Instantiate(missile, gameObject.transform.position, new Quaternion(0,1,-1,0));
            missileTemp.SendMessage("SetDamage", enemyDamage);
            fireTimer = 5;
        }
    }
}
