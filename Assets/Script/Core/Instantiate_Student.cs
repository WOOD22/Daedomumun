using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Student : MonoBehaviour
{
    List<Dictionary<string, object>> last_name_table;
    List<Dictionary<string, object>> first_name_male_table;
    List<Dictionary<string, object>> first_name_female_table;
    //���� ����====================================================================================
    public string last_name;
    public string first_name;
    public char gender;
    public int school_class;
    public int birth_month;
    //�ɷ�ġ ����==================================================================================
    public float st_STR, pt_STR;    
    public float st_DEX, pt_DEX;    
    public float st_CON, pt_CON;    
    public float st_INT, pt_INT;    
    public float st_WIS, pt_WIS;    
    public float st_WIL, pt_WIL;    
    public float st_LUK;
    //CSV���� ȣ��=================================================================================
    void Start()
    {
        last_name_table = CSVReader.Read("DataBase/CSV/LastName");
        first_name_male_table = CSVReader.Read("DataBase/CSV/FirstName_Male");
        first_name_female_table = CSVReader.Read("DataBase/CSV/FirstName_Female");
    }
    //�л� ���� ����===============================================================================
    public void Set_Student()
    {
        Set_Gender();
        Set_Name(gender);
        Set_School_Class();
        Set_Birth_Month();
        Set_Stat(school_class,'C');
    }
    //���� ����====================================================================================
    void Set_Gender()
    {
        if(Random.Range(0,2) == 0)
        {
            gender = 'M';
        }
        else
        {
            gender = 'F';
        }
    }
    //�̸� ����====================================================================================
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
    //���� ����====================================================================================
    void Set_LastName()
    {
        int num = Random.Range(0, int.Parse(last_name_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < last_name_table.Count; i++)
        {
            if (total < num)
            {
                last_name = last_name_table[i]["name"].ToString();
                Debug.Log(total + last_name);
            }
            total += int.Parse(last_name_table[i]["number"].ToString());
        }
    }
    //���� �̸�====================================================================================
    void Set_FirstName_Male()
    {
        int num = Random.Range(0, int.Parse(first_name_male_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < first_name_male_table.Count; i++)
        {
            if (total < num)
            {
                first_name = first_name_male_table[i]["name"].ToString();
                Debug.Log(total + first_name);
            }
            total += int.Parse(first_name_male_table[i]["number"].ToString());
        }
    }
    //���� �̸�====================================================================================
    void Set_FirstName_Female()
    {
        int num = Random.Range(0, int.Parse(first_name_female_table[0]["total"].ToString()));
        int total = 0;

        for (int i = 0; i < first_name_female_table.Count; i++)
        {
            if (total < num)
            {
                first_name = first_name_female_table[i]["name"].ToString();
                Debug.Log(total + first_name);
            }
            total += int.Parse(first_name_female_table[i]["number"].ToString());
        }
    }
    //�г�=========================================================================================
    void Set_School_Class()
    {
        school_class = Random.Range(1, 4);
    }
    //����=========================================================================================
    void Set_Birth_Month()
    {
        birth_month = Random.Range(1, 13);
    }
    //���� ����(Rank�� ����Ͽ� �����, ���� ���� ����, �г⿡ ����Ͽ� ���� ���� ����)============
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

            st_STR = Random.Range(pt_STR / 5 * _school_class, pt_STR / 3 * _school_class);
            st_DEX = Random.Range(pt_DEX / 5 * _school_class, pt_DEX / 3 * _school_class);
            st_CON = Random.Range(pt_CON / 5 * _school_class, pt_CON / 3 * _school_class);
            st_INT = Random.Range(pt_INT / 5 * _school_class, pt_INT / 3 * _school_class);
            st_WIS = Random.Range(pt_WIS / 5 * _school_class, pt_WIS / 3 * _school_class);
            st_WIL = Random.Range(pt_WIL / 5 * _school_class, pt_WIL / 3 * _school_class);

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

            st_STR = Random.Range(pt_STR / 5 * _school_class, pt_STR / 3 * _school_class);
            st_DEX = Random.Range(pt_DEX / 5 * _school_class, pt_DEX / 3 * _school_class);
            st_CON = Random.Range(pt_CON / 5 * _school_class, pt_CON / 3 * _school_class);
            st_INT = Random.Range(pt_INT / 5 * _school_class, pt_INT / 3 * _school_class);
            st_WIS = Random.Range(pt_WIS / 5 * _school_class, pt_WIS / 3 * _school_class);
            st_WIL = Random.Range(pt_WIL / 5 * _school_class, pt_WIL / 3 * _school_class);

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

            st_STR = Random.Range(pt_STR / 5 * _school_class, pt_STR / 3 * _school_class);
            st_DEX = Random.Range(pt_DEX / 5 * _school_class, pt_DEX / 3 * _school_class);
            st_CON = Random.Range(pt_CON / 5 * _school_class, pt_CON / 3 * _school_class);
            st_INT = Random.Range(pt_INT / 5 * _school_class, pt_INT / 3 * _school_class);
            st_WIS = Random.Range(pt_WIS / 5 * _school_class, pt_WIS / 3 * _school_class);
            st_WIL = Random.Range(pt_WIL / 5 * _school_class, pt_WIL / 3 * _school_class);

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

            st_STR = Random.Range(pt_STR / 5 * _school_class, pt_STR / 3 * _school_class);
            st_DEX = Random.Range(pt_DEX / 5 * _school_class, pt_DEX / 3 * _school_class);
            st_CON = Random.Range(pt_CON / 5 * _school_class, pt_CON / 3 * _school_class);
            st_INT = Random.Range(pt_INT / 5 * _school_class, pt_INT / 3 * _school_class);
            st_WIS = Random.Range(pt_WIS / 5 * _school_class, pt_WIS / 3 * _school_class);
            st_WIL = Random.Range(pt_WIL / 5 * _school_class, pt_WIL / 3 * _school_class);

            st_LUK = Random.Range(0.0f, 200.0f);
        }
    }
}