using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_Game : MonoBehaviour
{
    GameData game_data;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
    }

    public void Set_School()
    {
        School new_school = new School();

        new_school.coach.name = "¿µÂù";

        new_school.prestige = Random.Range(0, 1000);

        game_data.schools.Add(new_school);
    }
}
