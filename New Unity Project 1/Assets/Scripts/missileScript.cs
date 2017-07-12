using UnityEngine;
using System.Collections;

public class missileScript : MonoBehaviour
{
    public float damage;
    levelMaster theLevelMaster;
    public ParticleSystem smoke;


    // Use this for initialization
    void Start()
    {
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
        smoke = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.up * 50 * Time.deltaTime * Time.timeScale);
        if(gameObject.transform.position.z > globalData.topEdge)
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
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        smoke.Play();
        if (smoke.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
