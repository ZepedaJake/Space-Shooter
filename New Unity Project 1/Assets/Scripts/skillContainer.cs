using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillContainer : MonoBehaviour {
    //this class initializes skills;
    //also holds custom color because this is carried from the main menu
    public GameObject[] skills;
    public skill[] chosenSkills = new skill[4];
    playerScript player;

    public Color playerMaterialColor;
    public Color playerGlowColor;

    // Use this for initialization
    void Start () {
        createSkills();
        globalData.theSkillContainer = gameObject.GetComponent<skillContainer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void createSkills()
    {
        globalData.allSkills.Add(new skill("SlowField", skill.skillType.Place, 7, 5, 30, 25, 20,"Slow Field"));
        globalData.allSkills.Add(new skill("Heal", skill.skillType.Buff, 0, 10, 60, 5, 25));
        globalData.allSkills.Add(new skill("Nuke", skill.skillType.Shoot, 10, .5f, 30, 1.5f, 50));
        globalData.allSkills.Add(new skill("Buff", skill.skillType.Buff, 0, 10, 20, 100, 25));
        globalData.allSkills.Add(new skill("Test2", skill.skillType.Place, 1, 1, 1, 1, 1));
        globalData.allSkills.Add(new skill("Test3", skill.skillType.Place, 1, 1, 1, 1, 1));

        SetDescriptionDetails("SlowField");
        SetDescriptionDetails("Heal");
        SetDescriptionDetails("Nuke");
        SetDescriptionDetails("Buff");
    }
    void SetDescriptionDetails(string s)
    {
        skill self = globalData.allSkills.Find(x => x.skillName.Contains(s));
        switch (s)
        {
            case "SlowField":
                {
                    setSkillDesc(self, "Place a " + self.size + "m bubble that slows enemies by " + self.power + "% for " + self.duration + " seconds.");
                    break;
                }
            case "Heal":
                {
                    setSkillDesc(self, "Heal " + self.power + " points of health per second, for " + self.duration + " seconds.");
                    break;
                }
            case "Nuke":
                {
                    setSkillDesc(self, "Launch a nuke, dealing " + self.power + " damage in a " + self.size + "m radius.");
                    break;
                }
            case "Buff":
                {
                    setSkillDesc(self, "Increase damage by " + self.power + "% for " + self.duration + " seconds.");
                    break;
                }
            default:
                break;
        }
    }


    void setSkillDesc(skill s, string description)
    {

        s.desc = description;
    }

    public void GivePlayerSkills()
    {
        player = GameObject.Find("Player").GetComponent<playerScript>();
        
        foreach(skill s in chosenSkills)
        {
            player.playerSkills.Add(s);
            Debug.Log(s.skillName);
        }
    }

    public void SetPlayerColor()
    {
        player = GameObject.Find("Player").GetComponent<playerScript>();
        player.GetComponent<Light>().color = playerGlowColor;
    }
}
