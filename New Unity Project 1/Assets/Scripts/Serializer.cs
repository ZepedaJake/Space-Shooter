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
    playerScript player;

    public void Start()
    {
        if (!Directory.Exists(saveData))
        {
            Directory.CreateDirectory(saveData);
        }
        player = globalData.player.GetComponent<playerScript>();
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

            playerSkill1 = player.playerSkills[0],
            playerSkill2 = player.playerSkills[1],
            playerSkill3 = player.playerSkills[2],
            playerSkill4 = player.playerSkills[3],

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
}
