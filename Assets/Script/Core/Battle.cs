using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //선언=========================================================================================
    public BattleStat red;
    public BattleStat blue;
    //전투 시작시 체력 초기화======================================================================
    void Start()
    {
        red.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        red.now_HP = red.max_HP;

        blue.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        blue.now_HP = red.max_HP;
    }
    //
    public void check(ActiveSkill _active_skill, BattleStat me, BattleStat target)
    {
        string active_effect;

        int i = _active_skill.effect.IndexOf("(");

        active_effect = _active_skill.effect.Substring(0, i);
        Debug.Log(active_effect);

        switch (active_effect)
        {
            case "DMG":
                int j = _active_skill.effect.IndexOf(",", i);
                int a;
                if (_active_skill.effect.IndexOf(",", j+1) == -1)
                {
                    a = _active_skill.effect.IndexOf(")", j);
                }
                else
                {
                    a = _active_skill.effect.IndexOf(",", j+1);
                }
                string stat = _active_skill.effect.Substring(i + 1, j - 1 - i);
                string facter = _active_skill.effect.Substring(j + 1, a - 1 - j);
                Debug.Log(stat);
                Debug.Log(facter);
                break;
        }
    }
    //액티브 스킬 DMG 효과=========================================================================
    void Active_DMG(string stat, string facter, string hit_num, BattleStat me, BattleStat target)
    {
        switch (stat)
        {
            case "STR":
                for (int i = 0; i < int.Parse(hit_num); i++)
                {
                    target.now_HP += (int)((-1) * Mathf.Floor(me.student.st_STR) * int.Parse(facter));
                }
                break;
            case "DEX":
                for (int i = 0; i < int.Parse(hit_num); i++)
                {
                    target.now_HP += (int)((-1) * Mathf.Floor(me.student.st_DEX) * int.Parse(facter));
                }
                break;
        }
    }
}
//전투시 필요 스탯 클래스화========================================================================
[System.Serializable]
public class BattleStat
{
    public int max_HP;
    public int now_HP;
    public int max_SP;
    public int now_SP;
    public Student student;
}
