using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelMaster : MonoBehaviour {
    public float difficulty = 1;
    public int money = 0;
    public int kills = 0;
    public int lives = 0;

    public Text killsText;
    public Text moneyText;
    public Text livesText;
    public Text levelText;
    public Text expText;
    public Text healthText;
    public Text energyText;
    

    public Slider expBar;
    public Slider healthBar;
    public Slider energyBar;
    public GameObject skillBlock;
    public GameObject[] skills;

    public int upCounter = 50;

    public bool pause = false;
    public bool stats = false;
    public bool shop = false;
    public bool skillOpen = false;

    public GameObject pauseMenu;
    public GameObject statMenu;
    public GameObject shopMenu;
    public GameObject skillMenu;

    playerScript player;

    public Text[] allotStats;
    int strTemp = 0;
    int agiTemp = 0;
    int conTemp = 0;
    int lukTemp = 0;
    int engTemp = 0;

    public Text[] txtStats;
    public Text[] txtStatsTemp;

    public GameObject[] enemies;
    public double[] spawnTimes;
    public GameObject[] bosses;
    public bool spawnBoss = true;
    public int bossSpawnCounter = 0;
    public int bossCounter = 0;
    

    
	// Use this for initialization
	void Start () {
        skills = new GameObject[5];
        spawnTimes = new double[enemies.Length];
        for (int x = 0; x < enemies.Length; x++)
        {
            spawnTimes[x] = enemies[x].GetComponent<enemyBase>().spawnTimer;
        }
        //spawnTimes[0] = enemies[0].GetComponent<enemyBase>().spawnTimer;
        globalData.theLevelMaster = gameObject.GetComponent<levelMaster>();
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        globalData.leftEdge = GameObject.FindWithTag("leftEdge").transform.position.x + (GameObject.FindWithTag("leftEdge").transform.localScale.x / 2) + .5f;
        globalData.rightEdge = GameObject.FindWithTag("rightEdge").transform.position.x - (GameObject.FindWithTag("rightEdge").transform.localScale.x / 2) - .5f;

        globalData.topEdge = GameObject.FindWithTag("top").transform.position.z - (GameObject.FindWithTag("top").transform.localScale.z / 2) - 22.5f;
        globalData.bottomEdge = GameObject.FindWithTag("bottom").transform.position.z + (GameObject.FindWithTag("bottom").transform.localScale.z / 2) + 12.5f;
        UpdateUI();
        //createSkills();

        Debug.Log(globalData.topEdge);
        Debug.Log(globalData.bottomEdge);
        Debug.Log(globalData.leftEdge);
        Debug.Log(globalData.rightEdge);

    }

    // Update is called once per frame
    void Update() {
        Spawn();
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (stats == false && shop == false && skillOpen == false)
            {
                pause = !pause;
                pauseMenu.SetActive(pause);
            }
            //close the shop
            else if(skillOpen == false && stats == false && pause == true)
            {
                shop = !shop;
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                shopMenu.SetActive(shop);
            }
            //close stats
            else if(skillOpen == false && shop == false && pause == true)
            {
                stats = !stats;
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                statMenu.SetActive(stats);

            }
            //close skills 
            else if (stats == false && shop == false && pause == true)
            {
                skillOpen = !skillOpen;
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                skillMenu.SetActive(skillOpen);

            }
        }

        if (kills >= upCounter)
        {
            difficulty = (float)(difficulty * 1.618);
            upCounter *= 2;
            upCounter++;
        }

        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        //show all skills
        if(Input.GetKeyUp(KeyCode.K))
        {
            Debug.Log("skills:");
            foreach(skill k in globalData.allSkills)
            {
                Debug.Log(k.skillName);
            }
           
        }
        //show player skills
        if (Input.GetKeyUp(KeyCode.L))
        {
            Debug.Log("Player skills:");
            
                foreach( skill s in player.playerSkills)
                {
                    
                        Debug.Log(s.skillName);
                    
                }
        }
        //force expand space
        if(Input.GetKeyUp(KeyCode.B))
        {
            ExpandSpace();
        }
        UpdateSkillCoolDown();
    }

    
    //spawn in enemies
    void Spawn()
    {

        for (int x = 0; x < enemies.Length; x++)
        {
            spawnTimes[x] -= Time.deltaTime;

            if (spawnTimes[x] <= 0)
            {
                Instantiate(enemies[x], new Vector3(Random.Range(globalData.leftEdge, globalData.rightEdge), 0, globalData.topEdge + 10), new Quaternion(0, 180, 0, 0));
                spawnTimes[x] = enemies[x].GetComponent<enemyBase>().spawnTimer - ((difficulty - 1) * .1);

                if (spawnTimes[x] < enemies[x].GetComponent<enemyBase>().minSpawnTime)
                {
                    spawnTimes[x] = enemies[x].GetComponent<enemyBase>().minSpawnTime;
                }
            }
        }
        //every 100 kills spawn a boss
        if(bossSpawnCounter>=100)
        {
            spawnBoss = false;
            bossSpawnCounter = 0;
            //instantiate a new boss each time
            Instantiate(bosses[bossCounter], new Vector3(0, -1, globalData.topEdge - 5), new Quaternion(0, 0, 0, 0));
        }
        /*if(kills == 100 && spawnBoss)
        {
            spawnBoss = false;
            Instantiate(bosses[0], new Vector3(0, 0, globalData.topEdge - 5), new Quaternion(0, 0, 0, 0));
        }*/
    }

    public void EnergyUpdate()
    {
        energyText.text = (int)player.currentEnergy + "/" + player.maxEnergy;
        energyBar.maxValue = player.maxEnergy;
        energyBar.value = player.currentEnergy;
    }

    public void UpdateUI()
    {
        killsText.text = "Kills: " + kills;
        moneyText.text = "$: " + money;
        livesText.text = "Lives: " + lives;
        levelText.text = "Level: " + player.level;
        expText.text = player.currentExp + " / " + player.expTillLevel;
        expBar.maxValue = player.expTillLevel;
        expBar.value = player.currentExp;
        healthText.text = player.currentHealth +" / " + player.maxHealth;
        healthBar.maxValue = player.maxHealth;
        healthBar.value = player.currentHealth;
        EnergyUpdate();

        txtStats[0].text = player.damage.ToString();
        txtStats[1].text = player.drag.ToString();
        txtStats[2].text = player.speed.ToString();
        txtStats[3].text = player.armor.ToString();
        txtStats[4].text = player.projectileSpeed.ToString();
        txtStats[5].text = player.fireRate.ToString();
        txtStats[6].text = player.critChance.ToString();
        txtStats[7].text = player.critMult.ToString();
        txtStats[8].text = player.evade.ToString();
        txtStats[9].text = player.energyDamage.ToString();
        txtStats[10].text = player.maxHealth.ToString();
        txtStats[11].text = player.maxEnergy.ToString();

        txtStatsTemp[0].text = "+ " + (strTemp * 7).ToString();
        txtStatsTemp[1].text = "+ " +  ((float)(agiTemp * .1)).ToString();
        txtStatsTemp[2].text = "+ " +  ((float)(agiTemp * .7)).ToString();
        txtStatsTemp[3].text = "+ " +  ((strTemp * 2) + (conTemp * 10)).ToString();
        txtStatsTemp[4].text = "+ " +  ((float)(strTemp * .03)).ToString();
        txtStatsTemp[5].text = "0";
        txtStatsTemp[6].text = "+ " +  (lukTemp * .5f).ToString();
        txtStatsTemp[7].text = "+ " +  ((float)(strTemp * .03) + (lukTemp * .07f)).ToString();
        txtStatsTemp[8].text = "+ " +  ((float)(agiTemp * .1) + (lukTemp * .3f)).ToString();
        txtStatsTemp[9].text = "+ " +  (engTemp * 3).ToString();
        txtStatsTemp[10].text = "+ " +  (conTemp * 20).ToString();
        txtStatsTemp[11].text = "+ " +  (engTemp * 15).ToString();


        allotStats[5].text = "Points: " + player.statPoints;
        allotStats[0].text = strTemp.ToString();
        allotStats[6].text = player.str.ToString();
        allotStats[1].text = agiTemp.ToString();
        allotStats[7].text = player.agi.ToString();
        allotStats[2].text = conTemp.ToString();
        allotStats[8].text = player.con.ToString();
        allotStats[4].text = lukTemp.ToString();
        allotStats[10].text = player.luk.ToString();
        allotStats[3].text = engTemp.ToString();
        allotStats[9].text = player.eng.ToString();

        /*for(int x = 0; x < skills.Length; x++)
        {
            skills[x].GetComponent<aSkill>().cost.text = (player.playerSkills[x].cost).ToString();
        }*/
    }

    public void ShowStats()
    {
        stats = !stats;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        statMenu.SetActive(stats);
    }
    
    public void ShowShop()
    {
        shop = !shop;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        shopMenu.SetActive(shop);
        
    }

    public void ShowSkills()
    {
        skillOpen = !skillOpen;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        skillMenu.SetActive(skillOpen);
    }


    public void StrUp()
    {
        if (player.statPoints > 0)
        {
            player.statPoints--;
            strTemp++;
            UpdateUI();
            allotStats[0].text = strTemp.ToString();
            allotStats[0].color = Color.green;
            
        }
    }

    public void StrDown()
    {
        if(strTemp>0)
        {
            strTemp--;
            player.statPoints++;
            UpdateUI();
            allotStats[0].text = strTemp.ToString();
            if(strTemp == 0)
            {               
                allotStats[0].color = Color.black;
            }
        }
    }

    public void AgiUp()
    {
        if (player.statPoints > 0)
        {
            player.statPoints--;
            agiTemp++;
            UpdateUI();
            allotStats[1].text = agiTemp.ToString();
            allotStats[1].color = Color.green;

        }
    }
    public void AgiDown()
    {
        if (agiTemp > 0)
        {
            agiTemp--;
            player.statPoints++;
            UpdateUI();
            allotStats[1].text = agiTemp.ToString();
            if (agiTemp == 0)
            {
                allotStats[1].color = Color.black;
            }
        }
    }

    public void ConUp()
    {
        if (player.statPoints > 0)
        {
            player.statPoints--;
            conTemp++;
            UpdateUI();
            allotStats[2].text = conTemp.ToString();
            allotStats[2].color = Color.green;

        }
    }
    public void ConDown()
    {
        if (conTemp > 0)
        {
            conTemp--;
            player.statPoints++;
            UpdateUI();
            allotStats[2].text = conTemp.ToString();
            if (conTemp == 0)
            {
                allotStats[2].color = Color.black;
            }
        }
    }

    public void LukUp()
    {
        if (player.statPoints > 0)
        {
            player.statPoints--;
            lukTemp++;
            UpdateUI();
            allotStats[4].text = lukTemp.ToString();
            allotStats[4].color = Color.green;

        }
    }

    public void LukDown()
    {
        if (lukTemp > 0)
        {
            lukTemp--;
            player.statPoints++;
            UpdateUI();
            allotStats[4].text = lukTemp.ToString();
            if (lukTemp == 0)
            {
                allotStats[4].color = Color.black;
            }
        }
    }

    public void EngUp()
    {
        if (player.statPoints > 0)
        {
            player.statPoints--;
            engTemp++;
            UpdateUI();
            allotStats[3].text = engTemp.ToString();
            allotStats[3].color = Color.green;

        }
    }

    public void EngDown()
    {
        if (engTemp > 0)
        {
            engTemp--;
            player.statPoints++;
            UpdateUI();
            allotStats[3].text = engTemp.ToString();
            if (engTemp == 0)
            {
                allotStats[3].color = Color.black;
            }
        }
    }
    public void ApplyStats()
    {
        player.str += strTemp;
        player.agi += agiTemp;
        player.con += conTemp;
        player.luk += lukTemp;
        player.eng += engTemp;
        
        strTemp = 0;
        agiTemp = 0;
        conTemp = 0;
        lukTemp = 0;
        engTemp = 0;

        for(int x = 0; x < allotStats.Length; x++)
        {
            allotStats[x].color = Color.black;
        }
        player.UpdateStats();
        UpdateUI();

    }

    /*void createSkills()
    {
        globalData.allSkills.Add(new skill("SlowField", skill.skillType.Place, 7, 5, 30, 25,20));
        globalData.allSkills.Add(new skill("Heal", skill.skillType.Buff, 0, 10, 60, 5, 25));
        globalData.allSkills.Add(new skill("Nuke", skill.skillType.Shoot, 10, .5f, 30, 1.5f, 50));
        globalData.allSkills.Add(new skill("Test", skill.skillType.Place, 1, 1, 1, 1,1));
        globalData.allSkills.Add(new skill("Test2", skill.skillType.Place, 1, 1, 1, 1,1));
        globalData.allSkills.Add(new skill("Test3", skill.skillType.Place, 1, 1, 1, 1,1));

        SetDescriptionDetails("SlowField");
        SetDescriptionDetails("Heal");
        SetDescriptionDetails("Nuke");
    }
    void SetDescriptionDetails(string s)
    {
        skill self = globalData.allSkills.Find(x => x.skillName.Contains(s));
        switch (s)
        {
            case "SlowField":
                {
                    setSkillDesc(self, "Place a "+self.size+"m bubble that slows enemies by "+self.power+"% for "+self.duration+" seconds.");
                    break;
                }
            case "Heal":
                {
                    setSkillDesc(self, "Heal " + self.power + " points of health per second, for " + self.duration + " seconds.");
                        break;
                }
                case"Nuke":
                {
                    setSkillDesc(self, "Launch a nuke, dealing " + self.power + " damage in a " + self.size + "m radius.");
                    break;
                }
                
            default:
                break;
        }
    }
    
    
    void setSkillDesc(skill s, string description)
    {
       
        s.desc = description;
    }*/

    //make cooldown boxes
    public void Skills()
    {
        Debug.Log("setting skills up");
        int y = 180;
        int x = 0;

        foreach(skill s in player.playerSkills)
        {            
            Debug.Log(s.skillName);
            GameObject newSkill = (GameObject)Instantiate(skillBlock, new Vector3(20, y, 0), new Quaternion(0, 0, 0, 0));
            skills[x] = newSkill;
            newSkill.transform.SetParent(GameObject.FindWithTag("skillHolder").transform, false);
            newSkill.tag = "skillBlock";
            newSkill.GetComponent<aSkill>().coolDownText.text = s.currentCool.ToString();
            newSkill.GetComponent<aSkill>().cost.text = s.cost.ToString();
            newSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>(s.skillName);
            newSkill.GetComponent<Image>().color = Color.white;
            y -= 40;           
            x++;   
        }

        //UpdateUI();
    }

    void UpdateSkillCoolDown()
    {
        try
        {
            for (int x = 0; x < skills.Length; x++)
            {
                if (player.playerSkills[x].currentCool >= .05f)
                {
                    skills[x].GetComponent<aSkill>().coolDownText.text = ((int)player.playerSkills[x].currentCool).ToString();
                }
                else
                {
                    skills[x].GetComponent<aSkill>().coolDownText.text = "";
                }

            }
        }
        catch { }
    }
    public void BossProgression()
    {
        if (bossCounter < bosses.Length - 1)
            bossCounter++;
        else
            bossCounter = 0;
    }
    public void ExpandSpace()
    {
        GameObject left = GameObject.FindWithTag("leftEdge");
        GameObject right = GameObject.FindWithTag("rightEdge");
        GameObject top = GameObject.FindWithTag("top");
        GameObject bottom = GameObject.FindWithTag("bottom");

        left.transform.position = new Vector3(left.transform.position.x - 9, left.transform.position.y, left.transform.position.z);
        right.transform.position = new Vector3(right.transform.position.x + 9, right.transform.position.y,right.transform.position.z);
        top.transform.position = new Vector3(top.transform.position.x,top.transform.position.y,top.transform.position.z + 4.5f);
        bottom.transform.position = new Vector3(bottom.transform.position.x,bottom.transform.position.y, bottom.transform.position.z-4.5f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 8, gameObject.transform.position.z);

        left.transform.localScale += new Vector3(6, 0, 8);
        right.transform.localScale += new Vector3(6, 0, 8);
        top.transform.localScale += new Vector3(10, 0, 0);
        bottom.transform.localScale += new Vector3(10, 0, 0);

        globalData.leftEdge = left.transform.position.x + (left.transform.localScale.x / 2) + .5f;
        globalData.rightEdge = right.transform.position.x - (right.transform.localScale.x / 2) - .5f;
        globalData.topEdge = top.transform.position.z - (top.transform.localScale.z / 2) - 22.5f;
        globalData.bottomEdge = bottom.transform.position.z + (bottom.transform.localScale.z / 2) + 12.5f;
    }


}
