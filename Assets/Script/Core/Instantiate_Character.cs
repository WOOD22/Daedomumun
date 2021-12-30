using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_Character : MonoBehaviour
{
    List<Dictionary<string, object>> school_name_table;
    List<Dictionary<string, object>> last_name_table;
    List<Dictionary<string, object>> first_name_male_table;
    List<Dictionary<string, object>> first_name_female_table;

    GameData game_data;

    Dictionary<int, ActiveSkill> active_skill_data;
    Dictionary<int, PassiveSkill> passive_skill_data;
    public string school_name;
    //캐릭터 개인 정보=============================================================================
    public string last_name;
    public string first_name;
    public char gender;
    public int school_class;
    public int birth_month;
    public string main_MA;
    public int prestige;
    //캐릭터 능력치 정보===========================================================================
    public float st_STR, pt_STR;    
    public float st_DEX, pt_DEX;    
    public float st_CON, pt_CON;    
    public float st_INT, pt_INT;    
    public float st_WIS, pt_WIS;    
    public float st_WIL, pt_WIL;    
    public float st_LUK;
    //보유한 스킬==================================================================================
    public List<ActiveSkill> active_skills;
    public List<PassiveSkill> passive_skills;
    //CSV파일 호출=================================================================================
    void Start()
    {
        school_name_table = CSVReader.Read("DataBase/CSV/SchoolName");              //학교이름 CSV
        last_name_table = CSVReader.Read("DataBase/CSV/LastName");                  //성씨 CSV
        first_name_male_table = CSVReader.Read("DataBase/CSV/FirstName_Male");      //남자이름 CSV
        first_name_female_table = CSVReader.Read("DataBase/CSV/FirstName_Female");  //여자이름 CSV

        active_skill_data = GameObject.Find("GameData").GetComponent<Skill_Data>().active_skill_data;
        passive_skill_data = GameObject.Find("GameData").GetComponent<Skill_Data>().passive_skill_data;
        game_data = GameObject.Find("GameData").GetComponent<Game_Data>().game_data;
    }
    //새로운 학교 생성=============================================================================
    public void Set_School()
    {
        School new_school = new School();
        Set_SchoolName();
        Set_Coach(new_school);

        for (int i = 0; i < Random.Range(3, 8); i++)
        {
            Set_Student(new_school);
        }

        new_school.name = school_name;
        new_school.prestige = Random.Range(0, 1000);

        game_data.schools.Add(new_school);
    }
    //코치 생성====================================================================================
    public void Set_Coach(School i_school)
    {
        Coach new_coach = new Coach();

        Set_Gender();
        Set_Name(gender);
        Set_Birth_Month(); 
        Set_Main_MA();

        new_coach.name = last_name + first_name;
        new_coach.gender = gender;
        new_coach.birth_month = birth_month;
        new_coach.main_MA = main_MA;

        i_school.coach = new_coach;
    }
    //학생 생성====================================================================================
    public void Set_Student(School i_school)
    {
        Student new_student = new Student();

        Set_Gender();
        Set_Name(gender);
        Set_School_Class();
        Set_Birth_Month();
        Set_Main_MA();
        Set_Stat(school_class,'C');

        new_student.name = last_name + first_name;
        new_student.gender = gender;
        new_student.birth_month = birth_month;
        new_student.main_MA = main_MA;

        i_school.students.Add(new_student);
    }
    //학교 이름 세팅===============================================================================
    void Set_SchoolName()
    {
        int i = Random.Range(0, school_name_table.Count);
        school_name = school_name_table[i]["school_name"].ToString();
    }
    //성별 세팅====================================================================================
    void Set_Gender()
    {
        if(Random.Range(0,100) < 70)
        {
            gender = 'M';
        }
        else
        {
            gender = 'F';
        }
    }
    //---------------------------------------------------------------------------------------------
    //이름 세팅====================================================================================
    void Set_Name(char _gender)
    {
        Set_LastName();
        if(_gender == 'M')
        {
            Set_FirstName_Male();
        }
        else if(_gender == 'F')
        {
            Set_FirstName_Female();
        }
    }
    //성씨 세팅====================================================================================
    void Set_LastName()
    {
        int num = Random.Range(0, int.Parse(last_name_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < last_name_table.Count; i++)
        {
            if (total < num)
            {
                last_name = last_name_table[i]["name"].ToString();
            }
            total += int.Parse(last_name_table[i]["number"].ToString());
        }
    }
    //남성 이름====================================================================================
    void Set_FirstName_Male()
    {
        int num = Random.Range(0, int.Parse(first_name_male_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < first_name_male_table.Count; i++)
        {
            if (total < num)
            {
                first_name = first_name_male_table[i]["name"].ToString();
            }
            total += int.Parse(first_name_male_table[i]["number"].ToString());
        }
    }
    //여성 이름====================================================================================
    void Set_FirstName_Female()
    {
        int num = Random.Range(0, int.Parse(first_name_female_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < first_name_female_table.Count; i++)
        {
            if (total < num)
            {
                first_name = first_name_female_table[i]["name"].ToString();
            }
            total += int.Parse(first_name_female_table[i]["number"].ToString());
        }
    }
    //---------------------------------------------------------------------------------------------
    //학년=========================================================================================
    void Set_School_Class()
    {
        school_class = Random.Range(1, 4);
    }
    //생월=========================================================================================
    void Set_Birth_Month()
    {
        birth_month = Random.Range(1, 13);
    }
    //주 무공======================================================================================
    void Set_Main_MA()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            main_MA = "권";
        }
        else if (r == 1)
        { 
            main_MA = "장";
        }
        else if (r == 2)
        {
            main_MA = "각";
        }
    }
    //스탯 세팅(Rank에 비례하여 잠재력, 현재 스탯 결정, 학년에 비례하여 현재 스탯 결정)============
    void Set_Stat(int _school_class, char rank)
    {
        if (rank == 'S')
        {
            pt_STR = Random.Range(160.0f, 200.0f);
            pt_DEX = Random.Range(160.0f, 200.0f);
            pt_CON = Random.Range(160.0f, 200.0f);
            pt_INT = Random.Range(160.0f, 200.0f);
            pt_WIS = Random.Range(160.0f, 200.0f);
            pt_WIL = Random.Range(160.0f, 200.0f);

            st_STR = Random.Range(pt_STR / 10 * _school_class, pt_STR / 6 * _school_class);
            st_DEX = Random.Range(pt_DEX / 10 * _school_class, pt_DEX / 6 * _school_class);
            st_CON = Random.Range(pt_CON / 10 * _school_class, pt_CON / 6 * _school_class);
            st_INT = Random.Range(pt_INT / 10 * _school_class, pt_INT / 6 * _school_class);
            st_WIS = Random.Range(pt_WIS / 10 * _school_class, pt_WIS / 6 * _school_class);
            st_WIL = Random.Range(pt_WIL / 10 * _school_class, pt_WIL / 6 * _school_class);

            st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'A')
        {
            pt_STR = Random.Range(120.0f, 200.0f);
            pt_DEX = Random.Range(120.0f, 200.0f);
            pt_CON = Random.Range(120.0f, 200.0f);
            pt_INT = Random.Range(120.0f, 200.0f);
            pt_WIS = Random.Range(120.0f, 200.0f);
            pt_WIL = Random.Range(120.0f, 200.0f);

            st_STR = Random.Range(pt_STR / 10 * _school_class, pt_STR / 6 * _school_class);
            st_DEX = Random.Range(pt_DEX / 10 * _school_class, pt_DEX / 6 * _school_class);
            st_CON = Random.Range(pt_CON / 10 * _school_class, pt_CON / 6 * _school_class);
            st_INT = Random.Range(pt_INT / 10 * _school_class, pt_INT / 6 * _school_class);
            st_WIS = Random.Range(pt_WIS / 10 * _school_class, pt_WIS / 6 * _school_class);
            st_WIL = Random.Range(pt_WIL / 10 * _school_class, pt_WIL / 6 * _school_class);

            st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'B')
        {
            pt_STR = Random.Range(100.0f, 200.0f);
            pt_DEX = Random.Range(100.0f, 200.0f);
            pt_CON = Random.Range(100.0f, 200.0f);
            pt_INT = Random.Range(100.0f, 200.0f);
            pt_WIS = Random.Range(100.0f, 200.0f);
            pt_WIL = Random.Range(100.0f, 200.0f);

            st_STR = Random.Range(pt_STR / 10 * _school_class, pt_STR / 6 * _school_class);
            st_DEX = Random.Range(pt_DEX / 10 * _school_class, pt_DEX / 6 * _school_class);
            st_CON = Random.Range(pt_CON / 10 * _school_class, pt_CON / 6 * _school_class);
            st_INT = Random.Range(pt_INT / 10 * _school_class, pt_INT / 6 * _school_class);
            st_WIS = Random.Range(pt_WIS / 10 * _school_class, pt_WIS / 6 * _school_class);
            st_WIL = Random.Range(pt_WIL / 10 * _school_class, pt_WIL / 6 * _school_class);

            st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'C')
        {
            pt_STR = Random.Range(60.0f, 200.0f);
            pt_DEX = Random.Range(60.0f, 200.0f);
            pt_CON = Random.Range(60.0f, 200.0f);
            pt_INT = Random.Range(60.0f, 200.0f);
            pt_WIS = Random.Range(60.0f, 200.0f);
            pt_WIL = Random.Range(60.0f, 200.0f);

            st_STR = Random.Range(pt_STR / 10 * _school_class, pt_STR / 6 * _school_class);
            st_DEX = Random.Range(pt_DEX / 10 * _school_class, pt_DEX / 6 * _school_class);
            st_CON = Random.Range(pt_CON / 10 * _school_class, pt_CON / 6 * _school_class);
            st_INT = Random.Range(pt_INT / 10 * _school_class, pt_INT / 6 * _school_class);
            st_WIS = Random.Range(pt_WIS / 10 * _school_class, pt_WIS / 6 * _school_class);
            st_WIL = Random.Range(pt_WIL / 10 * _school_class, pt_WIL / 6 * _school_class);

            st_LUK = Random.Range(0.0f, 200.0f);
        }
    }
    //무공 세팅(임시)==============================================================================
    void Set_Active_Skills(int _school_class)
    {
        active_skills.Add(active_skill_data[0]);
    }
}