using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Check : MonoBehaviour
{
    public ActiveSkill_Data active_skill;

    public Student red;
    public Student blue;


    public void check(ActiveSkill_Data _active_skill, Student target)
    {
        string active_effect;

        active_effect = _active_skill.active_skill.effect;

        switch(active_effect)
        {
            case "DMG":
                break;
        }
    }
}
