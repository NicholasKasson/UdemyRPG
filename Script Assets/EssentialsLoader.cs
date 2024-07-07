using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject Player;
    // public GameObject PlayerStatsHolder;
    public GameObject gameManager;
    public GameObject audioManager;
    public GameObject battleManager;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance == null){
            PlayerController clone = Instantiate(Player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        if(UIFade.instance == null){
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
        // if(StatSheet.instance == null)
        // {
        //     StatSheet.instance = Instantiate(PlayerStatsHolder).GetComponent<StatSheet>();
        // }
        if(GameManager.instance == null){
           GameManager.instance = Instantiate(gameManager).GetComponent<GameManager>();
        }
        if(AudioManager.instance == null){
           AudioManager.instance = Instantiate(audioManager).GetComponent<AudioManager>();
        }
        if(BattleManager.instance == null)
        {
            BattleManager.instance = Instantiate(battleManager).GetComponent<BattleManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
