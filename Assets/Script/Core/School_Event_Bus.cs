using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#game_data.dict_gamedata에 접근하지 않는다.
public class School_Event_Bus : MonoBehaviour
{
    Game_Data game_data;

    List<int> ok_month_list;
    bool ok_month;
    //이외의 모든 조건이 만족할 경우 true가 됨
    bool have_ticket;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
    }

    public void School_Event_Ride(School_Event school_event, School school)
    {
        ok_month_list = new List<int>();     //리스트에 해당 시 해당 조건완료
        ok_month = true;

        have_ticket = false;
        //#조건:월
        for (int i = 1; i < 12; i++)
        {
            if (school_event.need.ContainsKey("#month:" + i))
            {
                ok_month_list.Add(i);
                ok_month = false;
            }
        }
        //반복문 사용 자제를 위해 조건 중 false가 존재할 때만 체크함
        if (!ok_month)
        {
            //이벤트 버스 티켓 체크
            School_Event_Ticket_Check(school_event, school);
        }

        //모든 조건 true시 버스 탑승
        if (ok_month)
        {
            //확률 굴림
            if (school_event.chance >= Random.Range(0, 100.0f))
            {
                school_event.school_code_list.Add(school.code);
            }
        }

        //School_Event_Effect(school_event);
    }
    //티켓 체크 함수
    void School_Event_Ticket_Check(School_Event school_event, School school)
    {
        if (ok_month_list.Contains(game_data.gamedata.month))
        {
            ok_month = true;
        }
        //모든 조건 완료 시 탑승 가능
        if (ok_month)
        {
            have_ticket = true;
        }
    }
    //이벤트 효과
    public void School_Event_Effect(School_Event school_event)
    {
        //#효과:신입생입부
        if(school_event.effect.ContainsKey("#freshman"))
        {
            for (int i = 0; i < school_event.school_code_list.Count; i++)
            {
                int freshman_count = Random.Range(0, int.Parse(Mathf.Ceil(game_data.gamedata.school_list[i].prestige / 100).ToString()));
                //for (int j = 0; j < freshman_count; j++)
                {
                    GameObject.Find("GameManager").GetComponent<Instantiate_Character>().New_Student(school_event.school_code_list[i]);
                }
            }
        }
    }
}
