using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prefab_Schedule_Slot_Property : MonoBehaviour
{
    //Prefab_Training_Slot�� Text �� ���� �Ӽ�=====================================================
    public new Text name;
    public GameObject portrait_card_slot;

    public Schedule schedule = new Schedule();
    public Student student = new Student();

    bool ticket_check;

    Schedule_Data schedule_data;
    Sort_Portrait_Card sort_portrait_card;

    void Start()
    {
        sort_portrait_card = GameObject.Find("GameManager").GetComponent<Sort_Portrait_Card>();
        schedule_data = GameObject.Find("GameData").GetComponent<Schedule_Data>();
    }
    //Text���� �ʿ��� ���� ������Ʈ================================================================
    void Update()
    {
        if (schedule.name != name.text)
        {
            name.text = schedule.name;
        }

        //���� ������ ä������ �����==============================================================
        if (portrait_card_slot.transform.childCount != 0)
        {
            student.schedule = "NONE";                      //��ü �� �ʱ�ȭ
            student = portrait_card_slot.transform.GetChild(0).GetComponent<Prefab_Portrait_Card_Property>().student;
            student.schedule = schedule.code;

        }
        //���� ������ ������� �����==============================================================
        else
        {

        }
        //���� ������ ���ġ�� �ʱ�ȭ==============================================================
        if (portrait_card_slot.transform.childCount == 0 && student != new Student())
        {
            student.schedule = "NONE";
            student = new Student();
        }
    }
}
