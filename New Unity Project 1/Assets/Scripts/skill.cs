using UnityEngine;
using System.Collections;
using System;

public class skill{
    public float size;
    public float baseSize;
    public float duration;
    public float baseDuration;
    public float coolDown;
    public float baseCoolDown;
    public float currentCool;
    public int level = 1;
    public float power;
    public float basePower;
    public bool active;
    public bool cooling;
    public string skillName;
    public int cost;
    public int baseCost;
    public string desc = "description missing";
    public string skillNameShown;
    public skillType type;
    public enum skillType
    {
        Shoot,
        Place,
        Passive,
        Buff   
    }

    public skill(string n, skillType t, float s, float d, float cd, float p, int c)
    {
        skillName = n;
        skillNameShown = n;
        type = t;
        size = s;
        baseSize = s;
        duration = d;
        baseDuration = d;
        coolDown = cd;
        baseCoolDown = cd;
        power = p;
        basePower = p;
        active = false;
        level = 1;
        cost = c;
        baseCost = c;
        cooling = false;
        currentCool = 0;
    }

    public skill(string n, skillType t, float s, float d, float cd, float p, int c,string ns)
    {
        skillName = n;
        skillNameShown = ns;
        type = t;
        size = s;
        baseSize = s;
        duration = d;
        baseDuration = d;
        coolDown = cd;
        baseCoolDown = cd;
        power = p;
        basePower = p;
        active = false;
        level = 1;
        cost = c;
        baseCost = c;
        cooling = false;
        currentCool = 0;
    }
}
