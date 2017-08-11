using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class globalData{
    public static List<skill> allSkills = new List<skill>();
    public static GameObject mainCanvas = GameObject.FindWithTag("mainCanvas");
    public static GameObject player;
    public static skillContainer theSkillContainer;
    public static levelMaster theLevelMaster;
    public static float leftEdge;
    public static float rightEdge;
    public static float topEdge;
    public static float bottomEdge;
    public static Serializer theSerializer;
}
