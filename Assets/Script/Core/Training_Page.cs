using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Training_Page : MonoBehaviour
{
    Sort_Portrait_Card sort_portrait_card;

    public GameObject prefab_portrait_card;
    public GameObject prefab_training_slot;
    public GameObject unit_scroll_view_content;
    public GameObject training_list_scroll_view_content;
    public GameObject pool;
    List<string> player_student_code_list;
    List<Training_Infra> player_training_infra_list;

    int portrait_card_in_page = 0;      //페이지 안에 있는 캐릭터 카드 갯수

    Game_Data game_data;
    //트레이닝 페이지 오픈시 작동==================================================================
    public void Open_Training_Page()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        //Portrait_Card문자순으로 정렬=============================================================
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        player_student_code_list = game_data.dict_gamedata.school_dict["0"].student_code_list;
        player_training_infra_list = game_data.dict_gamedata.school_dict["0"].training_infra_list;

        sort_portrait_card.student_list = new List<Student>();

        for (int i = 0; i < player_student_code_list.Count; i++)
        {
            sort_portrait_card.student_list.Add(game_data.dict_gamedata.student_dict[player_student_code_list[i]]);
        }

        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            if (sort_portrait_card.student_list[i].training != "NONE")
            {
                sort_portrait_card.student_list.Remove(sort_portrait_card.student_list[i]);
            }
        }

        sort_portrait_card.Sort_Name(false);
    }
    //트레이닝 페이지 정렬=========================================================================
    public void Sort_Training_Page()
    {
        //sort_portrait_card.student_list.RemoveAll(s => s.training != "NONE");

        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            unit_scroll_view_content.transform.GetChild(i).GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
        }
    }

    void Update()
    {
        try
        {
            //training != NONE 의 경우 정렬 리스트에서 제외
            sort_portrait_card.student_list.RemoveAll(student => student.training != "NONE");
            //training == NONE & 정렬 리스트에 중복이 없을 경우 리스트에 추가
            for (int i = 0; i < player_student_code_list.Count; i++)
            {
                if (sort_portrait_card.student_list.Contains(game_data.dict_gamedata.student_dict[player_student_code_list[i]]) == false && game_data.dict_gamedata.student_dict[player_student_code_list[i]].training == "NONE")
                {
                    sort_portrait_card.student_list.Add(game_data.dict_gamedata.student_dict[player_student_code_list[i]]);
                }
            }
            //training_infra_list의 크기만큼 training_infra(GameObject)생성
            for (int i = training_list_scroll_view_content.transform.childCount; i < player_training_infra_list.Count; i++)
            {
                if (i < player_training_infra_list.Count)
                {
                    GameObject instance;
                    instance = Instantiate(prefab_training_slot, training_list_scroll_view_content.transform);
                    instance.GetComponent<Prefab_Training_Slot_Property>().training_infra = player_training_infra_list[i];
                }
            }
            //sort_portrait_card.student_list에 존재하며 현재 생성되지 않은 Portrait_Card를 unit_scroll_view_content에 생성한다
            for (int i = unit_scroll_view_content.transform.childCount; i < sort_portrait_card.student_list.Count; i++)
            {
                //unit_scroll_view_content에 Portrait_Card 생성하기
                if (i < sort_portrait_card.student_list.Count && portrait_card_in_page < player_student_code_list.Count)
                {
                    GameObject instance;
                    instance = Instantiate(prefab_portrait_card, unit_scroll_view_content.transform);
                    instance.GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
                    instance.name = instance.GetComponent<Prefab_Portrait_Card_Property>().student.code;

                    portrait_card_in_page++;
                }
            }
            //트레이닝이 할당된 Portrait_Card를 트레이닝 슬롯에 생성한다.
            for (int i = 0; i < player_training_infra_list.Count; i++)
            {
                if (player_training_infra_list[i].user_code != "" && portrait_card_in_page < player_student_code_list.Count)
                {
                    GameObject instance;
                    instance = Instantiate(prefab_portrait_card, training_list_scroll_view_content.transform.GetChild(i).Find("Portrait_Card_Slot").transform);
                    instance.GetComponent<Prefab_Portrait_Card_Property>().student = game_data.dict_gamedata.student_dict[player_training_infra_list[i].user_code];
                    instance.name = instance.GetComponent<Prefab_Portrait_Card_Property>().student.code;

                    portrait_card_in_page++;
                }
            }
        }
        catch (NullReferenceException)
        {

        }
    }
}
