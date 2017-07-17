using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commanderBossDrone : enemyBase {
    public bool alive = true;
    commanderBoss parent;
    float fireTimer = 3;
    public GameObject missile;
    // Use this for initialization
    void Start () {
        parent = gameObject.GetComponentInParent<commanderBoss>();
        health = parent.health / 5;
        exp = 200;
        enemyDamage = parent.enemyDamage;
	}

    private void OnEnable()
    {
        parent = gameObject.GetComponentInParent<commanderBoss>();
        health = parent.health / 5;

        enemyDamage = parent.enemyDamage;
    }

    // Update is called once per frame
    void Update ()
    {
        Fire();
        if (health <= 0)
        {           
            gameObject.SetActive(false);           
        }
    }

    void Fire()
    {
        fireTimer -= Time.deltaTime * Time.timeScale;
        if (fireTimer <= 0 && alive)
        {
            GameObject missileTemp = (GameObject)Instantiate(missile, gameObject.transform.position, new Quaternion(0, 1, -1, 0));
            missileTemp.SendMessage("SetDamage", enemyDamage);
            fireTimer = 10;
        }
    }
}
