using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillMenuMain : MonoBehaviour {
    public GameObject skillButton;
    public Text skillDescription;
    public MainMenu main;
    public Text skillDescriptionTitle;
    public skillContainer skillContainer;
    skill highlightedSkill;

    // Use this for initialization
    void Start () {
        main = GameObject.Find("Main Camera").GetComponent<MainMenu>();
        skillContainer = GameObject.Find("SkillContainer").GetComponent<skillContainer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        int y = 160;
        int x = -50;
        foreach(skill s in globalData.allSkills)
        {

            if (y < -160)
            {
                x += 100;
                y = 160;
            }
            GameObject newSkill = (GameObject)Instantiate(skillButton, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
            newSkill.transform.SetParent(gameObject.transform, false);
            newSkill.GetComponent<skillAddButtonMain>().skillName.text = s.skillName;
            newSkill.GetComponent<Image>().sprite = Resources.Load<Sprite>(s.skillName);
            AddListener(newSkill.GetComponent<Button>(), s);                        
            y -= 80;
            
        }
    }

    void AddListener(Button b, skill s)
    {

        b.onClick.AddListener(() => { ShowSkill(s); });

    }

    public void ShowSkill(skill s)
    {
        highlightedSkill = s;
        skillDescription.text = s.desc;
        skillDescriptionTitle.text = s.skillNameShown + " Description";
    }

    public void Close()
    {
        main.openMenu = -1;
        gameObject.SetActive(false);
    }

    public void SetToSkill1()
    {   
        //move skill from another slot
        for(int x = 0; x<4;x++)
        {
            if(skillContainer.chosenSkills[x] == highlightedSkill)
            {
                skillContainer.chosenSkills[x] = null;
            }
        }
        skillContainer.chosenSkills[0] = highlightedSkill;
        UpdateSelectedSkills();
    }

    public void SetToSkill2()
    {
        for (int x = 0; x < 4; x++)
        {
            if (skillContainer.chosenSkills[x] == highlightedSkill)
            {
                skillContainer.chosenSkills[x] = null;
            }
        }
        skillContainer.chosenSkills[1] = highlightedSkill;
        UpdateSelectedSkills();
    }

    public void SetToSkill3()
    {
        for (int x = 0; x < 4; x++)
        {
            if (skillContainer.chosenSkills[x] == highlightedSkill)
            {
                skillContainer.chosenSkills[x] = null;
            }
        }
        skillContainer.chosenSkills[2] = highlightedSkill;
        UpdateSelectedSkills();
    }

    public void SetToSkill4()
    {
        for (int x = 0; x < 4; x++)
        {
            if (skillContainer.chosenSkills[x] == highlightedSkill)
            {
                skillContainer.chosenSkills[x] = null;
            }
        }
        skillContainer.chosenSkills[3] = highlightedSkill;
        UpdateSelectedSkills();
    }

    void UpdateSelectedSkills()
    {
        //look if skill is null, if yes, blank pic, if no find correct pic
        if (skillContainer.chosenSkills[0] != null)
            main.skill1.GetComponent<Image>().sprite = Resources.Load<Sprite>(skillContainer.chosenSkills[0].skillName);
        else
            main.skill1.GetComponent<Image>().sprite = null;

        if (skillContainer.chosenSkills[1] != null)
            main.skill2.GetComponent<Image>().sprite = Resources.Load<Sprite>(skillContainer.chosenSkills[1].skillName);
        else
            main.skill2.GetComponent<Image>().sprite = null;

        if(skillContainer.chosenSkills[2] != null)
            main.skill3.GetComponent<Image>().sprite = Resources.Load<Sprite>(skillContainer.chosenSkills[2].skillName);
        else
            main.skill3.GetComponent<Image>().sprite = null;

        if(skillContainer.chosenSkills[3] != null)
            main.skill4.GetComponent<Image>().sprite = Resources.Load<Sprite>(skillContainer.chosenSkills[3].skillName);
        else
            main.skill4.GetComponent<Image>().sprite = null;
    }
}
