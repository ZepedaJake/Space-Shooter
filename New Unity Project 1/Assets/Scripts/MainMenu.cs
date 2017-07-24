using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Text startAlertText;

    //for random skills button
    int[] randomNums = new int[4];

    // Use this for initialization
    void Start() {
        playerMaterial.color = Color.white;
        playerGlowPreview.color = Color.white;

        //skillContainer = GameObject.Find("Skill Container").GetComponent<skillContainer>();
    }

    // Update is called once per frame
    void Update() {

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
        //if skills are not blank start game
        bool doStart = false;

        //check if skillls were chosen
        for(int x = 0; x<4;x++)
        {
            if(globalData.theSkillContainer.chosenSkills[x] != null)
            {
                doStart = true;
            }
            else
            {
                doStart = false;
                break;
            }

            
        }

        if (doStart)
        {
            SceneManager.UnloadSceneAsync("Main");
            SceneManager.LoadScene("Play", LoadSceneMode.Single);
            globalData.theSkillContainer.playerMaterialColor = playerMaterial.color;
            globalData.theSkillContainer.playerGlowColor = playerGlowPreview.color;
            //skillContainer.GivePlayerSkills();
        }
        else
        {
            startAlertText.text = "Unable to start, one or more skills were not chosen";
        }

    }

    public void RandomColors()
    {
        playerMaterial.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        playerGlowPreview.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void RandomSkills()
    {
        //pick 4 random skills

        //first random skill
        RandomNum(0);
        RandomNum(1);
        RandomNum(2);
        RandomNum(3);

        //after choosing random numbers, set chosen skill in correlation to the random numbers
        globalData.theSkillContainer.chosenSkills[0] = globalData.allSkills[randomNums[0]];
        globalData.theSkillContainer.chosenSkills[1] = globalData.allSkills[randomNums[1]];
        globalData.theSkillContainer.chosenSkills[2] = globalData.allSkills[randomNums[2]];
        globalData.theSkillContainer.chosenSkills[3] = globalData.allSkills[randomNums[3]];
        UpdateSelectedSkills();
    }

    void RandomNum(int x)
    {
        randomNums[x] = Random.Range(0, globalData.allSkills.Count);
        
        //dont check against itself
        for(int y = 3; y>-1; y--)
        {
            if(y != x)
            {
                //if another slot has this number
                if(randomNums[x] == randomNums[y])
                {
                    //repick number
                    RandomNum(x);
                }
            }
        }
    }

    void UpdateSelectedSkills()
    {
        //look if skill is null, if yes, blank pic, if no find correct pic
        if (globalData.theSkillContainer.chosenSkills[0] != null)
            skill1.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalData.theSkillContainer.chosenSkills[0].skillName);
        else
            skill1.GetComponent<Image>().sprite = null;

        if (globalData.theSkillContainer.chosenSkills[1] != null)
            skill2.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalData.theSkillContainer.chosenSkills[1].skillName);
        else
            skill2.GetComponent<Image>().sprite = null;

        if (globalData.theSkillContainer.chosenSkills[2] != null)
            skill3.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalData.theSkillContainer.chosenSkills[2].skillName);
        else
            skill3.GetComponent<Image>().sprite = null;

        if (globalData.theSkillContainer.chosenSkills[3] != null)
            skill4.GetComponent<Image>().sprite = Resources.Load<Sprite>(globalData.theSkillContainer.chosenSkills[3].skillName);
        else
            skill4.GetComponent<Image>().sprite = null;
    }
}
