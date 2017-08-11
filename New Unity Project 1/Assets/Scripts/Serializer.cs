using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Serializer : MonoBehaviour {
    string saveFile;
    string loadFile;
    string fileName;
    //probably change this later
    public string saveData = "Assets/Resources/Save/";
    public playerScript player;

    public void Start()
    {
        globalData.theSerializer = gameObject.GetComponent<Serializer>();
        if (!Directory.Exists(saveData))
        {
            Directory.CreateDirectory(saveData);
        }       
    }

    private void FixedUpdate()
    {
        if(player == null)
        {
            try
            {
                player = globalData.player.GetComponent<playerScript>();
            }
            catch
            {

            }            
        }
    }

    public void Save()
    {
        
        playerSaveData playerData = new playerSaveData()
        {
            level = player.level,
            str = player.str,
            agi = player.agi,
            con = player.con,
            luk = player.luk,
            eng = player.eng,

            maxEnergy = player.maxEnergy,
            currentEnergy = player.currentEnergy,
            maxHealth = player.maxHealth,
            currentHealth = player.currentHealth,
            currentExp = player.currentExp,
            expTillLevel = player.expTillLevel,


            skillPoints = player.skillPoints,
            statPoints = player.statPoints,

            playerSkill1Name = player.playerSkills[0].skillName,
            playerSkill2Name = player.playerSkills[1].skillName,
            playerSkill3Name = player.playerSkills[2].skillName,
            playerSkill4Name = player.playerSkills[3].skillName,

            playerSkill1Level = player.playerSkills[0].level,
            playerSkill2Level = player.playerSkills[1].level,
            playerSkill3Level = player.playerSkills[2].level,
            playerSkill4Level = player.playerSkills[3].level,

            playerColor = globalData.theSkillContainer.playerMaterialColor,
            playerGlowColor = globalData.theSkillContainer.playerGlowColor,

            money = globalData.theLevelMaster.money,
            kills = globalData.theLevelMaster.kills,
            upCounter = globalData.theLevelMaster.upCounter,
            bossCounter = globalData.theLevelMaster.bossCounter,
            killsTillNextBoss = globalData.theLevelMaster.bossSpawnCounter,
            difficulty = globalData.theLevelMaster.difficulty,


        };
        string json = JsonUtility.ToJson(playerData);

        string fileName = Path.Combine(saveData, "player.json");

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        File.WriteAllText(fileName, json);       
        Debug.Log(json);
        Debug.Log(fileName);
    }

    public void LoadSkills()
    {
        fileName = Path.Combine(saveData, "player.json");
        loadFile = File.ReadAllText(fileName);

        playerSaveData load = JsonUtility.FromJson<playerSaveData>(loadFile);

        globalData.theSkillContainer.LoadSkill(load.playerSkill1Name, 0, load.playerSkill1Level);
        globalData.theSkillContainer.LoadSkill(load.playerSkill2Name, 1, load.playerSkill2Level);
        globalData.theSkillContainer.LoadSkill(load.playerSkill3Name, 2, load.playerSkill3Level);
        globalData.theSkillContainer.LoadSkill(load.playerSkill4Name, 3, load.playerSkill4Level);
    }

    public void LoadPlayerData()
    {
        fileName = Path.Combine(saveData, "player.json");
        loadFile = File.ReadAllText(fileName);

        playerSaveData load = JsonUtility.FromJson<playerSaveData>(loadFile);

        player.level = load.level;

        player.maxHealth = load.maxHealth;
        player.currentHealth = (int)load.currentHealth;
        player.maxEnergy = load.maxEnergy;
        player.currentEnergy = load.currentEnergy;
        player.expTillLevel = load.expTillLevel;
        player.currentExp = load.currentExp;

        player.str = load.str;
        player.agi = load.agi;
        player.con = load.con;
        player.eng = load.eng;
        player.luk = load.luk;

        player.skillPoints = load.skillPoints;
        player.statPoints = load.statPoints;

        player.playerSkills[0].level = globalData.theSkillContainer.loadedSkillLevels[0];
        player.playerSkills[1].level = globalData.theSkillContainer.loadedSkillLevels[1];
        player.playerSkills[2].level = globalData.theSkillContainer.loadedSkillLevels[2];
        player.playerSkills[3].level = globalData.theSkillContainer.loadedSkillLevels[3];

        UpdateSkillValues(player.playerSkills[0], player.playerSkills[0].level);
        UpdateSkillValues(player.playerSkills[1], player.playerSkills[1].level);
        UpdateSkillValues(player.playerSkills[2], player.playerSkills[2].level);
        UpdateSkillValues(player.playerSkills[3], player.playerSkills[3].level);

    }

    public void UpdateSkillValues(skill k, int num)
    {
        int tempLvl = num - 1;
        switch (k.skillName)
        {
            case "SlowField":
                k.basePower += (5 * tempLvl);
                k.baseDuration += (2 * tempLvl);
                k.baseSize += (1 * tempLvl);
                k.baseCost -= (1 * tempLvl);
                break;
            case "Heal":
                k.basePower += (5 * tempLvl);
                k.baseDuration += (1 * tempLvl);
                k.baseCost += (7 * tempLvl);
                k.baseCoolDown += (3 * tempLvl);
                break;
            case "Nuke":
                k.basePower += (.5f * tempLvl);
                k.baseCost += (15 * tempLvl);
                k.baseSize += (3 * tempLvl);
                k.baseCoolDown -= (2 * tempLvl);
                break;
            case "Buff":
                k.basePower += (20 * tempLvl);
                k.baseCost += (5 * tempLvl);
                k.baseDuration += (5 * tempLvl);

                break;
            default:
                break;
        }
    }
}
