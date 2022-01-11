using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prefab_Portrait_Card_Property : MonoBehaviour
{
    //Prefab_Portrait_Card의 Text 및 내부 속성=====================================================
    public Text name;
    public Text st_STR, st_DEX, st_CON;
    public Text st_INT, st_WIS, st_WIL;

    public Student student = new Student();
    //Text변경 필요할 때만 업데이트================================================================
    void Start()
    {
        Debug.Log("Check");
        if (student.name != name.text)
        {
            name.text = student.name;
        }
        if (student.stat.st_STR != int.Parse(st_STR.text))
        {
            st_STR.text = student.stat.st_STR.ToString();
        }
        if (student.stat.st_DEX != int.Parse(st_DEX.text))
        {
            st_DEX.text = student.stat.st_DEX.ToString();
        }
        if (student.stat.st_CON != int.Parse(st_CON.text))
        {
            st_CON.text = student.stat.st_CON.ToString();
        }
        if (student.stat.st_INT != int.Parse(st_INT.text))
        {
            st_INT.text = student.stat.st_INT.ToString();
        }
        if (student.stat.st_WIS != int.Parse(st_WIS.text))
        {
            st_WIS.text = student.stat.st_WIS.ToString();
        }
        if (student.stat.st_WIL != int.Parse(st_WIL.text))
        {
            st_WIL.text = student.stat.st_WIL.ToString();
        }
    }
}
