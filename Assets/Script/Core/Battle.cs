using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public ActiveSkill_Data active_skill;

    public BattleStat red;
    public BattleStat blue;

    void Start()
    {
        red.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        red.now_HP = red.max_HP;

        blue.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        blue.now_HP = red.max_HP;
    }

    public void check(ActiveSkill_Data _active_skill, BattleStat me ,BattleStat taget)
    {
        string active_effect;

        int i = _active_skill.active_skill.effect.IndexOf("(");

        active_effect = _active_skill.active_skill.effect.Substring(0, i);
        Debug.Log(active_effect);

        switch (active_effect)
        {
            case "DMG":
                int j = _active_skill.active_skill.effect.IndexOf(",", i);
                int a;
                if (_active_skill.active_skill.effect.IndexOf(",", j+1) == -1)
                {
                    a = _active_skill.active_skill.effect.IndexOf(")", j);
                }
                else
                {
                    a = _active_skill.active_skill.effect.IndexOf(",", j+1);
                }
                string stat = _active_skill.active_skill.effect.Substring(i + 1, j - 1 - i);
                string facter = _active_skill.active_skill.effect.Substring(j + 1, a - 1 - j);
                Debug.Log(stat);
                Debug.Log(facter);
                break;
        }
    }

    void DMG(string stat, string facter, string hit_num, int target_HP)
    {
        switch (stat)
        {
            case "STR":
                target_HP += (-1);
                break;
        }
    }
}

[System.Serializable]
public class BattleStat
{
    public int max_HP;
    public int now_HP;
    public int max_SP;
    public int now_SP;
    public Student student;
}
