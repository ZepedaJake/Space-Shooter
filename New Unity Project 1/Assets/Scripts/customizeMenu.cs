using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customizeMenu : MonoBehaviour {
    public Slider bodyR;
    public Slider bodyG;
    public Slider bodyB;
    public Slider glowR;
    public Slider glowG;
    public Slider glowB;
    public Toggle glowToggle;
    public MainMenu main;
    
	// Use this for initialization
	void Start () {
        main = GameObject.Find("Main Camera").GetComponent<MainMenu>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       main.playerMaterial.color = new Color((bodyR.value/255), (bodyG.value/255), (bodyB.value/255));
        main.playerGlowPreview.color = new Color((glowR.value/255),glowG.value/255,glowB.value/255);
        main.playerGlowPreview.enabled = glowToggle.isOn;
	}

    public void Close()
    {
        main.openMenu = -1;
        gameObject.SetActive(false);
    }
}
