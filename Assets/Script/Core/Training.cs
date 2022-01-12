using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Training : MonoBehaviour
{
    public GameObject prefab_portrait_card;
    public GameObject content;
    public School player_school = new School();
    public List<Student> player_student_list = new List<Student>();

    public void Init_Training_Unit()
    {
        player_school = GameObject.Find("GameData").GetComponent<Game_Data>().game_data.schools[1];
        //문자순으로 정렬==========================================================================
        player_student_list = player_school.students;
        var temp = player_student_list.OrderBy(x => x.name);
        player_student_list = temp.ToList<Student>();
        //===========
        for (int i = 0; i < player_school.students.Count; i++)
        {
            GameObject instance;
            instance = Instantiate(prefab_portrait_card, content.transform);
            instance.GetComponent<Prefab_Portrait_Card_Property>().student = player_student_list[i];
        }
    }
}
