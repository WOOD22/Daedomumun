﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Data : MonoBehaviour
{
    public GameData game_data;
}
//게임 데이터 클래스===============================================================================
[System.Serializable]
public class GameData
{
    public int year;//게임 진행 년도
    public int month;//현재 월
    public List<School> school_list;//학교 리스트
    public List<Coach> coach_list;//코치 리스트
    public List<Student> student_list;//학생 리스트
}
//격투부 클래스====================================================================================
[System.Serializable]
public class School
{
    //인물 정보====================================================================================
    public string code;                                         //학교의 코드(0~999)
    public string name;                                         //학교의 이름
    public Coach coach = new Coach();                           //학교의 코치
    public List<Student> students = new List<Student>();        //학교의 학생 리스트
    //상태 정보====================================================================================
    public int prestige;                                        //학교의 명망(학생의 대회 성적, 프로데뷔 여부로 상승)
}
//코치 클래스======================================================================================
[System.Serializable]
public class Coach
{
    //개인 정보====================================================================================
    public string code;             //코치의 코드 01(생월)/0000(중복되지 않는 랜덤)
    public string name;             //코치의 이름
    public char gender;             //코치의 성별(M : 남성, F : 여성)
    public string nickname;         //코치의 별명
    public int birth_month;         //코치의 생월
    //상태 정보====================================================================================
    public int condition;           //컨디션 (기본 최대치 100, 배치한 훈련의 효과 = 훈련 효과 수치 * (1 + (condition/100)))
    public string training;         //이번 턴 배정된 훈련(없을 시 NONE 으로 초기화)
    public string main_MA;          //주 무공 종류(권, 장, 각 3타입)
    //교육 가능한 스킬 목록(최대 4개)==============================================================
    public List<ActiveSkill> active_skills = new List<ActiveSkill>();
    public List<PassiveSkill> passive_skills = new List<PassiveSkill>();
}
//학생 클래스======================================================================================
[System.Serializable]
public class Student
{
    //개인 정보====================================================================================
    public string code;             //학생의 코드 01(생월)/0000(중복되지 않는 랜덤)
    public string name;             //학생의 이름
    public char gender;             //학생의 성별(M : 남성, F : 여성)
    public string nickname;         //학생의 별명
    public int birth_month;         //학생의 생월
    public int school_class;        //학년
    public int prestige;            //학생의 명망(대회 성적으로 상승)
    //상태 정보====================================================================================
    public int condition;           //컨디션 (기본 최대치 100, 훈련의 효과 = 훈련 효과 수치 * (condition/100))
    public string training = "NONE";//이번 턴 배정된 훈련(없을 시 NONE 으로 초기화)
    public string schedule = "NONE";//이번 턴 배정된 일정(없을 시 NONE 으로 초기화)
    public string main_MA;          //주 무공 종류(권, 장, 각 3타입)
    //능력치 정보==================================================================================
    public Stat stat = new Stat(); 
    //장착한 스킬==================================================================================
    public ActiveSkill active_skill = new ActiveSkill();
    public PassiveSkill passive_skill = new PassiveSkill();
    //보유한 스킬==================================================================================
    public List<ActiveSkill> active_skills = new List<ActiveSkill>();
    public List<PassiveSkill> passive_skills = new List<PassiveSkill>();
}
[System.Serializable]
public class Stat
{
    //능력치 정보==================================================================================
    public float st_STR, pt_STR;    //근력, 근력 잠재력
    public float st_DEX, pt_DEX;    //민첩, 민첩 잠재력
    public float st_CON, pt_CON;    //체질, 체질 잠재력
    public float st_INT, pt_INT;    //지능, 지능 잠재력
    public float st_WIS, pt_WIS;    //지혜, 지혜 잠재력
    public float st_WIL, pt_WIL;    //의지, 의지 잠재력
    public float st_LUK;            //운(표시되지 않음)
}
//액티브 스킬 클래스===============================================================================
[System.Serializable]
public class ActiveSkill
{
    public int code;                //스킬의 코드 00(타입)/0000(작성 순서)
    public string name;             //스킬의 이름
    public string main_MA;          //무공 종류(권, 장, 각 3타입)
    public string need;             //사용에 필요한 조건
    public string qualification;    //스킬 습득 조건(정해진 규칙에 따라 작성)
    public string effect;           //스킬의 효과(정해진 규칙에 따라 작성)
}
//패시브 스킬 클래스===============================================================================
[System.Serializable]
public class PassiveSkill
{
    public int code;                //스킬의 코드 00(타입)/0000(작성 순서)
    public string name;             //스킬의 이름
    public string main_MA;          //무공 종류(권, 장, 각 3타입)
    public string need;             //사용에 필요한 조건
    public string qualification;    //스킬 습득 조건(정해진 규칙에 따라 작성)
    public string effect;           //스킬의 효과(정해진 규칙에 따라 작성)
}
//훈련 기반 시설===================================================================================
[System.Serializable]
public class Training_Infra
{
    public string name;                                 //이름
    public int num;                                     //갯수
    public int cost;                                    //설치 비용
    public int upgrade;                                 //업그레이드 횟수
    public float change_STR, change_DEX, change_CON;    //육체 능력치 변화량
    public float change_INT, change_WIS, change_WIL;    //정신 능력치 변화량
}

    
    