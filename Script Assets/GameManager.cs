using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas, shopActive;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;
    public int currentGold;
    private bool dataLoadedFromFile = false;
    private bool canSave = false;
    public bool debugEnabled = true;
    public bool battleActive = false;

    public StatSheet[] playerStats;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SortItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogueActive || fadingBetweenAreas || shopActive || battleActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
        if((!dataLoadedFromFile && !debugEnabled) || Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Data Loaded");
            LoadData();
            dataLoadedFromFile = true;
            canSave = true;
        }
        if(Input.GetKeyDown(KeyCode.E) && canSave)
        {
            Debug.Log("Data Saved");
            SaveData();
        }
    }
    public Item GetItemDetails(string ItemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == ItemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
    }
    public void SortItem()
    {
        bool itemAfterSpace = true;
        while(itemAfterSpace)
        {
            itemAfterSpace = false;
            for(int i = 0;i < itemsHeld.Length - 1; i++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i+1];
                    itemsHeld[i + 1] = "";
                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;
                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }
    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                    i = itemsHeld.Length;
                    foundSpace = true;

            }
        }
        if(foundSpace)
        {
            bool itemExists = false;
            for(int i = 0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }
            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does not exist!");
            }
        }
    }
    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }
        if(foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
                GameMenu.instance.ShowItems();
            }
        }
        else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }
    public void SaveData()
    {
        //Below is the preset load condtion, add String or Int to set variables desired. 
        //PlayerPrefs.Set
        PlayerPrefs.SetString("Active_Scene_Location", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_Y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_Z", PlayerController.instance.transform.position.z);
        PlayerPrefs.SetInt("Current_Gold_", currentGold);
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Level", playerStats[i].characterLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentXP",playerStats[i].currentXP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentHP",playerStats[i].currentHealth);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxHP",playerStats[i].maxHealth);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_CurrentMP",playerStats[i].currentMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_MaxMP",playerStats[i].maxMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength",playerStats[i].baseStrength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Endurance",playerStats[i].baseEndurance);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Agility",playerStats[i].baseAgility);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Intellect",playerStats[i].baseIntelligence);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WeaponPhys",playerStats[i].weaponPowerPhysical);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WeaponMagic",playerStats[i].weaponPowerMagical);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmorPhys",playerStats[i].armorPowerPhysical);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmorMagic",playerStats[i].armorPowerMagical);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_SkillPoints",playerStats[i].skillPoints);
            // PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "",playerStats[i].);
            // For extra required Variable saving
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedWeapon", playerStats[i].equippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedArmor", playerStats[i].equippedArmor);
        }
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);

        }
    }
    public void LoadData()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Active_Scene_Location", SceneManager.GetActiveScene().name));
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_X"), PlayerPrefs.GetFloat("Player_Position_Y"), PlayerPrefs.GetFloat("Player_Position_Z"));
        currentGold = PlayerPrefs.GetInt("Current_Gold_", currentGold);
        GameMenu.instance.UpdateGold();
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_"+ playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }
            playerStats[i].characterLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level");
            playerStats[i].currentXP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentXP");
            playerStats[i].currentHealth = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentHP");
            playerStats[i].maxHealth = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxHP");
            playerStats[i].currentMana = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_CurrentMP");
            playerStats[i].maxMana = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_MaxMP");
            playerStats[i].baseStrength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength");
            playerStats[i].baseEndurance = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Endurance");
            playerStats[i].baseAgility = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Agility");
            playerStats[i].baseIntelligence = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Intellect");
            playerStats[i].weaponPowerPhysical = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WeaponPhys");
            playerStats[i].weaponPowerMagical = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WeaponMagic");
            playerStats[i].armorPowerPhysical = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmorPhys");
            playerStats[i].armorPowerMagical = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmorMagic");
            playerStats[i].skillPoints = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_SkillPoints");
            // PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "",playerStats[i].);
            // For extra required Variable saving
            playerStats[i].equippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedWeapon");
            playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedArmor");
        }
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}
 