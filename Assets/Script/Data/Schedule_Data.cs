using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule_Data : MonoBehaviour
{
    Game_Data game_data;

    List<Dictionary<string, object>> schedule_table;
    public List<Schedule> schedule_data_temp;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        schedule_table = CSVReader.Read("DataBase/CSV/Schedule_Table");

        for (int i = 0; i < schedule_table.Count; i++)
        {
            Schedule new_schedule = new Schedule();

            new_schedule.name = schedule_table[i]["name"].ToString();
            new_schedule.type = schedule_table[i]["type"].ToString();
            new_schedule.ticket = schedule_table[i]["ticket"].ToString();
            new_schedule.month = int.Parse(schedule_table[i]["month"].ToString());
            new_schedule.prestige = int.Parse(schedule_table[i]["prestige"].ToString());
            new_schedule.money = int.Parse(schedule_table[i]["money"].ToString());

            schedule_data_temp.Add(new_schedule);
        }
    }
}
