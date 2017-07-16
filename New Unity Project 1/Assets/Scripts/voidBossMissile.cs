using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBossMissile : MonoBehaviour {

    public float damage;
    levelMaster theLevelMaster;
    public ParticleSystem smoke;
    public float lifetime = 7;


    // Use this for initialization
    void Start()
    {
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
        smoke = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime * Time.timeScale;
        //follow player
        transform.LookAt(globalData.player.transform);
        transform.position = Vector3.MoveTowards(transform.position,globalData.player.transform.position,Time.timeScale * Time.deltaTime * 8);
        //transform.Translate(transform.forward * Time.deltaTime * Time.timeScale * -10);
        if (gameObject.transform.position.z > globalData.topEdge)
        {
            Destroy(gameObject);
        }

        if(lifetime <=0)
        {
            Destroy(gameObject);
        }
        
    }

    void SetDamage(float d)
    {
        damage = d;
    }

    void OnTriggerEnter(Collider o)
    {
        if (o.tag == "playerTrigger")
        {
            int y = Random.Range(0, 100);

            if (y > GameObject.FindWithTag("Player").GetComponent<playerScript>().evade)
            {
                int dmgTemp = (int)(damage - GameObject.FindWithTag("Player").GetComponent<playerScript>().armor / (theLevelMaster.difficulty / 2));

                if (dmgTemp > 1)
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= dmgTemp;
                else
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= 1;
            }


            Death();
            theLevelMaster.UpdateUI();
        }
    }

    void Death()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        smoke.Play();
        if (smoke.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
