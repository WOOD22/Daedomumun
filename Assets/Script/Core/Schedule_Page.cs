using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Schedule_Page : MonoBehaviour
{
    Sort_Portrait_Card sort_portrait_card;

    public GameObject prefab_portrait_card;
    public GameObject prefab_schedule_slot;
    public GameObject unit_scroll_view_content;
    public GameObject schedule_list_scroll_view_content;
    public GameObject pool;
    List<string> player_student_code_list;

    int portrait_card_in_page = 0;      //������ �ȿ� �ִ� ĳ���� ī�� ����

    public Text calendar_month_text;
    int calendar_year = 0;
    int calendar_month = 0;

    Game_Data game_data;
    //������ ������ ���½� �۵�====================================================================
    public void Open_Schedule_Page()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        //Portrait_Card���ڼ����� ����=============================================================
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        player_student_code_list = game_data.dict_gamedata.school_dict["1"].student_code_list;
        //������ ������ �� �޷� �ʱ�ȭ
        calendar_year = game_data.dict_gamedata.year;
        calendar_month = game_data.dict_gamedata.month;
        calendar_month_text.text = calendar_month.ToString();

        sort_portrait_card.student_list = new List<Student>();

        for (int i = 0; i < player_student_code_list.Count; i++)
        {
            sort_portrait_card.student_list.Add(game_data.dict_gamedata.student_dict[player_student_code_list[i]]);
        }

        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            if (sort_portrait_card.student_list[i].schedule != "NONE")
            {
                sort_portrait_card.student_list.Remove(sort_portrait_card.student_list[i]);
            }
        }

        sort_portrait_card.Sort_Name(false);
    }
    //������ ������ ����===========================================================================
    public void Sort_Schedule_Page()
    {
        for (int i = 0; i < sort_portrait_card.student_list.Count; i++)
        {
            unit_scroll_view_content.transform.GetChild(i).GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
        }
    }
    //������ ������ �޷� �� ����===================================================================
    public void Turn_Calendar(bool is_Next)
    {
        //���� �� �̵�
        if (is_Next == true && calendar_month + calendar_year * 12 < game_data.dict_gamedata.month + 2 + game_data.dict_gamedata.year * 12)
        {
            calendar_month++;
        }
        //���� �� �̵�
        else if (is_Next == false && calendar_month + calendar_year * 12 > game_data.dict_gamedata.month - 2 + game_data.dict_gamedata.year * 12)
        {
            calendar_month--;
        }
        if(calendar_month > 12)
        {
            calendar_month = 1;
            calendar_year++;
        }
        else if (calendar_month < 1)
        {
            calendar_month = 12;
            calendar_year--;
        }

        calendar_month_text.text = calendar_month.ToString();
    }

    void Update()
    {
        int slot_count = 0;
        List<Schedule> calendar_schedule = new List<Schedule>();

        try
        {
            //schedule != NONE �� ��� ���� ����Ʈ���� ����
            sort_portrait_card.student_list.RemoveAll(student => student.schedule != "NONE");
            //training == NONE & ���� ����Ʈ�� �ߺ��� ���� ��� ����Ʈ�� �߰�
            for (int i = 0; i < player_student_code_list.Count; i++)
            {
                if (sort_portrait_card.student_list.Contains(game_data.dict_gamedata.student_dict[player_student_code_list[i]]) == false && game_data.dict_gamedata.student_dict[player_student_code_list[i]].schedule == "NONE")
                {
                    sort_portrait_card.student_list.Add(game_data.dict_gamedata.student_dict[player_student_code_list[i]]);
                }
            }
            //player_student_list�� �����ϸ� ���� �������� ���� Portrait_Card�� unit_scroll_view_content�� �����Ѵ�
            for (int i = unit_scroll_view_content.transform.childCount; i < sort_portrait_card.student_list.Count; i++)
            {
                if (i < sort_portrait_card.student_list.Count && portrait_card_in_page < player_student_code_list.Count)
                {
                    GameObject instance;
                    instance = Instantiate(prefab_portrait_card, unit_scroll_view_content.transform);
                    instance.GetComponent<Prefab_Portrait_Card_Property>().student = sort_portrait_card.student_list[i];
                    instance.name = instance.GetComponent<Prefab_Portrait_Card_Property>().student.code;

                    portrait_card_in_page++;
                }
            }
            //calendar_month�� ���� ���� ī��Ʈ====================================================
            for (int i = 0; i < game_data.dict_gamedata.start_schedule_dict.Count; i++)
            {
                if (game_data.gamedata.start_schedule_list[i].month == calendar_month
                    && game_data.gamedata.start_schedule_list[i].year == calendar_year)
                {
                    slot_count++;
                    calendar_schedule.Add(game_data.gamedata.start_schedule_list[i]);
                }
            }
            //�߰� ���� ������ �ʿ��� ��� ���� ���� ����==========================================
            for (int i = schedule_list_scroll_view_content.transform.childCount; i < slot_count; i++)
            {
                GameObject instance;
                instance = Instantiate(prefab_schedule_slot, schedule_list_scroll_view_content.transform);
                instance.GetComponent<Prefab_Schedule_Slot_Property>().schedule = game_data.gamedata.start_schedule_list[i];
            }
            //���� ���Կ� schedule�� �Է��ϱ�======================================================
            for (int i = 0; i < schedule_list_scroll_view_content.transform.childCount; i++)
            {
                if (i < slot_count)
                {
                    schedule_list_scroll_view_content.transform.GetChild(i).gameObject.SetActive(true);
                    schedule_list_scroll_view_content.transform.GetChild(i).GetComponent<Prefab_Schedule_Slot_Property>().schedule = calendar_schedule[i];
                }
                else
                {
                    schedule_list_scroll_view_content.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
        catch (NullReferenceException)
        {

        }
    }
}
