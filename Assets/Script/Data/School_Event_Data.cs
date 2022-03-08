using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School_Event_Data : MonoBehaviour
{
    List<Dictionary<string, object>> school_event_table;
    public List<School_Event> school_event_list;

    void Start()
    {
        school_event_table = CSVReader.Read("DataBase/CSV/School_Event_Table");

        for (int i = 0; i < school_event_table.Count; i++)
        {
            School_Event new_school_event = new School_Event();
            new_school_event.code = school_event_table[i]["code"].ToString();
            new_school_event.name = school_event_table[i]["name"].ToString();
            new_school_event.chance = float.Parse(school_event_table[i]["chance"].ToString());
            
            string need = school_event_table[i]["need"].ToString();
            //해쉬태그 형태로 need리스트에 저장
            while(need.Contains("#") == true)
            {
                 int string_cut = need.LastIndexOf("#", need.Length);
                 string tag_need = need.Substring(string_cut);
                 need = need.Replace(need.Substring(string_cut), "");
                 new_school_event.need.Add(tag_need, tag_need);
            }

            string effect = school_event_table[i]["effect"].ToString();
            //해쉬태그 형태로 effect리스트에 저장
            while (effect.Contains("#") == true)
            {
                int string_cut = effect.LastIndexOf("#", effect.Length);
                string tag_effect = effect.Substring(string_cut);
                effect = effect.Replace(effect.Substring(string_cut), "");
                new_school_event.effect.Add(tag_effect, tag_effect);
            }
            school_event_list.Add(new_school_event);
        }
    }
}
