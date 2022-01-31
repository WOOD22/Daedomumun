using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save_Load : MonoBehaviour
{
    GameData game_data = new GameData();
    Dict_GameData dict_gamedata = new Dict_GameData();

    public void Save_File(string save_file_name)
    {
        dict_gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata;
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;

        game_data.year = dict_gamedata.year;
        game_data.month = dict_gamedata.month;
        game_data.school_list = new List<School>(dict_gamedata.school_dict.Values);
        game_data.coach_list = new List<Coach>(dict_gamedata.coach_dict.Values);
        game_data.student_list = new List<Student>(dict_gamedata.student_dict.Values);

        string save = JsonUtility.ToJson(game_data, true);
        string path = Path.Combine(Application.dataPath + "/Save", save_file_name + ".json");
        File.WriteAllText(path, save);
    }

    public void Load_File(string save_file_name)
    {
        string path = Path.Combine(Application.dataPath + "/Save", save_file_name + ".json");
        string save = File.ReadAllText(path);
        game_data = JsonUtility.FromJson<GameData>(save);

        GameObject.Find("GameData").GetComponent<Game_Data>().game_data = game_data;

        dict_gamedata.year = game_data.year;
        dict_gamedata.month = game_data.month;
        for (int i = 0; i < game_data.school_list.Count; i++)
        {
            dict_gamedata.school_dict.Add(game_data.school_list[i].code, game_data.school_list[i]);
        }
        for (int i = 0; i < game_data.coach_list.Count; i++)
        {
            dict_gamedata.coach_dict.Add(game_data.coach_list[i].code, game_data.coach_list[i]);
        }
        for (int i = 0; i < game_data.student_list.Count; i++)
        {
            dict_gamedata.student_dict.Add(game_data.student_list[i].code, game_data.student_list[i]);
        }

        GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata = dict_gamedata;
    }
}
