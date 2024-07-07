using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    private bool battleActive;
    public GameObject[] battleSceneBackgrounds;
    public GameObject battleScene;
    // public int battleSceneToActivate;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleCharacterStats[] playerCharacters;
    public BattleCharacterStats[] enemyCharacters;
    public List<BattleCharacterStats> activeFighters = new List<BattleCharacterStats>();
    
    public bool hasActed;
    public int activeFighterInSequence;
    public int currentTurn = 1;
    public bool waitingForTurn, isPlayerTurn, isEnemyTurn, isControllable;
    public GameObject UIButtonsForBattle;
    private int livingFriendliesCount = 0, livingEnemiesCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        battleScene.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            BattleStart(new string[] {"Lesser Slime", "Lesser Spider"});
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            battleActive = false;
            GameManager.instance.battleActive = false;
            battleScene.SetActive(false);
        }
        if(battleActive)
        {
            if(waitingForTurn)
            {
                if(activeFighters[activeFighterInSequence].isFriendly)
                {
                    UIButtonsForBattle.SetActive(true);
                    isPlayerTurn = true;
                    isEnemyTurn = false;
                }
                else
                {
                    UIButtonsForBattle.SetActive(false);
                    isPlayerTurn = false;
                    isEnemyTurn = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.N))
            {
                NextTurn();
            }
        }
    }
    public void BattleStart(string[] enemiesToSpawn)
    {
        if(!battleActive)
        {
            battleActive = true;
            GameManager.instance.battleActive = true;
            battleScene.SetActive(true);
            transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            for(int i = 0; i < battleSceneBackgrounds.Length; i++)
            {
                battleSceneBackgrounds[i].gameObject.SetActive(false);
            }
            battleSceneBackgrounds[CameraController.instance.callSpecificBackgroundForBattle].gameObject.SetActive(true);
            AudioManager.instance.PlayMusic(4);
            for(int i = 0; i < playerPositions.Length; i++)
            {
                if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playerCharacters.Length; j++)
                    {
                        if(playerCharacters[j].characterName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleCharacterStats newFriendlyCharacter = Instantiate(playerCharacters[j], playerPositions[i].position, playerPositions[i].rotation);
                            newFriendlyCharacter.transform.parent = playerPositions[i];
                            activeFighters.Add(newFriendlyCharacter);
                            livingFriendliesCount++;

                            StatSheet ActivePlayer = GameManager.instance.playerStats[i];
                            activeFighters[i].BattleCurrentHP = ActivePlayer.currentHealth;
                            activeFighters[i].BattleMaxHP = ActivePlayer.maxHealth;
                            activeFighters[i].BattleCurrentMP = ActivePlayer.currentMana;
                            activeFighters[i].BattleMaxMP = ActivePlayer.maxMana;
                            activeFighters[i].BattleStrength = ActivePlayer.strength;
                            activeFighters[i].BattleEndurance = ActivePlayer.endurance;
                            activeFighters[i].BattleAgility = ActivePlayer.agility;
                            activeFighters[i].BattleIntelligence = ActivePlayer.intelligence;
                            activeFighters[i].BattleWeaponMagic = ActivePlayer.weaponPowerMagical;
                            activeFighters[i].BattleArmorPhysical = ActivePlayer.weaponPowerPhysical;
                            activeFighters[i].BattleArmorMagic = ActivePlayer.armorPowerMagical;
                            activeFighters[i].BattleArmorPhysical= ActivePlayer.armorPowerPhysical;
                            activeFighters[i].isActive = true;

                        }
                    }
                }
            }
            for(int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if(enemiesToSpawn[i] != "")
                {
                    for(int j = 0; j < enemyCharacters.Length; j++)
                    {
                        if(enemyCharacters[j].characterName == enemiesToSpawn[i])
                        {
                            BattleCharacterStats newEnemyCharacter = Instantiate(enemyCharacters[j], enemyPositions[i].position, enemyPositions[i].rotation);
                            newEnemyCharacter.transform.parent = enemyPositions[i];
                            activeFighters.Add(newEnemyCharacter);
                            livingEnemiesCount++;
                        }
                    }
                }
            }
            waitingForTurn = true;
            activeFighterInSequence = Random.Range(0, activeFighters.Count);
        }
    }
    public void NextTurn()
    {
        activeFighterInSequence++;
        if(activeFighterInSequence >= activeFighters.Count)
        {
            activeFighterInSequence = 0;
            currentTurn++;
        }
        waitingForTurn = true;
        UpdateBattle();
    }
    public void UpdateBattle()
    {
        bool allEnemiesDead = false;
        bool allFriendliesDead = false;
        for(int i = 0; i < activeFighters.Count; i++)
        {
            if(activeFighters[i].BattleCurrentHP <= 0)
            {
                activeFighters[i].BattleCurrentHP = 0;

                if(activeFighters[i].isFriendly && livingFriendliesCount > 0)
                {
                    livingFriendliesCount--;
                    if(livingFriendliesCount == 0)
                    {
                        allFriendliesDead = true;
                    }
                }
                if(!activeFighters[i].isFriendly && livingEnemiesCount > 0)
                {
                    livingEnemiesCount--;
                    if(livingFriendliesCount == 0)
                    {
                        allEnemiesDead = true;
                    }
                }
            }
        }
        if(allEnemiesDead || allFriendliesDead)
        {
            if(allFriendliesDead)
            {
                //Run Gameover sequence
            }
            else
            {

            }
            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;
        }
    }
    public void EnemyAttack()
    {
        
    }
}
