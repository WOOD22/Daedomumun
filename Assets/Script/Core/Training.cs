using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Training : MonoBehaviour
{
    Sort_Portrait_Card sort_portrait_card;

    public GameObject prefab_portrait_card;
    public GameObject content;
    public GameObject pool;
    List<Student> player_student_list;
    //트레이닝 페이지 오픈시 작동==================================================================
    public void Open_Training_Page()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        sort_portrait_card.student_list.Clear();
        //Portrait_Card문자순으로 정렬=============================================================
        player_student_list = GameObject.Find("GameData").GetComponent<Game_Data>().game_data.school_list[1].students;
        sort_portrait_card.student_list = new List<Student>();

        for (int i = 0; i < player_student_list.Count; i++)
        {
            sort_portrait_card.student_list.Add(player_student_list[i]);
        }

        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            if (sort_portrait_card.student_list[i].training != "NONE")
            {
                sort_portrait_card.student_list.Remove(sort_portrait_card.student_list[i]);
            }
        }

        sort_portrait_card.Sort_Name(false);

        //player_student_list에 존재하며 현재 생성되지 않은 Portrait_Card를 content에 생성한다=====
        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            if (content.transform.childCount < sort_portrait_card.student_list.Count)
            {
                GameObject instance;
                instance = Instantiate(prefab_portrait_card, content.transform);
                instance.GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
            }
        }
        Sort_Training_Page();
    }
    //트레이닝 페이지 정렬=========================================================================
    public void Sort_Training_Page()
    {
        //sort_portrait_card.student_list.RemoveAll(s => s.training != "NONE");

        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            content.transform.GetChild(i).GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
        }
    }
    void Update()
    {
        try
        {
            sort_portrait_card.student_list.RemoveAll(student => student.training != "NONE");

            for (int i = 0; i < player_student_list.Count; i++)
            {
                if (sort_portrait_card.student_list.Contains(player_student_list[i]) == false && player_student_list[i].training == "NONE")
                {
                    sort_portrait_card.student_list.Add(player_student_list[i]);
                }
            }
        }
        catch (NullReferenceException)
        {

        }
    }
}
