using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Student : MonoBehaviour
{
    List<Dictionary<string, object>> last_name_table;
    List<Dictionary<string, object>> first_name_male_table;
    List<Dictionary<string, object>> first_name_female_table;

    public string last_name;
    public string first_name;

    void Start()
    {
        last_name_table = CSVReader.Read("DataBase/CSV/LastName");
        first_name_male_table = CSVReader.Read("DataBase/CSV/FirstName_Male");
        first_name_female_table = CSVReader.Read("DataBase/CSV/FirstName_Female");
    }

    public void Set_Name()
    {
        Set_LastName();
        Set_FirstName_Male();
    }

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
}
