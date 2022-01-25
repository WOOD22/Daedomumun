using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prefab_Training_Slot_Property : MonoBehaviour
{
    //Prefab_Training_Slot의 Text 및 내부 속성=====================================================
    public new Text name;
    public Text change_STR, change_DEX, change_CON;
    public Text change_INT, change_WIS, change_WIL;
    public GameObject portrait_card_slot;

    public Training_Infra training_infra = new Training_Infra();
    public Student student = new Student();

    int t = 0;

    List<Dictionary<string, object>> training_infra_table;
    Sort_Portrait_Card sort_portrait_card;

    void Start()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        training_infra_table = CSVReader.Read("DataBase/CSV/Training_Infra_Table");
    }
    //Text변경 필요할 때만 업데이트================================================================
    void Update()
    {
        if(training_infra.name != name.text)
        {
            name.text = training_infra.name;
        }
        if (training_infra.change_STR != float.Parse(change_STR.text))
        {
            change_STR.text = Mathf.Round(training_infra.change_STR).ToString();
        }
        if (training_infra.change_DEX != float.Parse(change_DEX.text))
        {
            change_DEX.text = Mathf.Round(training_infra.change_DEX).ToString();
        }
        if (training_infra.change_CON != float.Parse(change_CON.text))
        {
            change_CON.text = Mathf.Round(training_infra.change_CON).ToString();
        }
        if (training_infra.change_INT != float.Parse(change_INT.text))
        {
            change_INT.text = Mathf.Round(training_infra.change_INT).ToString();
        }
        if (training_infra.change_WIS != float.Parse(change_WIS.text))
        {
            change_WIS.text = Mathf.Round(training_infra.change_WIS).ToString();
        }
        if (training_infra.change_WIL != float.Parse(change_WIL.text))
        {
            change_WIL.text = Mathf.Round(training_infra.change_WIL).ToString();
        }
        //트레이닝 슬롯이 채워지면 적용됨==========================================================
        if(portrait_card_slot.transform.childCount != 0)
        {
            student.training = "NONE";                      //교체 시 초기화
            student = portrait_card_slot.transform.GetChild(0).GetComponent<Prefab_Portrait_Card_Property>().student;
            student.training = training_infra.name;
            training_infra.change_STR = student.stat.pt_STR / 100 * float.Parse(training_infra_table[t]["change_STR"].ToString());
            training_infra.change_DEX = student.stat.pt_DEX / 100 * float.Parse(training_infra_table[t]["change_DEX"].ToString());
            training_infra.change_CON = student.stat.pt_CON / 100 * float.Parse(training_infra_table[t]["change_CON"].ToString());
            training_infra.change_INT = student.stat.pt_INT / 100 * float.Parse(training_infra_table[t]["change_INT"].ToString());
            training_infra.change_WIS = student.stat.pt_WIS / 100 * float.Parse(training_infra_table[t]["change_WIS"].ToString());
            training_infra.change_WIL = student.stat.pt_WIL / 100 * float.Parse(training_infra_table[t]["change_WIL"].ToString());

        }
        //트레이닝 슬롯이 비워지면 적용됨==========================================================
        else
        {
            training_infra.name = training_infra_table[t]["name"].ToString();
            training_infra.cost = int.Parse(training_infra_table[t]["cost"].ToString());
            training_infra.change_STR = float.Parse(training_infra_table[t]["change_STR"].ToString());
            training_infra.change_DEX = float.Parse(training_infra_table[t]["change_DEX"].ToString());
            training_infra.change_CON = float.Parse(training_infra_table[t]["change_CON"].ToString());
            training_infra.change_INT = float.Parse(training_infra_table[t]["change_INT"].ToString());
            training_infra.change_WIS = float.Parse(training_infra_table[t]["change_WIS"].ToString());
            training_infra.change_WIL = float.Parse(training_infra_table[t]["change_WIL"].ToString());
        }
        //트레이닝 슬롯이 비워치면 초기화==========================================================
        if (portrait_card_slot.transform.childCount == 0 && student != new Student())
        {
            student.training = "NONE";
            student = new Student();
        }
    }
}
