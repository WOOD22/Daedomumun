using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Data : MonoBehaviour
{
    List<Dictionary<string, object>> active_skill_table;
    public Dictionary<string, ActiveSkill> active_skill_data = new Dictionary<string, ActiveSkill>();

    List<Dictionary<string, object>> passive_skill_table;
    public Dictionary<string, PassiveSkill> passive_skill_data = new Dictionary<string, PassiveSkill>();

    private void Start()
    {
        active_skill_table = CSVReader.Read("DataBase/CSV/Active_Skill_Table");
        passive_skill_table = CSVReader.Read("DataBase/CSV/Passive_Skill_Table");

        for (int i = 0; i < active_skill_table.Count; i++)
        {
            ActiveSkill new_activeskill = new ActiveSkill();

            new_activeskill.code = active_skill_table[i]["code"].ToString();
            new_activeskill.name = active_skill_table[i]["name"].ToString();
            new_activeskill.need = active_skill_table[i]["need"].ToString();
            new_activeskill.qualification = active_skill_table[i]["qualification"].ToString();
            new_activeskill.effect = active_skill_table[i]["effect"].ToString();

            active_skill_data.Add(new_activeskill.code, new_activeskill);
        }

        for (int i = 0; i < passive_skill_table.Count; i++)
        {
            PassiveSkill new_passiveskill = new PassiveSkill();

            new_passiveskill.code = passive_skill_table[i]["code"].ToString();
            new_passiveskill.name = passive_skill_table[i]["name"].ToString();
            new_passiveskill.need = passive_skill_table[i]["need"].ToString();
            new_passiveskill.qualification = passive_skill_table[i]["qualification"].ToString();
            new_passiveskill.effect = passive_skill_table[i]["effect"].ToString();

            passive_skill_data.Add(new_passiveskill.code, new_passiveskill);
        }
    }
}
