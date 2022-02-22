using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Turn : MonoBehaviour
{
    Game_Data game_data;
    Save_Load save_load;

    void Start()
    {
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>();
        save_load = GameObject.Find("GameData").GetComponent<Save_Load>();
    }
    //턴 종료 시 계산==============================================================================
    public void End_Turn_Check()
    {
        //Dict에 있는 데이터를 GameData로 옮긴다.
        save_load.From_Dict_to_Data();
        game_data.gamedata.month++;
        if(game_data.gamedata.month > 12)
        {
            game_data.gamedata.month = 1;
            game_data.gamedata.year++;
        }
        Training_Page_Check();
        Schedule_Page_Check();
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
    //2개월 앞의 일정 생성=========================================================================
    void Schedule_Page_Check()
    {
        List<Schedule> schedule_data_temp = GameObject.Find("GameData").GetComponent<Schedule_Data>().schedule_data_temp;
        for (int i = 0; i < schedule_data_temp.Count; i++)
        {
            Schedule new_schedule = new Schedule();
            //1~10월
            if (schedule_data_temp[i].month == game_data.gamedata.month + 2)
            {
                new_schedule = schedule_data_temp[i];
                new_schedule.year = 0;
                game_data.gamedata.schedule_list.Add(new_schedule);
            }
            //11~12월
            else if (schedule_data_temp[i].month == game_data.gamedata.month - 10)
            {
                new_schedule = schedule_data_temp[i];
                new_schedule.year = 1;
                game_data.gamedata.schedule_list.Add(new_schedule);
            }
        }
    }
    void Set_Schedule_Code()
    {

    }
}
