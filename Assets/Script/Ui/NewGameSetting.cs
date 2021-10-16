using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameSetting : MonoBehaviour
{
    //게임데이터 호출==============================================================================
    Data game_data;
    //가상키보드===================================================================================
    TouchScreenKeyboard keyboard;
    //텍스트 입력용================================================================================
    public GameObject newgame_coach_name;           //NewGame_Coach_Name 오브젝트
    public GameObject newgame_school_name;          //NewGame_School_Name 오브젝트
    public Text coach_name_text;                    //Text_Coach_Name 텍스트
    public Text school_name_text;                   //Text_Coach_Name 텍스트
    public Text birth_month_text;                   //
    public GameObject scroll_birth_month_content;   //Scroll_Birth_Month 의 Content
    //=============================================================================================
    void Start()
    {
        //플레이어 격투부 생성=====================================================================
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
        game_data.clubs.Add(new Club());
        //=========================================================================================
    }

    public void Name_Keyboard()
    {
        //가상키보드 NamePhonePad 타입으로 오픈 ===================================================
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad);
        //=========================================================================================
    }

    void Update()
    {
        //확인 -> NewGame_Coach_Name 오브젝트가 액티브되어 있는 경우===============================
        if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done && newgame_coach_name.activeSelf == true)
        {
            coach_name_text.text = keyboard.text;
        }
        //확인 -> NewGame_School_Name 오브젝트가 액티브되어 있는 경우==============================
        else if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Done && newgame_school_name.activeSelf == true)
        {
            school_name_text.text = keyboard.text;
        }
        //Scroll_Birth_Month 무한 스크롤===========================================================
        if (scroll_birth_month_content.GetComponent<RectTransform>().localPosition.x > 0)
        {
            scroll_birth_month_content.GetComponent<RectTransform>().localPosition = new Vector2(-1200+(scroll_birth_month_content.GetComponent<RectTransform>().localPosition.x), 0);
        }
        else if (scroll_birth_month_content.GetComponent<RectTransform>().localPosition.x < -1200)
        {
            scroll_birth_month_content.GetComponent<RectTransform>().localPosition = new Vector2(0+ (scroll_birth_month_content.GetComponent<RectTransform>().localPosition.x + 1200), 0);
        }
    }
    //코치 이름 확정 함수==========================================================================
    public void Coach_Name_Agree()
    {
        game_data.clubs[0].coach.name = coach_name_text.text;
    }
    //코치 성별 결정 함수(남)======================================================================
    public void Coach_Gender_Male_Decision()
    {
        game_data.clubs[0].coach.gender = 'M';
    }
    //코치 성별 결정 함수(여)======================================================================
    public void Coach_Gender_Female_Decision()
    {
        game_data.clubs[0].coach.gender = 'F';
    }
    //코치 생월 결정 함수==========================================================================
    public void Coach_Birth_Month_Decision(int birth)
    {
        game_data.clubs[0].coach.birth_month = birth;
    }
    //학교 이름 확정 함수==========================================================================
    public void School_Name_Agree()
    {
        game_data.clubs[0].name = school_name_text.text;
    }
    //=============================================================================================
    public void Add_Rival()
    {
        for (int i = 0; i < 16; i++)
        {

        }
    }
}
