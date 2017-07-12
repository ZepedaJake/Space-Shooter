using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Material playerMaterial;
    public GameObject playerPreview;
    
    
    public Light playerGlowPreview;
    public int openMenu = -1;

    public GameObject skillMenuMain;
    public GameObject customizeMenu;

    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;

    
    // Use this for initialization
    void Start () {
        playerMaterial.color = Color.white;
        playerGlowPreview.color = Color.white;

        //skillContainer = GameObject.Find("Skill Container").GetComponent<skillContainer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Customize()
    {
        //open customize menu
        int openMenu = 0;
        customizeMenu.SetActive(true);
    }

    public void PickSkills()
    {
        //open pick skill menu
        int openMenu = 1;
        skillMenuMain.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("Main");
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
        globalData.theSkillContainer.playerMaterialColor = playerMaterial.color;
        globalData.theSkillContainer.playerGlowColor = playerGlowPreview.color;
        //skillContainer.GivePlayerSkills();
        
    }
}
