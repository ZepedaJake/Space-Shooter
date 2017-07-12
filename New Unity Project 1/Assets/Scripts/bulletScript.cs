using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
    public float bulletSpeed;
    public float damage;
    float timeout = 15;
	// Use this for initialization
	void Start () {
        damage = GameObject.FindWithTag("Player").GetComponent<playerScript>().damage;
        bulletSpeed = GameObject.FindWithTag("Player").GetComponent<playerScript>().projectileSpeed;
        
    }
	   

	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.forward * bulletSpeed * Time.timeScale);
        timeout -= Time.deltaTime * Time.timeScale;
        if(timeout <= 0)
        {
          
            Destroy(gameObject);
        }
        
	}

    void OnTriggerEnter(Collider o)
    {
        if (o.tag == "top")
        {           
            Destroy(gameObject);           
        }
    }
}
