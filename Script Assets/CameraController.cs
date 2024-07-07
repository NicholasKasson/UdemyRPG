using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CameraController : MonoBehaviour
{
    public Transform followtarget;
    public Tilemap theMap;
    public Vector3 bottomleftLimit;
    public Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;
    public int musicToPlay;
    private bool musicIsActive;
    public int callSpecificBackgroundForBattle;
    public static CameraController instance;
    // Start is called before the first frame update
    void Start(){
        instance = this;
        followtarget = FindObjectOfType<PlayerController>().transform; 
    
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomleftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);;

        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    // Update is called once per frame
    void LateUpdate(){
        transform.position = new Vector3(followtarget.position.x, followtarget.position.y, transform.position.z);

        //Keep Camera inside the boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomleftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomleftLimit.y, topRightLimit.y), transform.position.z);
        if(!musicIsActive){
            musicIsActive = true;
            AudioManager.instance.PlayMusic(musicToPlay);
        }

    }

}
