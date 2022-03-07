using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Prefab_Portrait_Card_Property : MonoBehaviour
{
    //Prefab_Portrait_Card의 Text==================================================================
    public new Text name;
    public Text st_STR, st_DEX, st_CON;
    public Text st_INT, st_WIS, st_WIL;

    public Student student = new Student();

    //Text변경 필요할 때만 업데이트================================================================
    void Update()
    {
        if (student.name != name.text)
        {
            name.text = student.name;
        }
        if (student.stat.st_STR != float.Parse(st_STR.text))
        {
            st_STR.text = Mathf.Floor(student.stat.st_STR).ToString();
        }
        if (student.stat.st_DEX != float.Parse(st_DEX.text))
        {
            st_DEX.text = Mathf.Floor(student.stat.st_DEX).ToString();
        }
        if (student.stat.st_CON != float.Parse(st_CON.text))
        {
            st_CON.text = Mathf.Floor(student.stat.st_CON).ToString();
        }
        if (student.stat.st_INT != float.Parse(st_INT.text))
        {
            st_INT.text = Mathf.Floor(student.stat.st_INT).ToString();
        }
        if (student.stat.st_WIS != float.Parse(st_WIS.text))
        {
            st_WIS.text = Mathf.Floor(student.stat.st_WIS).ToString();
        }
        if (student.stat.st_WIL != float.Parse(st_WIL.text))
        {
            st_WIL.text = Mathf.Floor(student.stat.st_WIL).ToString();
        }
    }
}
