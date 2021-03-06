using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameSetting : MonoBehaviour
{
    //게임데이터 선언==============================================================================
    GameData gamedata;
    Dict_GameData dict_gamedata;
    //가상키보드===================================================================================
    TouchScreenKeyboard coach_name_keyboard;
    TouchScreenKeyboard school_name_keyboard;
    //텍스트 입력용================================================================================
    public GameObject newgame_coach_name;           //NewGame_Coach_Name 오브젝트
    public GameObject newgame_school_name;          //NewGame_School_Name 오브젝트
    public Text coach_name_text;                    //Text_Coach_Name 텍스트
    public Text school_name_text;                   //Text_Coach_Name 텍스트
    public Text birth_month_text;                   //
    //=============================================================================================
    public School player_school = new School();
    public Coach player_coach = new Coach();
    void Start()
    {
        //플레이어 학교 생성=======================================================================
        gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().gamedata;
        dict_gamedata = GameObject.Find("GameData").GetComponent<Game_Data>().dict_gamedata;
        player_school.code = "0";
        player_coach.code = "COCH000001";
        player_school.coach_code = player_coach.code;
        dict_gamedata.school_dict.Add(player_school.code, player_school);
        dict_gamedata.coach_dict.Add(player_coach.code, player_coach);
        dict_gamedata.year = 1;
        dict_gamedata.month = 1;
        //=========================================================================================
    }

    public void Name_Keyboard()
    {
        //가상키보드 NamePhonePad 타입으로 오픈 ===================================================
        coach_name_keyboard = TouchScreenKeyboard.Open("강백호", TouchScreenKeyboardType.NamePhonePad);
        school_name_keyboard = TouchScreenKeyboard.Open("무림", TouchScreenKeyboardType.NamePhonePad);
        //=========================================================================================
    }

    void Update()
    {
        //확인 -> NewGame_Coach_Name 오브젝트가 액티브되어 있는 경우===============================
        if (coach_name_keyboard != null && coach_name_keyboard.status == TouchScreenKeyboard.Status.Done && newgame_coach_name.activeSelf == true)
        {
            coach_name_text.text = coach_name_keyboard.text;
        }
        //확인 -> NewGame_School_Name 오브젝트가 액티브되어 있는 경우==============================
        if (school_name_keyboard != null && school_name_keyboard.status == TouchScreenKeyboard.Status.Done && newgame_school_name.activeSelf == true)
        {
            school_name_text.text = school_name_keyboard.text;
        }
    }
    //코치 이름 입력 함수==========================================================================
    public void Coach_Name_Agree()
    {
        player_coach.name = coach_name_text.text;
    }
    //코치 성별 결정 함수(남)======================================================================
    public void Coach_Gender_Male_Decision()
    {
        player_coach.gender = 'M';
    }
    //코치 성별 결정 함수(여)======================================================================
    public void Coach_Gender_Female_Decision()
    {
        player_coach.gender = 'F';
    }
    //코치 생월 결정 함수==========================================================================
    public void Coach_Birth_Month_Decision(int birth)
    {
        player_coach.birth_month = birth;
    }
    //학교 이름 확정 함수==========================================================================
    public void School_Name_Agree()
    {
        player_school.name = school_name_text.text;
    }
    //=============================================================================================
    public void Coach_ActiveSkill_Decision(int code)
    {
        ActiveSkill active_skill = new ActiveSkill();
        active_skill = GameObject.Find("GameData").GetComponent<Skill_Data>().active_skill_data[code];
        player_coach.active_skills.Add(active_skill);
    }
    //
    public void Coach_PassiveSkill_Decision(int code)
    {
        PassiveSkill passive_skill = new PassiveSkill();
        passive_skill = GameObject.Find("GameData").GetComponent<Skill_Data>().passive_skill_data[code];
        player_coach.passive_skills.Add(passive_skill);
    }
}
