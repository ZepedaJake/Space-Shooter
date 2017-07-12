using UnityEngine;
using System.Collections;

public class bigEnemy : enemyBase {
    
    public bigEnemy()
    {
        health = 500;
        exp = 150;
        money = 60;
        enemyDamage = 15;
        speed = .2f;
        maxSpeed = 3;
        spawnTimer = 7;
        minSpawnTime = 1.5f;
    }

    // Use this for initialization
    void Start()
    {
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
        smoke = gameObject.GetComponent<ParticleSystem>();
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        speed += theLevelMaster.difficulty / 4;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        money *= (int)(theLevelMaster.difficulty);
        health += theLevelMaster.difficulty * (int)(Mathf.Pow((float)(theLevelMaster.difficulty), 1.6f));

        enemyDamage *= (int)(theLevelMaster.difficulty);
        enemyDamage *= (int)(theLevelMaster.difficulty);
        if (exp < exp * (int)(theLevelMaster.difficulty / 2))
        {
            exp *= (int)(theLevelMaster.difficulty / 2);
            exp *= (int)(1 + (player.luk / 100));
        }

        speedTemp = speed;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.Translate(Vector3.forward * speed / 10 * Time.timeScale);
        if (gameObject.transform.position.z < globalData.bottomEdge)
        { Destroy(gameObject); }
    }
}
