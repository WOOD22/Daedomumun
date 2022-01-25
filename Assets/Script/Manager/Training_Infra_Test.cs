using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Infra_Test : MonoBehaviour
{
    GameData game_data;
    Dictionary<int, Training_Infra> training_infra_data = new Dictionary<int, Training_Infra>();
    public List<Training_Infra> training_Infras = new List<Training_Infra>();
    void Start()
    {
        training_infra_data = GameObject.Find("GameData").GetComponent<Training_Infra_Data>().training_infra_data;
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
    }

    public void infra_test()
    {
        training_Infras = game_data.school_list[1].training_infra_list;
        training_Infras.Add(training_infra_data[0]);
        training_Infras.Add(training_infra_data[1]);
        training_Infras.Add(training_infra_data[2]);
        training_Infras.Add(training_infra_data[3]);
    }
}
