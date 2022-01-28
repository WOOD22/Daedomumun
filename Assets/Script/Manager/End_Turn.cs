using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Turn : MonoBehaviour
{
    GameData gamedata;

    void Start()
    {
        gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
    }

    public void End_Turn_Check()
    {
        Training_Page_Check();

        void Training_Page_Check()
        {
            for(int i = 0; i < gamedata.student_list.Count; i++)
            {
                Student student = gamedata.student_list[i];
                Training_Infra_Data training_infra_data = GameObject.Find("GameData").GetComponent<Training_Infra_Data>();

                if (student.training != "NONE")
                {
                    for (int j = 0; j < training_infra_data.training_infra_table.Count; j++)
                    {
                        Debug.Log("1");
                        if (student.training == training_infra_data.training_infra_table[j]["name"].ToString())
                        {
                            student.stat.st_STR += student.stat.pt_STR / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_STR"].ToString());
                            student.stat.st_DEX += student.stat.pt_DEX / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_DEX"].ToString());
                            student.stat.st_CON += student.stat.pt_CON / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_CON"].ToString());
                            student.stat.st_INT += student.stat.pt_INT / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_INT"].ToString());
                            student.stat.st_WIS += student.stat.pt_WIS / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_WIS"].ToString());
                            student.stat.st_WIL += student.stat.pt_WIL / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_WIL"].ToString());
                            Debug.Log("2");
                        }
                    }
                }
            }
        }
    }
}
