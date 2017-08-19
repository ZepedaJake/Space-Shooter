using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class skillMenu : MonoBehaviour {
    playerScript player;
    levelMaster theLevelMaster;
    public GameObject aSkillPanel;
    public Text skillPointsText;
   

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();

        PlaceSkillMenu();
        
        
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    void OnEnable()
    {
        //PlaceSkillMenu();
        UpdateSkillMenu();

    }

    public void PlaceSkillMenu()
    {
        int y = -50;
        foreach(skill s in player.playerSkills)
        {
            GameObject newSkill = (GameObject)Instantiate(aSkillPanel, new Vector3(100, y, 0), new Quaternion(0, 0, 0, 0));
            newSkill.transform.SetParent(gameObject.transform, false);
            newSkill.GetComponent<aSkillPanel>().skillName.text = s.skillName;
            newSkill.GetComponent<aSkillPanel>().skillLevel.text = "("+s.level.ToString() + ")";
            newSkill.GetComponent<aSkillPanel>().skillCoolDown.text = "Cool Down: "+s.coolDown.ToString();
            newSkill.GetComponent<aSkillPanel>().skillCost.text = "Cost: "+s.cost.ToString();
            newSkill.GetComponent<aSkillPanel>().skillDesc.text = s.desc;            
            AddListener(newSkill.GetComponentInChildren<Button>(), s);
            Debug.Log(s.skillName + "ready");
            y -= 100;     
        }
        UpdateSkillMenu();
    }

    void AddListener(Button b, skill s)
    {

        b.onClick.AddListener(() => { SkillUpgrade(s); });
       
    }

    public void SkillUpgrade(skill k)
    {

        if (k.level < 5 && player.skillPoints > 0)
        {
            k.level += 1;
            player.skillPoints -= 1;

            switch (k.skillName)
            {
                case "SlowField":
                    k.basePower += 5;
                    k.baseDuration += 2;
                    k.baseSize += 1;
                    k.baseCost -= 1;
                    break;
                case "Heal":
                    k.basePower += 5;
                    k.baseDuration += 1;
                    k.baseCost += 7;
                    k.baseCoolDown += 3;
                    break;
                case "Nuke":
                    k.basePower += .5f;
                    k.baseCost += 15;
                    k.baseSize += 3;
                    k.baseCoolDown -=2;
                    break;
                case "Buff":
                    k.basePower += 20;
                    k.baseCost += 5;
                    k.baseDuration += 5;
                    
                    break;
                default:
                    break;
            }
            Debug.Log("skill up");
            
        }
        else { Debug.Log("skill Maxed"); }
        
        UpdateSkillMenu();
    }

    

    public void UpdateSkillMenu()
    {
        player.UpdateSkillValues();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("skillPanel"))
        {
            skill s = player.playerSkills.Find(x => x.skillName.Contains(g.GetComponent<aSkillPanel>().skillName.text));
            g.GetComponent<aSkillPanel>().skillName.text = s.skillName;          
            g.GetComponent<aSkillPanel>().skillLevel.text = "(" + s.level.ToString() + "/5)";
            g.GetComponent<aSkillPanel>().skillCoolDown.text = "Cool Down: " + s.coolDown.ToString();
            g.GetComponent<aSkillPanel>().skillCost.text = "Cost: " + s.cost.ToString();
            g.GetComponent<aSkillPanel>().skillDesc.text = s.desc;
        }
        skillPointsText.text = "Skill Points: " + player.skillPoints.ToString();
    }

}
