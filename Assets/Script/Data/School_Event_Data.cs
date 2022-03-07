using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School_Event_Data : MonoBehaviour
{
    Game_Data game_data;

    List<Dictionary<string, object>> school_event_table;
    public List<School_Event> school_event_data;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        school_event_table = CSVReader.Read("DataBase/CSV/School_Event_Table");

        for (int i = 0; i < school_event_table.Count; i++)
        {
            School_Event new_school_event = new School_Event();
            new_school_event.code = school_event_table[i]["code"].ToString();
            new_school_event.name = school_event_table[i]["name"].ToString();
            new_school_event.chance = float.Parse(school_event_table[i]["chance"].ToString());

            string need = school_event_table[i]["need"].ToString();

            while(need.Contains("#") == true)
            {
                 int string_cut = need.LastIndexOf("#", need.Length);
                 string tag_need = need.Substring(string_cut);
                 need = need.Replace(need.Substring(string_cut), "");
                 new_school_event.need.Add(tag_need);
            }

            string effect = school_event_table[i]["effect"].ToString();

            while (effect.Contains("#") == true)
            {
                int string_cut = effect.LastIndexOf("#", effect.Length);
                string tag_effect = effect.Substring(string_cut);
                effect = effect.Replace(effect.Substring(string_cut), "");
                new_school_event.effect.Add(tag_effect);
            }
            school_event_data.Add(new_school_event);
        }
    }
}
