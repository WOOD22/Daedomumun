using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Training_Infra_Add : MonoBehaviour
{
    public School school;
    Training_Infra training_infra;

    public void Buy_Training_Infra(string code)
    {
        school = GameObject.Find("GameData").GetComponent<Game_Data>().gamedata.school_list[1];
        training_infra = GameObject.Find("GameData").GetComponent<Training_Infra_Data>().training_infra_data[int.Parse(code)];
        if (training_infra.cost <= school.money)
        {
            Training_Infra new_training_infra = new Training_Infra();
            new_training_infra.code = training_infra.code;
            new_training_infra.name = training_infra.name;
            new_training_infra.cost = training_infra.cost;
            school.money = school.money - training_infra.cost;
            school.training_infra_list.Add(new_training_infra);
        }
    }
}
