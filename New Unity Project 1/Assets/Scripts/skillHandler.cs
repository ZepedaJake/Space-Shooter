using UnityEngine;
using System.Collections;

public class skillHandler:MonoBehaviour {
    //this class holds the functions to activate skills
    public GameObject slowField;
    public GameObject nuke;
    playerScript player;
    levelMaster theLevelMaster;
    public float t = 0;
    public bool shouldTick = false;
    public bool tick = false;

    //needed to properly heal with skill
    bool doHeal = false;
    bool doBuff = false;
    skill skillTemp;
    float durationTemp;
    float damageTemp;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
    }

    void Update()
    {

        // one second tick timer. tell it to start from a skill. after a second sets tick bool to true, saying a second passed. remember to set tick to false in skill
        if (shouldTick)
        {
            t += (Time.deltaTime * Time.timeScale);
            if (t>= 1)
            {
                tick = true;
                t = 0;
            }
        } 

        //handles healing skill
        if (doHeal)
        {
            durationTemp -= Time.deltaTime * Time.timeScale;
            
            if (tick && (player.maxHealth - player.currentHealth) >= skillTemp.power)
            {
                player.currentHealth += (int)skillTemp.power;
                theLevelMaster.UpdateUI();
                tick = false;
            }
            else if(tick && (player.maxHealth -player.currentHealth) < skillTemp.power)
            {
                player.currentHealth += (player.maxHealth - player.currentHealth);
                theLevelMaster.UpdateUI();
                tick = false;
            }
            else
            {
                theLevelMaster.UpdateUI();
                tick = false;
            }

            if (durationTemp <= 0)
            {
                shouldTick = false;
                doHeal = false;
                //skillTemp.currentCool = skillTemp.coolDown;
                //skillTemp.cooling = true;
                t = 0;
            }
            
        }

        //Buff Timer
        if(doBuff)
        {
            durationTemp -= Time.deltaTime * Time.timeScale;

            //if damage gets reset at some point. rebuff
            if(player.GetComponent<playerScript>().damage != damageTemp)
            {
                player.GetComponent<playerScript>().damage = damageTemp;
            }

            if(durationTemp <= 0)
            {
                player.GetComponent<playerScript>().UpdateStats();
            }
        }
        
    }
	public void SlowField()
    {
        Instantiate(slowField, new Vector3(player.setPos.x, 0, player.setPos.z), gameObject.transform.rotation);       
    }

    public void Heal()
    {
       skillTemp =  player.playerSkills.Find(x => x.skillName.Contains("Heal"));
        
        shouldTick = true;
        doHeal = true;
        //set this early to get the inital tick on startup
        tick = true;
        durationTemp = skillTemp.duration;                           
    }

    public void Nuke()
    {
        Instantiate(nuke, new Vector3(player.transform.position.x, 0, player.transform.position.z), gameObject.transform.rotation);
    }

    public void Buff()
    {
        skillTemp = player.playerSkills.Find(x => x.skillName.Contains("Buff"));
        damageTemp = player.GetComponent<playerScript>().damage * (1 + (skillTemp.power / 100));
        doBuff = true;
        durationTemp = skillTemp.duration;
    }

}

