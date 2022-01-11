using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save_Load : MonoBehaviour
{
    GameData game_data = new GameData();

    public void Save_File(string save_file_name)
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
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
    }
}
