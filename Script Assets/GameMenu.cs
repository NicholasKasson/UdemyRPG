using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    
    public GameObject theMenu;
    public GameObject[] windows;
    private StatSheet[] playerStats; 
    public Text[] nameText, HPText, MPText, LevelText, XPText;
    public Slider[] HPSlider, MPSlider, XPSlider;
    public Image[] characterImage;
    public GameObject[] numActiveCharacter;    
    public GameObject[] statusButtons;
    public GameObject[] playerButtons;
    public Text statusName, statusHP, statusMP, statusSkillPoints, statusStr, statusEnd, statusAgi, statusInt, statusWpnNm, statusWpnPhys, statusWpnMgk, statusArmorNm, statusArmorPhys, statusArmorMgk, statusXP, statusLevel;
    public Slider statusHPSlider, statusMPSlider, statusXPSlider;
    public Text[] StatType;

    private int SPStr, SPEnd, SPAgi, SPInt;
    public Text SPStrText, SPEndText, SPAgiText, SPIntText;
    public Image statusImage;
    public GameObject statAllocationPanel;
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;   
    public static GameMenu instance;
    public GameObject itemCharacterChoiceMenu;
    public Text[] itemCharacterChoiceNames;
    public Text goldText;
    private bool firstStatusOpenCheck;
    public int lastSelected;
    private int lastCharacterPanel;
    public bool lastCharacterSet;
    void Start()
    {
        instance = this;
        theMenu.SetActive(false);
        UpdateGold();
        GameManager.instance.SortItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.battleActive)
        {
            if(theMenu.activeInHierarchy || ShopMenu.instance.shopMenu.activeInHierarchy)
            {
                CloseMenu();
                ShopMenu.instance.CloseShop();
            }
            else
            {
                theMenu.SetActive(true); 
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
            // AudioManager.instance.PlaySFX(5);
        }
    }
    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;
        for(int i = 0; i < playerStats.Length; i++)
        {
            //Debug.Log("Setting Character " + i + "'s Stats.");
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                numActiveCharacter[i].SetActive(true);
                nameText[i].text = playerStats[i].charName;
                HPText[i].text = "HP: " + playerStats[i].currentHealth + "/" + playerStats[i].maxHealth;
                HPSlider[i].maxValue = playerStats[i].maxHealth;
                HPSlider[i].value = playerStats[i].currentHealth;
                MPText[i].text = "MP: " + playerStats[i].currentMana + "/" + playerStats[i].maxMana;
                MPSlider[i].maxValue = playerStats[i].maxMana;
                MPSlider[i].value = playerStats[i].currentMana;
                LevelText[i].text = "Level: \n" + playerStats[i].characterLevel;
                XPText[i].text = "" + playerStats[i].currentXP + "/" + playerStats[i].xpToNextLevel[playerStats[i].characterLevel] + " XP";
                XPSlider[i].maxValue = playerStats[i].xpToNextLevel[playerStats[i].characterLevel];
                XPSlider[i].value = playerStats[i].currentXP;
                characterImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                numActiveCharacter[i].SetActive(false);
            }
        }
    }
    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        itemCharacterChoiceMenu.SetActive(false);
        }
        UpdateGold();
    }
    public void CloseMenu()
    {
        for(int i = 0;i < windows.Length;i++)
        {
            windows[i].SetActive(false);
        }
        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        itemCharacterChoiceMenu.SetActive(false);
    }
    public void OpenStatus()
    {
        UpdateMainStats();
        if(!firstStatusOpenCheck)
        {
            CharacterStatus(0);
            lastCharacterPanel = 0;
            firstStatusOpenCheck = true;
        }
        else
        {
            CharacterStatus(lastCharacterPanel);
        }
        for(int i = 0;i < statusButtons.Length; i++)
        {  
            
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }
    public void CharacterStatus(int Selected)
    {
        if(!lastCharacterSet)
        {
            Debug.Log("Character set as " + Selected);
            lastSelected = lastCharacterPanel;
            ResetSkillInvest(lastSelected);
            lastCharacterSet = true;  
        }
        statusName.text = playerStats[Selected].charName;
        statusHP.text = "HP " + playerStats[Selected].currentHealth + "/" + playerStats[Selected].maxHealth;
        statusHPSlider.maxValue = playerStats[Selected].maxHealth;
        statusHPSlider.value = playerStats[Selected].currentHealth;
        statusMP.text = "MP " + playerStats[Selected].currentMana + "/" + playerStats[Selected].maxMana;
        statusMPSlider.maxValue = playerStats[Selected].maxMana;
        statusMPSlider.value = playerStats[Selected].currentMana;
        statusStr.text = "Strength: ["+ playerStats[Selected].strength.ToString() + "]";
        statusEnd.text = "Endurance: [" + playerStats[Selected].endurance.ToString() + "]";
        statusAgi.text = "Agility: [" + playerStats[Selected].agility.ToString() + "]";
        statusInt.text = "Intelligence: [" + playerStats[Selected].intelligence.ToString() + "]";
        if (playerStats[Selected].skillPoints != 0)
        {
            statusSkillPoints.gameObject.SetActive(true);
            statusSkillPoints.text = playerStats[Selected].skillPoints.ToString() + " Skill Points Left";
            statAllocationPanel.SetActive(true);
        }
        else
        {
            statusSkillPoints.gameObject.SetActive(false);            
            statAllocationPanel.SetActive(false);
        }
        if(playerStats[Selected].equippedWeapon != "")
        {
            statusWpnNm.text = "[" + playerStats[Selected].equippedWeapon + "]";
        }
        statusWpnPhys.text = "Physical Power " + playerStats[Selected].weaponPowerPhysical.ToString();
        statusWpnMgk.text = "Magic Power " + playerStats[Selected].weaponPowerMagical.ToString();
        if(playerStats[Selected].equippedArmor != "")
        {
            statusArmorNm.text = "[" + playerStats[Selected].equippedArmor + "]";
        }
        statusArmorPhys.text = "Physical Defense " + playerStats[Selected].armorPowerPhysical.ToString();
        statusArmorMgk.text = "Magic Defense " + playerStats[Selected].armorPowerMagical.ToString();
        statusXP.text = playerStats[Selected].currentXP + "/" + playerStats[Selected].xpToNextLevel[playerStats[Selected].characterLevel] + " XP";
        statusXPSlider.maxValue = playerStats[Selected].xpToNextLevel[playerStats[Selected].characterLevel];
        statusXPSlider.value = playerStats[Selected].currentXP;
        statusLevel.text = "Level: \n" + playerStats[Selected].characterLevel.ToString();
        statusImage.sprite = playerStats[Selected].charImage;
        lastCharacterPanel = Selected;
        lastCharacterSet = false;
    }
    public void InvestSkillpoint(string StatMod)
    {
        if(StatMod.EndsWith("+"))
        {
            if(playerStats[lastCharacterPanel].skillPoints > 0)
            {
                if(StatMod.StartsWith("Strength"))
                {SPStr++; SPStrText.text = SPStr.ToString();}
                if(StatMod.StartsWith("Endurance"))
                {SPEnd++; SPEndText.text = SPEnd.ToString();}
                if(StatMod.StartsWith("Agility"))
                {SPAgi++; SPAgiText.text = SPAgi.ToString();}
                if(StatMod.StartsWith("Intelligence"))
                {SPInt++; SPIntText.text = SPInt.ToString();}
                playerStats[lastCharacterPanel].skillPoints--;
            }
            statusSkillPoints.text = playerStats[lastCharacterPanel].skillPoints.ToString() + " Skill Points Left";
        }
        if(StatMod.EndsWith("-"))
        {
            
            if(StatMod.StartsWith("Strength"))
            {
                if(SPStr > 0)
                {
                SPStr--; 
                SPStrText.text = SPStr.ToString();
                playerStats[lastCharacterPanel].skillPoints++;
                }
            }
            if(StatMod.StartsWith("Endurance"))
            {
                if(SPStr > 0)
                {
                SPEnd--; 
                SPEndText.text = SPEnd.ToString();
                playerStats[lastCharacterPanel].skillPoints++;
                }
            }
            if(StatMod.StartsWith("Agility"))
            {
                if(SPAgi > 0)
                {
                SPAgi--; 
                SPAgiText.text = SPAgi.ToString();
                playerStats[lastCharacterPanel].skillPoints++;
                }
            }
            if(StatMod.StartsWith("Intelligence"))
            {
                if(SPInt > 0)
                {
                SPInt--; 
                SPIntText.text = SPInt.ToString();
                playerStats[lastCharacterPanel].skillPoints++;
                }
            }
            
            statusSkillPoints.text = playerStats[lastCharacterPanel].skillPoints.ToString() + " Skill Points Left";
        }
        if(StatMod.StartsWith("Confirm"))
        {
            playerStats[lastCharacterPanel].strength += SPStr;
            playerStats[lastCharacterPanel].baseStrength += SPStr;
            playerStats[lastCharacterPanel].endurance += SPEnd;
            playerStats[lastCharacterPanel].baseEndurance += SPEnd;
            playerStats[lastCharacterPanel].agility += SPAgi;
            playerStats[lastCharacterPanel].baseAgility += SPAgi;
            playerStats[lastCharacterPanel].intelligence += SPInt; 
            playerStats[lastCharacterPanel].baseIntelligence += SPInt; 
            
            SPStr = 0; SPEnd = 0; SPAgi = 0; SPInt = 0;
            statusSkillPoints.text = playerStats[lastCharacterPanel].skillPoints.ToString() + " Skill Points Left";
            statusStr.text = "Strength: ["+ playerStats[lastCharacterPanel].strength.ToString() + "]";
            statusEnd.text = "Endurance: [" + playerStats[lastCharacterPanel].endurance.ToString() + "]";
            statusAgi.text = "Agility: [" + playerStats[lastCharacterPanel].agility.ToString() + "]";
            statusInt.text = "Intelligence: [" + playerStats[lastCharacterPanel].intelligence.ToString() + "]";
            SPStrText.text = SPStr.ToString();
            SPEndText.text = SPEnd.ToString();
            SPAgiText.text = SPAgi.ToString();
            SPIntText.text = SPInt.ToString();
        }
    }
    private void ResetSkillInvest(int Selected)
    {
        playerStats[Selected].skillPoints += SPStr + SPEnd + SPAgi + SPInt;
        SPStr = 0; SPEnd = 0; SPAgi = 0; SPInt = 0;
        statusSkillPoints.text = playerStats[Selected].skillPoints.ToString() + " Skill Points Left";
        SPStrText.text = SPStr.ToString();
        SPEndText.text = SPEnd.ToString();
        SPAgiText.text = SPAgi.ToString();
        SPIntText.text = SPInt.ToString();
    }
    public void ShowItems()
    {
        GameManager.instance.SortItem();
        itemButtons[0].Press();
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;
            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].ButtonImage.gameObject.SetActive(true);
                itemButtons[i].ButtonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                if(GameManager.instance.numberOfItems[i] > 1)
                {
                    itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
                }
                else
                {
                    itemButtons[i].amountText.text = "";
                }
            } 
            else
            {
                itemButtons[i].ButtonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if(activeItem.isItem)
        {
            useButtonText.text = "Use";
        }
        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.itemDescription;
    }
    public void DiscardItem()
    {
        
        GameManager.instance.RemoveItem(activeItem.itemName);
    }
    public void OpenItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(true);
        for(int i = 0; i < itemCharacterChoiceNames.Length; i++)
        {
            itemCharacterChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(false);
    }
    public void UseItem(int selectedCharacter)
    {
        activeItem.Use(selectedCharacter);
        CloseItemCharacterChoice();
        activeItem = null;
    }
    public void UpdateGold()
    {
        goldText.text = GameManager.instance.currentGold.ToString() + " Gold";
    }
    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }
    public void PlayButtonSound(){
        AudioManager.instance.PlaySFX(4);
    }
}