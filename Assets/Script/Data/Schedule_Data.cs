using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_Data : MonoBehaviour
{
    Game_Data game_data;

    List<Dictionary<string, object>> schedule_table;
    public Dictionary<string, Schedule> schedule_data;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
    }

    public void Set_Schedule()
    {
        
    }

    public void Refresh_Schedule()
    {

    }
}
