using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaExit : MonoBehaviour{
    public string areaToLoad;
    //The Scene to Load on entering trigger zone.
    public string areaTransitionName;
    //The referenced location for the game to grab, must match another in order for there to be a connection
    public AreaEntrance theEntrance;
    public float waitToLoad;
    public bool shouldLoadAfterFade;
    void Start(){
        theEntrance.transitonName = areaTransitionName;
    }
    void Update(){
        if(shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();

            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}