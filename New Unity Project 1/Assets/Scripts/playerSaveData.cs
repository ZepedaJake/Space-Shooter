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

    //should have a lvl attribute that cna be saved
    public skill playerSkill1;
    public skill playerSkill2;
    public skill playerSkill3;
    public skill playerSkill4;

    public Color playerColor;
    public Color playerGlowColor;

    public int money;
    public int kills;
    public int upCounter;
    public int bossCounter;
    public int killsTillNextBoss;
    public float difficulty;

}
