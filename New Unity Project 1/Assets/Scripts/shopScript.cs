using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class shopScript : MonoBehaviour {
    public List<shopItems> theShopItems = new List<shopItems>();
    public GameObject itemPanel;
    public Canvas mainCanvas;
    public GameObject parentMenu;
    playerScript player;
    levelMaster theLevelMaster;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<playerScript>();
        theLevelMaster = GameObject.FindWithTag("MainCamera").GetComponent<levelMaster>();

        addShopItem("Laser", 5000);
        addShopItem("Piercing Shots", 6000);
        addShopItem("Fire Rate", 200);
        
        UpdateShop();

        
    }

    void OnEnable()
    {
        UpdateShop();
    }

	// Update is called once per frame
	void Update () {
	    
	}

    public void UpdateShop()
    {
        refreshHeal();
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("shopButton"))
        {
            Destroy(button);
        }
        int y = (Screen.height / 4);
        foreach (shopItems item in theShopItems)
        {
            if(item.unlocked && !item.purchased)
            {
                GameObject newItem = (GameObject)Instantiate(itemPanel, new Vector3(0, y, 0), new Quaternion(0, 0, 0, 0));
                newItem.transform.SetParent(gameObject.transform, false);
                newItem.GetComponent<aShopItem>().textName.text = item.itemName;
                newItem.GetComponent<aShopItem>().textCost.text = item.itemCost.ToString();
                newItem.transform.SetParent(GameObject.Find("ShopItemsPanel").transform, false);
                newItem.tag = "shopButton";
                AddListener(newItem.GetComponentInChildren<Button>(), item.itemName,item.itemCost);
                y -= 52;
            }
            
        }
    }

    void AddListener(Button b, string n, int c)
    {
        b.onClick.AddListener(() => { shopFunction(n, c); });
    }

    public void shopFunction(string item, int cost)
    {

        switch (item)
        {
            case "Laser":
                Debug.Log("bought Laser");
                break;
            case "Piercing Shots":
                Debug.Log("piercing shots");
                
                if (theLevelMaster.money >= cost)
                {
                    theLevelMaster.money -= cost;
                    player.piercingShots = true;
                    removeShopItem("Piercing Shots");
                }
                
                break;
            case "Heal":
                Debug.Log("Heal");
                if(theLevelMaster.money >= cost)
                {
                    theLevelMaster.money -= cost;
                    player.currentHealth += player.level * 5;
                }
                break;
            case "Perfect Thrusters":
                if(theLevelMaster.money >= cost)
                {
                    theLevelMaster.money -= cost;
                    player.perfectThrusters = true;
                    
                    removeShopItem("Perfect Thrusters");
                }
                break;
            case "Fire Rate":
                if (theLevelMaster.money >= cost)
                {
                    theLevelMaster.money -= cost;

                    player.fireRate = player.baseFireRate / (1 + ((float)player.fireRateLevel / 10));

                    removeShopItem("Fire Rate");
                    player.fireRateLevel++;
                    if(player.fireRateLevel <31)
                    {
                        addShopItem("Fire Rate", player.fireRateLevel * 200);
                    }
                }
                    
                break;
            default:
                break;
        }
        theLevelMaster.UpdateUI();
        UpdateShop();
    }

    public void addShopItem(string n, int c)
    {
        theShopItems.Add(new shopItems(n, c,false,true));
    }

    public void removeShopItem(string n)
    {
        for (int x = 0; x< theShopItems.Count; x++)
        {
            if (theShopItems[x].itemName == n)
            {
                theShopItems[x].purchased = true;
            }
        }
        UpdateShop();
    }

    public void refreshHeal()
    {
        for (int x = 0; x < theShopItems.Count; x++)
        {
            if (theShopItems[x].itemName == "Heal")
            {
                theShopItems.RemoveAt(x);
            }
        }
        addShopItem("Heal", player.level * 100);
    }
}
