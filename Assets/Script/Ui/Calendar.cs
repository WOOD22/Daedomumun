using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    public Text year_text;
    public Text month_text;

    Game_Data game_data;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
    }

    void Update()
    {
        if (month_text.text != game_data.dict_gamedata.month.ToString())
        {
            month_text.text = game_data.dict_gamedata.month.ToString();
        }
        if (year_text.text != game_data.dict_gamedata.year.ToString())
        {
            year_text.text = game_data.dict_gamedata.year.ToString();
        }
    }
}
