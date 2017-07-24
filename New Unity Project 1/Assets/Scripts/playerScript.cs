using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerScript : MonoBehaviour {
    
    public GameObject bullet;
    public AudioClip shoot;
    Rigidbody rb;
    levelMaster theLevelMaster;
    public GameObject shopHolder;
    shopScript theShop;
    public List<skill> playerSkills = new List<skill>();
    public GameObject reticle;
    

    public int currentExp;
    public int expTillLevel = 100;
    public int level = 1;

    public int maxHealth;
    public int baseMaxHealth = 50;
    public int currentHealth;

    public int maxEnergy;
    public int baseMaxEnergy = 50;
    public float currentEnergy;

    public int points;
    public int allottetPoints;
    public int str = 0;
    public int agi = 0;
    public int con = 0;
    public int eng = 0;
    public int luk = 0;

    public float baseDamage = 100;
    public float baseDrag = 2;
    public float baseSpeed = 100;
    public float baseArmor = 0;
    public float baseProjectileSpeed = .5f;
    public float baseFireRate = 1;
    public float baseCritChance = 5;
    public float baseCritMult = 1.5f;
    public float baseEvade = 5;
    public float baseEnergyDamage = 20;
    public int fireRateLevel = 1;

    public float damage;
    public float drag;
    public float speed;
    public float armor;
    public float projectileSpeed;
    public float fireRate;
    public float critChance;
    public float critMult;
    public float evade;
    public float energyDamage;
    
    public double fireTimer = 0;
    public int statPoints = 0;
    public int skillPoints = 0;

    //upgrades
    public bool piercingShots = false;
    public bool perfectThrusters = false;

    //skills
    public int skillChoice = -1;

    
    public Vector3 setPos;

    // Use this for initialization
    void Start()
    {
        globalData.player = gameObject;
        rb = gameObject.GetComponent<Rigidbody>();
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();
        theShop = shopHolder.GetComponent<shopScript>();
        

        currentHealth = maxHealth;
        currentEnergy = maxEnergy;

        Invoke("SetSkills",.1f);

        UpdateSkillValues();
        UpdateStats();
        globalData.theSkillContainer.SetPlayerColor();
    }
    void SetSkills()
    {
        //playerSkills.Add(globalData.allSkills.Find(x => x.skillName.Contains("SlowField")));
        //.Add(globalData.allSkills.Find(x => x.skillName.Contains("Heal")));
        //playerSkills.Add(globalData.allSkills.Find(x => x.skillName.Contains("Nuke")));
        globalData.theSkillContainer.GivePlayerSkills();

        theLevelMaster.Skills();
        UpdateSkillValues(); 
    }

    

	// Update is called once per frame
	void Update () {
        Move(speed);
        Fire();
        Skill();
        
        if(currentEnergy < maxEnergy)
        {
            currentEnergy += Time.deltaTime * Time.timeScale;
            theLevelMaster.EnergyUpdate();
        }
        
        foreach(skill s in playerSkills)
        {
            if (s.cooling == true)
            {
                s.currentCool -= Time.deltaTime * Time.timeScale;
                if(s.currentCool <= 0)
                {
                    s.cooling = false;
                }
            }
            
        }
    }

    

    void Move(float x)
    {
        if(perfectThrusters)
        {
            //rb.AddForce(rb.velocity * -1);
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (gameObject.transform.position.z < globalData.topEdge)
                    gameObject.transform.Translate(Vector3.forward * x/100 * Time.timeScale);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (gameObject.transform.position.z > globalData.bottomEdge)
                    gameObject.transform.Translate(Vector3.back * x/100 * Time.timeScale);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if(gameObject.transform.position.x > globalData.leftEdge)
                gameObject.transform.Translate(Vector3.left * x/100 * Time.timeScale);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (gameObject.transform.position.x < globalData.rightEdge)
                gameObject.transform.Translate(Vector3.right * x/100 * Time.timeScale);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rb.AddForce(Vector3.forward * x);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector3.back * x);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector3.left * x);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector3.right * x);
            }
        }
        
    }

    void Fire()
    {
        
        if(fireTimer <= 0 && !theLevelMaster.pause)
        {
            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            
            gameObject.GetComponent<AudioSource>().PlayOneShot(shoot,.6f);
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void Exp(int x)
    {
        currentExp += x;
        while(currentExp >= expTillLevel)
        {
            currentExp -= expTillLevel;
            LevelUp();
        }

    }

    void LevelUp()
    {
        expTillLevel = (int)(expTillLevel * 1.618);
        level++;       
        
        statPoints += 5;
        skillPoints += 1;
        
        UpdateStats();
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;     
        theLevelMaster.UpdateUI();
    }

    public void UpdateStats()
    {
        damage = baseDamage + str * 7;
        drag = baseDrag + (float)(agi * .1);

        speed = baseSpeed + (float)(agi * .7);
        armor = baseArmor + (str * 2) + (con * 10);
        projectileSpeed = baseProjectileSpeed + (float)(str * .03);
        //fireRate = baseFireRate - (float)(agi * .025);
        critChance = baseCritChance + (luk * .5f);
        critMult = baseCritMult + (float)(str * .03) + (luk * .07f);
        evade = baseEvade + (float)(agi * .1) + (luk * .3f);
        energyDamage = baseEnergyDamage + (eng * 3);
        maxHealth = baseMaxHealth + (int)((level - 1) * 16) + (con * 20);
        maxEnergy = baseMaxEnergy + (int)((level - 1) * 12) + (eng * 15);

        //maxes
        if (drag > 2)
        {
            drag = 2;

            if (!searchShop("Perfect Trusters"))
            {
                theShop.addShopItem("Perfect Thrusters", 10000);
            }         
        }

        /*if (fireRate < .025)
        {
            fireRate = .025;
        }*/

        rb.drag = drag;

        UpdateSkillValues();
    }

    public bool searchShop(string n)
    {
        for (int x = 0; x < theShop.theShopItems.Count; x++)
        {
            if (theShop.theShopItems[x].itemName == "Perfect Thrusters")
            {
                return true;
            }
        }
        return false;
    }

    public void Skill()
    {
        // determines what skill to use based on key pressed
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (playerSkills[0].cooling == false && currentEnergy >= playerSkills[1].cost)
            {
                skillChoice = 0;
                //Debug.Log("skill1");
            }

            //Debug.Log("skill cooling" + playerSkills[0].currentCool.ToString()+ " seconds left");           
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (playerSkills[1].cooling == false && currentEnergy >= playerSkills[1].cost)
            {
                skillChoice = 1;
                //currentEnergy -= playerSkills[1].cost;
                //GameObject.FindWithTag("MainCamera").SendMessage(playerSkills[skillChoice].skillName);                               
                //Debug.Log("skill2");
            }

            //Debug.Log("skill cooling" + playerSkills[0].currentCool.ToString() + " seconds left");
        }

        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (playerSkills[2].cooling == false && currentEnergy >= playerSkills[2].cost)
            {
                skillChoice = 2;
                //currentEnergy -= playerSkills[skillChoice].cost;
                //playerSkills[2].currentCool = playerSkills[2].coolDown;
                //GameObject.FindWithTag("MainCamera").SendMessage(playerSkills[skillChoice].skillName);
                //Debug.Log("nuke");
                //playerSkills[skillChoice].cooling = true;

            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (playerSkills[3].cooling == false && currentEnergy >= playerSkills[3].cost)
            {
                skillChoice = 3;
                //currentEnergy -= playerSkills[skillChoice].cost;
                //playerSkills[2].currentCool = playerSkills[2].coolDown;
                //GameObject.FindWithTag("MainCamera").SendMessage(playerSkills[skillChoice].skillName);
                //Debug.Log("nuke");
                //playerSkills[skillChoice].cooling = true;

            }
        }
            //set location of field
            if (skillChoice > -1)
        {
            if(playerSkills[skillChoice].type == skill.skillType.Place)
            {
                PlaceField();
            }
            else
            {
                //take mana
                currentEnergy -= playerSkills[skillChoice].cost;

                //do skill
                GameObject.FindWithTag("MainCamera").SendMessage(playerSkills[skillChoice].skillName);
                
                //cooldown
                playerSkills[skillChoice].currentCool = playerSkills[skillChoice].coolDown;
                playerSkills[skillChoice].cooling = true;
                skillChoice = -1;
               
            }
                
        }

        
        
    }

    public void PlaceField()
    {
        //get position
            var x = Input.mousePosition.x;
            var y = Input.mousePosition.y;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
            {
                setPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, theLevelMaster.gameObject.transform.position.y));
                reticle.transform.position = setPos;
            }
                

        //if you have enough mana. place it
        if (Input.GetMouseButtonUp(0) && skillChoice > -1 && currentEnergy >= playerSkills[skillChoice].cost)
        {
            //sends message to skill handler to perfom skill
            GameObject.FindWithTag("MainCamera").SendMessage(playerSkills[skillChoice].skillName);

            playerSkills[skillChoice].currentCool = playerSkills[skillChoice].coolDown;
            playerSkills[skillChoice].cooling = true;
            currentEnergy -= playerSkills[skillChoice].cost;
            theLevelMaster.EnergyUpdate();
            skillChoice = -1;
            reticle.transform.position = new Vector3(0, -30, 0);
        }
    }

    //Balance
    //allows skills to scale with energy damage stat
    public void UpdateSkillValues()
    {
        foreach (skill s in playerSkills)
        {
            switch(s.type)
            {
                case skill.skillType.Passive:
                    s.power = s.basePower * energyDamage;
                    break;
                case skill.skillType.Place:
                    s.power = s.basePower + (energyDamage / 100);
                    s.duration = s.baseDuration * (1 + (eng / 500));
                    s.size = s.baseSize + (eng / 100);
                    break;
                case skill.skillType.Shoot:
                    s.power = s.basePower * energyDamage;
                    s.size = s.baseSize + (eng / 100);
                    break;
                case skill.skillType.Buff:
                    s.power = s.basePower + (energyDamage/10);
                    s.duration = s.baseDuration * (1 + (eng / 500));
                    break;
                default:
                    break;
            }
           
            

            

            if((1-(eng)/1000) < .25)
            {
                s.coolDown = (float)(s.baseCoolDown * .25);
            }
            else
            {
                s.coolDown = s.baseCoolDown *  (1-(eng/1000));
            }

           

            switch(s.skillName)
            {
                case "SlowField":
                    s.desc = "Place a " + s.size + "m bubble that slows enemies by " + s.power+" % for "+s.duration+" seconds."; 
                     break;
                case "Heal":
                    s.desc = "Heal " + s.power + " points of health per second, for " + s.duration + "seconds.";
                        break;
                case "Nuke":
                    s.desc = "Launch a nuke, dealing " + s.power + " damage in a " + s.size + "m radius.";
                    break;
                case "Buff":            
                    s.desc = "Increase damage by " + s.power + "% for " + s.duration + " seconds.";
                    break;                   
                default:
                    break;
            }
            
        }

    }

}
