using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private StatSheet[] playerStats; 
    [Header("Item Type")]
    public bool isItem; 
    public bool isWeapon, isArmor, isAccessory;
    public bool isConsumable, isSellable;
    public string itemName, itemDescription;
    public int itemValue;
    public Sprite itemSprite;
    
    [Header("Item Details")]
    public int amountToChange;
    public bool modifiesHP, modifiesMP, modifiesStat;

    [Header("Weapon/Armor Details")]
    public int statModSTR;
    public int statModEND, statModAGI, statModINT;
    public int weaponPhysical, weaponMagical;
    public int armorPhysical, armorMagical;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Use(int characterToUseOn)
    {
        StatSheet selectedCharacter = GameManager.instance.playerStats[characterToUseOn];
        if(isItem)
        {
            if(modifiesHP)
            {
                selectedCharacter.currentHealth += amountToChange;
                if(selectedCharacter.currentHealth > selectedCharacter.maxHealth)
                {
                    selectedCharacter.currentHealth = selectedCharacter.maxHealth;
                }
            }
            if(modifiesMP)
            {
                selectedCharacter.currentMana += amountToChange;
                if(selectedCharacter.currentMana > selectedCharacter.maxMana)
                {
                    selectedCharacter.currentMana = selectedCharacter.maxMana;
                }
            }
            if(isConsumable)
            {
                GameManager.instance.RemoveItem(itemName);
            }
        }
        if(isArmor)
        {
            if(selectedCharacter.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }
            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPowerPhysical = armorPhysical;
            selectedCharacter.armorPowerMagical = armorMagical;
            if(modifiesStat)
            {
                // if(statModSTR != 0)
                // {
                    if(selectedCharacter.currentArmorStatModSTR != 0)
                    {
                        selectedCharacter.strength -= selectedCharacter.currentArmorStatModSTR;
                    }
                    selectedCharacter.strength += statModSTR;
                    selectedCharacter.currentArmorStatModSTR = 0;
                    selectedCharacter.currentArmorStatModSTR += statModSTR;
                // }
                // if(statModEND != 0)
                // {
                    if(selectedCharacter.currentArmorStatModEND != 0)
                    {
                        selectedCharacter.endurance -= selectedCharacter.currentArmorStatModEND;
                    }
                    selectedCharacter.endurance += statModEND;
                    selectedCharacter.currentArmorStatModEND = 0;
                    selectedCharacter.currentArmorStatModEND += statModEND;
                // }
                // if(statModAGI != 0)
                // {
                    if(selectedCharacter.currentArmorStatModSTR != 0)
                    {
                        selectedCharacter.agility -= selectedCharacter.currentArmorStatModSTR;
                    }
                    selectedCharacter.agility += statModAGI;
                    selectedCharacter.currentArmorStatModAGI = 0;
                    selectedCharacter.currentArmorStatModAGI += statModAGI;
                // }
                // if(statModINT != 0)
                // {
                    if(selectedCharacter.currentArmorStatModINT != 0)
                    {
                        selectedCharacter.intelligence -= selectedCharacter.currentArmorStatModINT;
                    }
                    selectedCharacter.intelligence += statModINT;
                    selectedCharacter.currentArmorStatModINT = 0;
                    selectedCharacter.currentArmorStatModINT += statModINT;
                // }
            }
            GameManager.instance.RemoveItem(itemName);
        }
        if(isWeapon)
        {
            if(selectedCharacter.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }
            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPowerPhysical = weaponPhysical;
            selectedCharacter.weaponPowerMagical = weaponMagical;
            if(modifiesStat)
            {
                // Debug.Log("Equipped Item STR, END, AGI, INT: " + statModSTR + ", " + statModEND + ", " + statModAGI + ", " + statModINT);
                // Debug.Log("Versus Prev Item STR, END, AGI, INT: " + selectedCharacter.currentWeaponStatModSTR + ", " + selectedCharacter.currentWeaponStatModEND + ", " + selectedCharacter.currentWeaponStatModAGI + ", " + selectedCharacter.currentWeaponStatModINT);
                // if(statModSTR != 0)
                // {
                    if(selectedCharacter.currentWeaponStatModSTR != 0)
                    {
                        selectedCharacter.strength -= selectedCharacter.currentWeaponStatModSTR;
                    }
                    selectedCharacter.strength += statModSTR;
                    selectedCharacter.currentWeaponStatModSTR = 0;
                    selectedCharacter.currentWeaponStatModSTR += statModSTR;
                // }
                // if(statModEND != 0)
                // {
                    if(selectedCharacter.currentWeaponStatModEND != 0)
                    {
                        selectedCharacter.endurance -= selectedCharacter.currentWeaponStatModEND;
                    }
                    selectedCharacter.endurance += statModEND;
                    selectedCharacter.currentWeaponStatModEND = 0;
                    selectedCharacter.currentWeaponStatModEND += statModEND;

                // }
                // if(statModAGI != 0)
                // {
                    if(selectedCharacter.currentWeaponStatModAGI != 0)
                    {
                        selectedCharacter.agility -= selectedCharacter.currentWeaponStatModAGI;
                    }
                    selectedCharacter.agility += statModAGI;
                    selectedCharacter.currentWeaponStatModAGI = 0;
                    selectedCharacter.currentWeaponStatModAGI += statModAGI;

                // }
                // if(statModINT != 0)
                // {
                    if(selectedCharacter.currentWeaponStatModINT != 0)
                    {
                        selectedCharacter.intelligence -= selectedCharacter.currentWeaponStatModINT;
                    }
                    selectedCharacter.intelligence += statModINT;
                    selectedCharacter.currentWeaponStatModINT = 0;
                    selectedCharacter.currentWeaponStatModINT += statModINT;

                // }
            }
            GameManager.instance.RemoveItem(itemName);            
        }
        // if(isAccessory)
        // {}
    }
}
// if(modifierStat == true)
// {
//     if(statModifier1 != null)
//     {
//         if(statModifier1 == "Strength")
//         {
            
//         }

//     }
// }