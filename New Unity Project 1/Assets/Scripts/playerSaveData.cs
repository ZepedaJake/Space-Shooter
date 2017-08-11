using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class playerSaveData
{
    public int level;
    public int str;
    public int agi;
    public int con;
    public int luk;
    public int eng;

    public int maxHealth;
    public float currentHealth;
    public int maxEnergy;
    public float currentEnergy;
    public int currentExp;
    public int expTillLevel;

    public int skillPoints;
    public int statPoints;

    //should have a lvl attribute that can be saved
    public string playerSkill1Name;
    public int playerSkill1Level;
    public string playerSkill2Name;
    public int playerSkill2Level;
    public string playerSkill3Name;
    public int playerSkill3Level;
    public string playerSkill4Name;
    public int playerSkill4Level;

    public Color playerColor;
    public Color playerGlowColor;

    public int money;
    public int kills;
    public int upCounter;
    public int bossCounter;
    public int killsTillNextBoss;
    public float difficulty;

}
