using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save_Load : MonoBehaviour
{
    GameData gamedata = new GameData();
    Dict_GameData dict_gamedata = new Dict_GameData();
    //파일 저장====================================================================================
    public void Save_File(string save_file_name)
    {
        GameData gamedata = new GameData();

        dict_gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata;
        gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().gamedata;

        gamedata.year = dict_gamedata.year;
        gamedata.month = dict_gamedata.month;
        gamedata.school_list = new List<School>(dict_gamedata.school_dict.Values);
        gamedata.coach_list = new List<Coach>(dict_gamedata.coach_dict.Values);
        gamedata.student_list = new List<Student>(dict_gamedata.student_dict.Values);

        string save = JsonUtility.ToJson(gamedata, true);
        string path = Path.Combine(Application.dataPath + "/Save", save_file_name + ".json");
        File.WriteAllText(path, save);
    }
    //디렉토리 -> 데이터===========================================================================
    public void From_Dict_to_Data()
    {
        GameData gamedata = new GameData();

        dict_gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata;
        gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().gamedata;

        gamedata.year = dict_gamedata.year;
        gamedata.month = dict_gamedata.month;
        gamedata.school_list = new List<School>(dict_gamedata.school_dict.Values);
        gamedata.coach_list = new List<Coach>(dict_gamedata.coach_dict.Values);
        gamedata.student_list = new List<Student>(dict_gamedata.student_dict.Values);
    }
    //파일 로드====================================================================================
    public void Load_File(string save_file_name)
    {
        string path = Path.Combine(Application.dataPath + "/Save", save_file_name + ".json");
        string save = File.ReadAllText(path);
        gamedata = JsonUtility.FromJson<GameData>(save);

        GameObject.Find("GameData").GetComponent<Game_Data>().gamedata = gamedata;

        From_Data_to_Dict();
    }
    //데이터 -> 디렉토리===========================================================================
    public void From_Data_to_Dict()
    {
        Dict_GameData dict_gamedata = new Dict_GameData();

        dict_gamedata.year = gamedata.year;
        dict_gamedata.month = gamedata.month;

        for (int i = 0; i < gamedata.school_list.Count; i++)
        {
            dict_gamedata.school_dict.Add(gamedata.school_list[i].code, gamedata.school_list[i]);
        }
        for (int i = 0; i < gamedata.coach_list.Count; i++)
        {
            dict_gamedata.coach_dict.Add(gamedata.coach_list[i].code, gamedata.coach_list[i]);
        }
        for (int i = 0; i < gamedata.student_list.Count; i++)
        {
            dict_gamedata.student_dict.Add(gamedata.student_list[i].code, gamedata.student_list[i]);
        }
        for (int i = 0; i < gamedata.schedule_list.Count; i++)
        {
            dict_gamedata.schedule_dict.Add(gamedata.schedule_list[i].code, gamedata.schedule_list[i]);
        }

        GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata = dict_gamedata;
    }
}
