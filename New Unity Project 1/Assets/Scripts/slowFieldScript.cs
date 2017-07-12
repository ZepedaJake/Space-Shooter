using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class slowFieldScript : MonoBehaviour {
    
    public float currentSize = 1;
   
       float size = 7;
        float duration = 5;
         public float power = 25;
        
    skill self;
    playerScript player;


    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        self = player.playerSkills.Find(x => x.skillName.Contains("SlowField"));
        size = self.size;
        duration = self.duration;
        power = self.power;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (currentSize < size)
        {
            currentSize += Time.deltaTime * Time.timeScale * 15;
        }
        else
        {
            currentSize = size;
        }
            
        gameObject.transform.localScale = new Vector3(currentSize,currentSize,currentSize);

        duration -= Time.deltaTime * Time.timeScale;
        if(duration <= 0)
        {
            Destroy(gameObject);
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("enemy"))
            {
                e.GetComponent<enemyBase>().slowed = false;
                e.GetComponent<enemyBase>().speed = e.GetComponent<enemyBase>().speedTemp;
            }
            
        }
	}
}
