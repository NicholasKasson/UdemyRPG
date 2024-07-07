using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public string[] questMarkerNames;
    public bool[] questMarkersCompletion;
    public static QuestManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        questMarkersCompletion = new bool[questMarkerNames.Length];
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Q))
        // {
        //     Debug.Log("Saving Quest Data, be patient!");
        //     SaveQuestData();
        // }
        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     Debug.Log("Loading Quest Data, be patient!");
        //     LoadQuestData();
        // }
    }
    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }
        return 0;
    }
    public bool CheckIfComplete(string questToCheck)
    {
        if(GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersCompletion[GetQuestNumber(questToCheck)];
            // MarkQuestComplete("quest name");
            //Only use when no other factors are available to complete quests. 
        }
        
        return false;
    }
    public void MarkQuestComplete(string questToMark)
    {
        questMarkersCompletion[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }
    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersCompletion[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }
    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }
    public void SaveQuestData()
    {
        Debug.Log("Saving Quest Data, be patient!");
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkersCompletion[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }
    public void LoadQuestData()
    {
        Debug.Log("Loading Quest Data, be patient!");
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }
            
            if(valueToSet == 0)
            {
                questMarkersCompletion[i] = false;
            }
            else
            {
                questMarkersCompletion[i] = true;
            }
        }
    }
}
