using UnityEngine;
using System.Collections;

public class enemyBase : MonoBehaviour
{

    public float speed;
    public float maxSpeed;
    public double spawnTimer;
    public double minSpawnTime;
    public float health;
    public levelMaster theLevelMaster;
    public playerScript player;
    public int exp;
    public int money;
    public int enemyDamage;
    public ParticleSystem smoke;
    public ParticleSystem bulletParticle;
    public float speedTemp;
    public bool slowed;
    public AudioSource enemyAudioSource;
    public AudioClip explode;
    

    // Use this for initialization
    void Start()
    {
        smoke = gameObject.GetComponent<ParticleSystem>();
       
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();    
        spawnTimer -= (theLevelMaster.difficulty - 1) * .1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * speed / 10 * Time.timeScale);
        if(slowed)
        {
            try
            {
                
            }
            catch
            {
                slowed = false;
                speed = speedTemp;
            }    
        }
        else
        {
            speed = speedTemp;
        }
    }

    void OnTriggerEnter(Collider o)
    {
        if (o.tag == "bullet")
        {


            int x = Random.Range(0, 100);
            if (x <= GameObject.FindWithTag("Player").GetComponent<playerScript>().critChance)
            {

                bulletParticle.startColor = Color.red;
                BulletHit(o);
                health -= o.GetComponent<bulletScript>().damage * GameObject.FindWithTag("Player").GetComponent<playerScript>().critMult;

                if (bulletParticle.isPlaying == false && GameObject.FindWithTag("Player").GetComponent<playerScript>().piercingShots == false)
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

                    Destroy(o.gameObject);
                }
            }

            if (health <= 0)
            {
                Death();
                GameObject.FindWithTag("Player").SendMessage("Exp", exp);
                theLevelMaster.kills += 1;
                //if a boss is spawnable, add to the spawn kill count down
                if(theLevelMaster.spawnBoss)
                {
                    theLevelMaster.bossSpawnCounter++;
                }
                theLevelMaster.money += money;
                theLevelMaster.UpdateUI();
            }
        }
        else if (o.tag == "playerTrigger")
        {
            int y = Random.Range(0, 100);

            if (y > GameObject.FindWithTag("Player").GetComponent<playerScript>().evade)
            {
                int dmgTemp = (int)(enemyDamage - GameObject.FindWithTag("Player").GetComponent<playerScript>().armor / (theLevelMaster.difficulty / 2));

                if (dmgTemp > 1)
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= dmgTemp;
                else
                    GameObject.FindWithTag("Player").GetComponent<playerScript>().currentHealth -= 1;

                Death();
                theLevelMaster.UpdateUI();
            }


        }
        else if (o.tag == "bottom")
        {
            Destroy(gameObject);

        }
        else if(o.tag == "nuke")
        {
            health -= o.GetComponent<nukeScript>().power;
            o.GetComponent<nukeScript>().travel = false;
            if (health <= 0)
            {
                Death();
                GameObject.FindWithTag("Player").SendMessage("Exp", exp);
                theLevelMaster.kills += 1;
                theLevelMaster.money += money;
                theLevelMaster.UpdateUI();
            }
        }
    }

    void Death()
    {
        try
        {
            //bosses dont die when hitting player because they have no enemyAudioSource
            enemyAudioSource.Play();
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            smoke.Play();
            
            if (smoke.isPlaying == false)
            {
                Destroy(gameObject);
            }
       }
       catch { }
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

    void OnTriggerStay(Collider c)
    {

        if (c.tag == "slow")
        {
            Debug.Log("sloooooow");
            if (slowed == false)
            {

                speed *= ((100 - c.gameObject.GetComponent<slowFieldScript>().power) / 100);
                slowed = true;
            }            
        }
        
        
    }

    void OnTriggerLeave(Collider c)
    {
        if (c.tag == "slow")
        {
            
            if (slowed == true)
            {
                slowed = false;              
            }

        }
    }
}