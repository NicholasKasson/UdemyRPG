using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterStats : MonoBehaviour
{
    public bool isFriendly;
    public string[] abilitiesAvailable;
    public string characterName;
    [Header("XP and Level Stats (Enemies Only)")]
    public int GivenXP; 
    public int EnemyLevel;
    [Header("Battle Stats (Specifics per Enemy)")]
    public int BattleCurrentHP;
    public int  BattleMaxHP, BattleCurrentMP, BattleMaxMP, BattleStrength, BattleEndurance, BattleAgility, BattleIntelligence;
    public int BattleWeaponPhysical, BattleWeaponMagic, BattleArmorPhysical, BattleArmorMagic;
    public bool isAlive, isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
