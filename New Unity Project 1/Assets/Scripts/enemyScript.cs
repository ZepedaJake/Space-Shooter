using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

    public float speed = .5f;
    public float maxSpeed = 10;
    public double spawnTimer = 1;
    public float health = 80;
    public levelMaster theLevelMaster;
    public int exp = 10;
    public int money = 10;
    public int enemyDamage = 5;
    public ParticleSystem smoke;
    public ParticleSystem bulletParticle;
   
	// Use this for initialization
	void Start () {
        smoke = gameObject.GetComponent<ParticleSystem>();
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
       
        speed += theLevelMaster.difficulty / 4;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        money *= (int)(theLevelMaster.difficulty);
        health += theLevelMaster.difficulty * (int)(Mathf.Pow((float)(theLevelMaster.difficulty),1.6f));
        
        enemyDamage *= (int)(theLevelMaster.difficulty);
        exp = (int)(theLevelMaster.difficulty * 10);
        spawnTimer -= (theLevelMaster.difficulty-1) *.1;
	}
	 
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * speed / 10 * Time.timeScale);
        

    }

    void OnTriggerEnter(Collider o)
    {
        if (o.tag == "bullet")
        {
            
            
            int x = Random.Range(0, 100);
            if(x <= GameObject.FindWithTag("Player").GetComponent<playerScript>().critChance)
            {
               
                bulletParticle.startColor = Color.red;            
                BulletHit(o);
                health -= o.GetComponent<bulletScript>().damage * GameObject.FindWithTag("Player").GetComponent<playerScript>().critMult;

                if(bulletParticle.isPlaying == false && GameObject.FindWithTag("Player").GetComponent<playerScript>().piercingShots == false)
                {
                    Destroy(o);
                }
            }
            else
            {
                bulletParticle.startColor = Color.yellow;
                BulletHit(o);           
                health -= o.GetComponent<bulletScript>().damage;
                if (bulletParticle.isPlaying == false && GameObject.FindWithTag("Player").GetComponent<playerScript>().piercingShots == false)
                {
                    Destroy(o);
                }
            }
            
            if(health <= 0)
            {
                Death();
                theLevelMaster.kills += 1;
                theLevelMaster.money += money;
                GameObject.FindWithTag("Player").SendMessage("Exp", exp);
                theLevelMaster.UpdateUI();               
            }     
        }
        else if (o.tag == "playerTrigger")
        {
            int y = Random.Range(0, 100);

            if(y > GameObject.FindWithTag("Player").GetComponent<playerScript>().evade)
            {
                int dmgTemp = (int)(enemyDamage - GameObject.FindWithTag("Player").GetComponent<playerScript>().armor / (theLevelMaster.difficulty / 2));

                if (dmgTemp >1)
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= dmgTemp;
                else
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= 1;

                Death();
                theLevelMaster.UpdateUI();
            }

            
        }
        else if(o.tag == "bottom")
        {
            Destroy(gameObject);
            
        }
    }

    void Death()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        smoke.Play();
        if(smoke.isPlaying == false)
        {
            Destroy(gameObject);
        }


    }

    void BulletHit(Collider o)
    {
        if (GameObject.FindWithTag("Player").GetComponent<playerScript>().piercingShots == false)
        {
            o.GetComponent<MeshRenderer>().enabled = false;
            o.GetComponent<BoxCollider>().enabled = false;
            o.GetComponent<TrailRenderer>().enabled = false;
            o.GetComponent<bulletScript>().bulletSpeed = 0;
        }
        Instantiate(bulletParticle, o.transform.position, new Quaternion(0, 0, 0, 0));
        
    }
}
