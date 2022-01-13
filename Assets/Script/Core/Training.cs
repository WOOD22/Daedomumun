using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Training : MonoBehaviour
{
    Sort_Portrait_Card sort_portrait_card;

    public GameObject prefab_portrait_card;
    public GameObject content;
    public List<Student> player_student_list;

    public void Init_Training_Unit()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        //문자순으로 정렬==========================================================================
        sort_portrait_card.student_list = GameObject.Find("GameData").GetComponent<Game_Data>().game_data.school_list[1].students;
        sort_portrait_card.Sort_Name(false);
        player_student_list = sort_portrait_card.student_list;
        //===========
        for (int i = 0; i < player_student_list.Count; i++)
        {
            GameObject instance;
            instance = Instantiate(prefab_portrait_card, content.transform);
            instance.GetComponent<Prefab_Portrait_Card_Property>().student = player_student_list[i];
        }
    }
}
