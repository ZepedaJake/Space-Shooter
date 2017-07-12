using UnityEngine;
using System.Collections;

public class bulletHitScript : MonoBehaviour {

    ParticleSystem self;
    float timeout = 5;
	// Use this for initialization
	void Start () {
        self = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	if(self.isPlaying == false)
        {
            Destroy(gameObject);
        }
        timeout -= Time.deltaTime * Time.timeScale;
        if(timeout <= 0)
        {
            Destroy(gameObject);
        }
    
	}
}
