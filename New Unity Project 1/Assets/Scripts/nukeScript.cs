using UnityEngine;
using System.Collections;

public class nukeScript : MonoBehaviour {
    skill self;
    public float power;
    public float size;
    playerScript player;
    public bool travel =  true;
    public bool playSound = true;
    public GameObject innerNuke;
    public GameObject explosion;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        self = player.playerSkills.Find(x => x.skillName.Contains("Nuke"));
        power = self.power;
        size = self.size;
    }
	
	// Update is called once per frame
	void Update () {
        if (travel)
        {
            gameObject.transform.Translate(Vector3.up * .1f * Time.timeScale);
        }
        else
        {
            if (gameObject.GetComponent<SphereCollider>().radius < size)
            {
                if(playSound)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                    playSound = false;
                }
                gameObject.GetComponent<SphereCollider>().radius += ((size - .5f)/.8f) * Time.deltaTime * Time.timeScale;
                explosion.transform.localScale = new Vector3(gameObject.GetComponent<SphereCollider>().radius*2, gameObject.GetComponent<SphereCollider>().radius*2, gameObject.GetComponent<SphereCollider>().radius*2);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        innerNuke.transform.Rotate(Vector3.right * Time.deltaTime * Time.timeScale * 20);
        innerNuke.transform.Rotate(Vector3.up * Time.deltaTime * Time.timeScale * 20);        

    }
}
