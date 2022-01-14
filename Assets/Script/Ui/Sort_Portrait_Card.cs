using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sort_Portrait_Card : MonoBehaviour
{
    public List<Student> student_list;
    //이름 정렬====================================================================================
    public void Sort_Name(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.name);
        student_list = temp.ToList<Student>();
        if (is_reverse == true)
        {
            student_list.Reverse();
        }
    }
    //근력 정렬====================================================================================
    public void Sort_STR(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_STR);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
    //민첩 정렬====================================================================================
    public void Sort_DEX(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_DEX);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
    //체질 정렬====================================================================================
    public void Sort_CON(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_CON);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
    //지능 정렬====================================================================================
    public void Sort_INT(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_INT);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
    //지혜 정렬====================================================================================
    public void Sort_WIS(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_WIS);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
    //의지 정렬====================================================================================
    public void Sort_WIL(bool is_reverse)
    {
        var temp = student_list.OrderBy(x => x.stat.st_WIL);
        student_list = temp.ToList<Student>();
        if (is_reverse == false)//내림차순을 기준으로 두었음
        {
            student_list.Reverse();
        }
    }
}
