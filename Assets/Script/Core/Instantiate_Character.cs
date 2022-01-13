using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string school_code;
    //캐릭터 개인 정보=============================================================================
    public string last_name;
    public string first_name;
    public char gender;
    public int school_class;
    public int birth_month;
    public string main_MA;
    public int prestige;
    //캐릭터 능력치================================================================================
    public Stat stat = new Stat();
    //보유한 스킬==================================================================================

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
    public void New_School()
    {
        School new_school = new School();
        Set_SchoolName();
        school_code = game_data.school_list.Count.ToString();

        new_school.name = school_name;
        new_school.code = school_code;
        new_school.prestige = Random.Range(0, 1000);

        game_data.school_list.Add(new_school);
        Set_School(school_code);
    }
    //학교 인원 구성하기===========================================================================
    public void Set_School(string code)
    {
        New_Coach(code);
        for (int i = 0; i < Random.Range(3, 7); i++)
        {
            New_Student(code);
        }
    }
    //코치 생성====================================================================================
    public void New_Coach(string school_code)
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

        Set_Active_Skills(new_coach.main_MA, new_coach.active_skills, 1);
        Set_Passive_Skills(new_coach.main_MA, new_coach.passive_skills, 1);

        game_data.school_list[int.Parse(school_code)].coach = new_coach;
        game_data.coach_list.Add(new_coach);
    }
    //학생 생성====================================================================================
    public void New_Student(string school_code)
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
        new_student.stat = stat;

        game_data.school_list[int.Parse(school_code)].students.Add(new_student);
        game_data.student_list.Add(new_student);
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
            stat.pt_STR = Random.Range(160.0f, 200.0f);
            stat.pt_DEX = Random.Range(160.0f, 200.0f);
            stat.pt_CON = Random.Range(160.0f, 200.0f);
            stat.pt_INT = Random.Range(160.0f, 200.0f);
            stat.pt_WIS = Random.Range(160.0f, 200.0f);
            stat.pt_WIL = Random.Range(160.0f, 200.0f);

            stat.st_STR = Random.Range(stat.pt_STR / 10 * _school_class, stat.pt_STR / 6 * _school_class);
            stat.st_DEX = Random.Range(stat.pt_DEX / 10 * _school_class, stat.pt_DEX / 6 * _school_class);
            stat.st_CON = Random.Range(stat.pt_CON / 10 * _school_class, stat.pt_CON / 6 * _school_class);
            stat.st_INT = Random.Range(stat.pt_INT / 10 * _school_class, stat.pt_INT / 6 * _school_class);
            stat.st_WIS = Random.Range(stat.pt_WIS / 10 * _school_class, stat.pt_WIS / 6 * _school_class);
            stat.st_WIL = Random.Range(stat.pt_WIL / 10 * _school_class, stat.pt_WIL / 6 * _school_class);

            stat.st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'A')
        {
            stat.pt_STR = Random.Range(120.0f, 200.0f);
            stat.pt_DEX = Random.Range(120.0f, 200.0f);
            stat.pt_CON = Random.Range(120.0f, 200.0f);
            stat.pt_INT = Random.Range(120.0f, 200.0f);
            stat.pt_WIS = Random.Range(120.0f, 200.0f);
            stat.pt_WIL = Random.Range(120.0f, 200.0f);

            stat.st_STR = Random.Range(stat.pt_STR / 10 * _school_class, stat.pt_STR / 6 * _school_class);
            stat.st_DEX = Random.Range(stat.pt_DEX / 10 * _school_class, stat.pt_DEX / 6 * _school_class);
            stat.st_CON = Random.Range(stat.pt_CON / 10 * _school_class, stat.pt_CON / 6 * _school_class);
            stat.st_INT = Random.Range(stat.pt_INT / 10 * _school_class, stat.pt_INT / 6 * _school_class);
            stat.st_WIS = Random.Range(stat.pt_WIS / 10 * _school_class, stat.pt_WIS / 6 * _school_class);
            stat.st_WIL = Random.Range(stat.pt_WIL / 10 * _school_class, stat.pt_WIL / 6 * _school_class);

            stat.st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'B')
        {
            stat.pt_STR = Random.Range(100.0f, 200.0f);
            stat.pt_DEX = Random.Range(100.0f, 200.0f);
            stat.pt_CON = Random.Range(100.0f, 200.0f);
            stat.pt_INT = Random.Range(100.0f, 200.0f);
            stat.pt_WIS = Random.Range(100.0f, 200.0f);
            stat.pt_WIL = Random.Range(100.0f, 200.0f);

            stat.st_STR = Random.Range(stat.pt_STR / 10 * _school_class, stat.pt_STR / 6 * _school_class);
            stat.st_DEX = Random.Range(stat.pt_DEX / 10 * _school_class, stat.pt_DEX / 6 * _school_class);
            stat.st_CON = Random.Range(stat.pt_CON / 10 * _school_class, stat.pt_CON / 6 * _school_class);
            stat.st_INT = Random.Range(stat.pt_INT / 10 * _school_class, stat.pt_INT / 6 * _school_class);
            stat.st_WIS = Random.Range(stat.pt_WIS / 10 * _school_class, stat.pt_WIS / 6 * _school_class);
            stat.st_WIL = Random.Range(stat.pt_WIL / 10 * _school_class, stat.pt_WIL / 6 * _school_class);

            stat.st_LUK = Random.Range(0.0f, 200.0f);
        }
        else if (rank == 'C')
        {
            stat.pt_STR = Random.Range(60.0f, 200.0f);
            stat.pt_DEX = Random.Range(60.0f, 200.0f);
            stat.pt_CON = Random.Range(60.0f, 200.0f);
            stat.pt_INT = Random.Range(60.0f, 200.0f);
            stat.pt_WIS = Random.Range(60.0f, 200.0f);
            stat.pt_WIL = Random.Range(60.0f, 200.0f);

            stat.st_STR = Random.Range(stat.pt_STR / 10 * _school_class, stat.pt_STR / 6 * _school_class);
            stat.st_DEX = Random.Range(stat.pt_DEX / 10 * _school_class, stat.pt_DEX / 6 * _school_class);
            stat.st_CON = Random.Range(stat.pt_CON / 10 * _school_class, stat.pt_CON / 6 * _school_class);
            stat.st_INT = Random.Range(stat.pt_INT / 10 * _school_class, stat.pt_INT / 6 * _school_class);
            stat.st_WIS = Random.Range(stat.pt_WIS / 10 * _school_class, stat.pt_WIS / 6 * _school_class);
            stat.st_WIL = Random.Range(stat.pt_WIL / 10 * _school_class, stat.pt_WIL / 6 * _school_class);

            stat.st_LUK = Random.Range(0.0f, 200.0f);
        }
    }
    //액티브 스킬 세팅(임시)=======================================================================
    void Set_Active_Skills(string main_MA, List<ActiveSkill> active_skill_list, int num)
    {
        for (; active_skill_list.Count < num;)
        {
            int i_MA = Random.Range(0, active_skill_data.Count);
            int chance = Random.Range(0, 100);

            if (active_skill_data[i_MA].main_MA == main_MA && chance < 90)
            {
                active_skill_list.Add(active_skill_data[i_MA]);
            }
            else if (active_skill_data[i_MA].main_MA != main_MA && 90 <= chance)
            {
                active_skill_list.Add(active_skill_data[i_MA]);
            }
        }
    }
    //패시브 스킬 세팅(임시)=======================================================================
    void Set_Passive_Skills(string main_MA, List<PassiveSkill> passive_skill_list, int num)
    {
        for (; passive_skill_list.Count < num;)
        {
            int i_MAE = Random.Range(0, passive_skill_data.Count);
            int chance = Random.Range(0, 100);

            if (passive_skill_data[i_MAE].main_MA == main_MA && chance < 90)
            {
                passive_skill_list.Add(passive_skill_data[i_MAE]);
            }
            else if (passive_skill_data[i_MAE].main_MA != main_MA && 90 <= chance)
            {
                passive_skill_list.Add(passive_skill_data[i_MAE]);
            }
        }
    }
}