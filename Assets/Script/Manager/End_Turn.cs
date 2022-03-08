using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Turn : MonoBehaviour
{
    Game_Data game_data;
    Save_Load save_load;
    School_Event_Data school_event_data;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        save_load = GameObject.Find("GameData").GetComponent<Save_Load>();
        school_event_data = GameObject.Find("GameData").GetComponent<School_Event_Data>();
    }
    //턴 종료 시 계산==============================================================================
    public void End_Turn_Check()
    {
        //Dict에 있는 데이터를 GameData로 옮긴다.
        save_load.From_Dict_to_Data();
        //이벤트 발동
        for (int i = 0; i < school_event_data.school_event_list.Count; i++)
        {
            for (int j = 0; j < game_data.gamedata.school_list.Count; j++)
            {
                GameObject.Find("Event_Bus").GetComponent<School_Event_Bus>().School_Event_Ride
                    (school_event_data.school_event_list[i], game_data.gamedata.school_list[j]);
            }
            GameObject.Find("Event_Bus").GetComponent<School_Event_Bus>().School_Event_Effect
                    (school_event_data.school_event_list[i]);
        }
        //달력 넘기기
        game_data.gamedata.month++;
        if(game_data.gamedata.month > 12)
        {
            game_data.gamedata.month = 1;
            game_data.gamedata.year++;
        }
        //트레이닝 적용
        Training_Page_Check();
        //공동 일정 적용
        Public_Schedule_Page_Check();
        //GameData를 Dict에 옮긴다.
        save_load.From_Data_to_Dict();
    }
    //트레이닝 효과 적용===========================================================================
    void Training_Page_Check()
    {
        for (int i = 0; i < game_data.gamedata.student_list.Count; i++)
        {
            Student student = game_data.gamedata.student_list[i];
            Training_Infra_Data training_infra_data = GameObject.Find("GameData").GetComponent<Training_Infra_Data>();

            if (student.training != "NONE")
            {
                for (int j = 0; j < training_infra_data.training_infra_table.Count; j++)
                {
                    if (student.training == training_infra_data.training_infra_table[j]["name"].ToString())
                    {
                        student.stat.st_STR += student.stat.pt_STR / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_STR"].ToString());
                        student.stat.st_DEX += student.stat.pt_DEX / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_DEX"].ToString());
                        student.stat.st_CON += student.stat.pt_CON / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_CON"].ToString());
                        student.stat.st_INT += student.stat.pt_INT / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_INT"].ToString());
                        student.stat.st_WIS += student.stat.pt_WIS / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_WIS"].ToString());
                        student.stat.st_WIL += student.stat.pt_WIL / 100 * float.Parse(training_infra_data.training_infra_table[j]["change_WIL"].ToString());
                    }
                }
            }
        }
    }
    //2개월치 공개 일정 생성=========================================================================
    void Public_Schedule_Page_Check()
    {
        List<Schedule> schedule_data_temp = GameObject.Find("GameData").GetComponent<Schedule_Data>().schedule_data_temp;
        for (int i = 0; i < schedule_data_temp.Count; i++)
        {
            Schedule new_schedule = new Schedule();
            //1~10월
            if (schedule_data_temp[i].month == game_data.gamedata.month + 2)
            {
                new_schedule = schedule_data_temp[i];
                new_schedule.year = game_data.gamedata.year;
                new_schedule.code = Set_Schedule_Code(new_schedule);//코드 매기기
                game_data.gamedata.start_schedule_list.Add(new_schedule);
                game_data.dict_gamedata.start_schedule_dict.Add(new_schedule.code, new_schedule);
            }
            //11~12월
            else if (schedule_data_temp[i].month == game_data.gamedata.month - 10)
            {
                new_schedule = schedule_data_temp[i];
                new_schedule.year = game_data.gamedata.year + 1;
                new_schedule.code = Set_Schedule_Code(new_schedule);//코드 매기기
                game_data.gamedata.start_schedule_list.Add(new_schedule);
                game_data.dict_gamedata.start_schedule_dict.Add(new_schedule.code, new_schedule);
            }
        }
    }
    //일정에 코드 매기기(Struct구조체라 return으로 값을 줘야함)====================================
    string Set_Schedule_Code(Schedule new_schedule)
    {
        while (true)
        {
            new_schedule.code = "S";

            if (new_schedule.year < 10)
            {
                new_schedule.code = new_schedule.code + "0" + new_schedule.year.ToString();
            }
            else
            {
                new_schedule.code = new_schedule.code + new_schedule.year.ToString();
            }

            if (new_schedule.month < 10)
            {
                new_schedule.code = new_schedule.code + "0" + new_schedule.month.ToString();
            }
            else
            {
                new_schedule.code = new_schedule.code + new_schedule.month.ToString();
            }

            new_schedule.code = new_schedule.code + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString()
                                + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();//랜덤 4자리 숫자 추가;

            if (game_data.dict_gamedata.start_schedule_dict.ContainsKey(new_schedule.code) == false)
            {
                break;
            }
        }
        return new_schedule.code;
    }
}
