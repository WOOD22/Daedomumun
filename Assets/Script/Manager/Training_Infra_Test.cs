using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Infra_Test : MonoBehaviour
{
    GameData game_data;
    List<Dictionary<string, object>> training_infra_table;
    List<Training_Infra> training_Infras = new List<Training_Infra>();
    void Start()
    {
        training_infra_table = CSVReader.Read("DataBase/CSV/Training_Infra_Table");
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
        training_Infras = game_data.school_list[1].training_infra_list;
    }
}
