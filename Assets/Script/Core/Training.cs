using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    public GameObject prefab_portrait_card;
    public GameObject content;
    public School player_school = new School();

    public void Init_Training_Unit()
    {
        player_school = GameObject.Find("GameData").GetComponent<Game_Data>().game_data.schools[1];

        for (int i = 0; i < player_school.students.Count; i++)
        {
            GameObject instance;
            instance = Instantiate(prefab_portrait_card, content.transform);
            instance.GetComponent<Prefab_Portrait_Card_Property>().student = player_school.students[i];
        }
    }
}
