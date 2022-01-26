using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Infra_Add : MonoBehaviour
{
    public School school;
    public void Buy_Training_Infra(string code)
    {
        school = GameObject.Find("GameData").GetComponent<Game_Data>().game_data.school_list[1];
        Training_Infra training_Infra = GameObject.Find("GameData").GetComponent<Training_Infra_Data>().training_infra_data[int.Parse(code)];
        if (training_Infra.cost <= school.money)
        {
            school.money = school.money - training_Infra.cost;
            school.training_infra_list.Add(training_Infra);
        }
    }
}
