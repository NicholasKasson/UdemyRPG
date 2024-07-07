using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour{
    public string transitonName;
    void Start(){
        if(transitonName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        } 
        UIFade.instance.FadeFromBlack();
        GameManager.instance.fadingBetweenAreas = false;

    }
    void Update(){
        
    }
}
