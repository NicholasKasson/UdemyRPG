using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] SFX, BackGroundMusic;
    public static AudioManager instance;
    void Start(){
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Update(){
        
    }
    public void PlaySFX(int soundToPlay){
        if(soundToPlay < SFX.Length){
        SFX[soundToPlay].Play();
        }
    }
    public void PlayMusic(int musicSelection){
        if(!BackGroundMusic[musicSelection].isPlaying){
            StopMusic();
            if(musicSelection < BackGroundMusic.Length){
            BackGroundMusic[musicSelection].Play();
            }
        }
    }
    public void StopMusic(){
        for(int i = 0; i < BackGroundMusic.Length; i++){
            BackGroundMusic[i].Stop();
        }
    }
}
