using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSheet : MonoBehaviour
{
    
    public string[] lines;
    public string charName;
    public int characterLevel = 1;
    public int lastCharacterLevel = 1;
    public int currentXP;
    public int[] xpToNextLevel;
    public int maxLevel = 101;
    private int baseXP = 80;
    //Each level is roughly 25% more than the last until max level.
    
    public int currentHealth;
    public int maxHealth = 1;
    public int currentMana;
    public int maxMana;
    public int strength;
    public int endurance;
    public int agility;
    public int intelligence;
    public int baseStrength, baseEndurance, baseAgility, baseIntelligence;
    public int skillPoints = 0;
    public int weaponPowerPhysical;
    public int weaponPowerMagical;
    public int armorPowerPhysical;
    public int armorPowerMagical; 
    public string equippedWeapon;
    public string equippedArmor;
    public Sprite charImage;
    public int currentArmorStatModSTR, currentArmorStatModEND, currentArmorStatModAGI, currentArmorStatModINT;
    public int currentWeaponStatModSTR, currentWeaponStatModEND, currentWeaponStatModAGI, currentWeaponStatModINT;
    public int currentAccessoryStatModSTR, currentAccessoryStatModEND, currentAccessoryStatModAGI, currentAccessoryStatModINT;
    public static StatSheet instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        xpToNextLevel = new int[maxLevel];
        xpToNextLevel[1] = baseXP;

        for(int i = 2; i < xpToNextLevel.Length; i++)
        {
            if(i < 51)
            {
                xpToNextLevel[i] = Mathf.FloorToInt((xpToNextLevel[i - 1] + 7) * (1 + (i * 0.004f)));
                // Debug.Log(xpToNextLevel[i] - xpToNextLevel[i-1] + " Level " + i);
            }
            if(i >= 51)
            {
                xpToNextLevel[i] = Mathf.FloorToInt((xpToNextLevel[i - 1] + 3) * (1 + (i * (0.0032f - ((i-50) * 0.00005f)))));
                // Debug.Log(xpToNextLevel[i] - xpToNextLevel[i-1] + " Level " + i);
            }

            // Debug.Log(Mathf.FloorToInt((xpToNextLevel[i - 1] + 8) * (1 + (i * 0.004f))) - Mathf.FloorToInt((xpToNextLevel[i - 1])));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.I))
        {
            AddXPScript(420);
        }
    }
    public void AddXPScript(int xpToAdd)
    {
        currentXP += xpToAdd;
        if(characterLevel < maxLevel)
        {
            if(currentXP > xpToNextLevel[characterLevel])
            {
                currentXP -= xpToNextLevel[characterLevel];
                characterLevel++;
                maxHealth = Mathf.FloorToInt((maxHealth + 5) * 1.05f);
                maxMana = Mathf.FloorToInt((maxMana + 2) * 1.03f);
                if(characterLevel > lastCharacterLevel)
                {
                    //todo: make script that opens a dialogue window and allows player to determine stat(s) to add points to
                    skillPoints += (characterLevel - lastCharacterLevel) * 5;
                    lastCharacterLevel = characterLevel;
                    string[] lines = {"name-Narrator", "You've gained a level, visit the stats screen to spend your newly earned skillpoints."};
                    DialogueManager.instance.ShowDialogue(lines, false);

                }
            }
        }
        if(characterLevel >= maxLevel)
        {
            currentXP = 0;
        } 
    }

}
