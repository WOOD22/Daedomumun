using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //선언=========================================================================================
    //red  : 홍코너
    //blue : 청코너
    public BattleStat red;
    public BattleStat blue;
    //전투 시작시 체력 초기화======================================================================
    void Start()
    {
        red.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        red.now_HP = red.max_HP;
        red.max_SP = int.Parse(red.student.active_skill.need);
        red.now_SP = 0;
        red.cool_ATK = 2.5f - red.student.st_DEX / 100;
        red.can_ATK = true;

        blue.max_HP = (int)Mathf.Floor(red.student.st_CON) * 100;
        blue.now_HP = red.max_HP;
        blue.max_SP = int.Parse(blue.student.active_skill.need);
        blue.now_SP = 0;
        blue.cool_ATK = 2.5f - blue.student.st_DEX / 100;
        blue.can_ATK = true;
    }

    void Update()
    {
        if(red.can_ATK == true)
        {
            red.can_ATK = false;
            StartCoroutine(ATK_Cooltime(red));
            Target_ATK(red, blue);
        }

        if (blue.can_ATK == true)
        {
            blue.can_ATK = false;
            StartCoroutine(ATK_Cooltime(blue));
            Target_ATK(blue, red);
        }
    }

    void Target_ATK(BattleStat user, BattleStat target)
    {
        int target_DMG = (-1) * (int)(user.student.st_STR * (1 + Random.Range(0, user.student.st_LUK) / 200));
        target.now_HP += target_DMG;
        Debug.Log(target.student.name + target_DMG);
        user.can_ATK = false;
    }

    IEnumerator ATK_Cooltime(BattleStat user)
    {
        yield return new WaitForSeconds(user.cool_ATK);
        user.can_ATK = true;
    }

    void Charge_SP(float time, BattleStat user)
    {
        user.now_SP += (int)(red.student.st_INT / 10);
    }
    //액티브 스킬 발동(복수의 effect의 경우 &를 기준으로 나누어서 적용 ## & 는 문자열에서 제외한다)
    public void Use_Active_Skill(string skill_effect, BattleStat user, BattleStat target)
    {
        string active_effect;

        int effect_start = skill_effect.IndexOf("(");

        active_effect = skill_effect.Substring(0, effect_start);
        Debug.Log(active_effect);

        switch (active_effect)
        {
            //DMG 효과일 경우======================================================================
            case "DMG":
                //effect_stat    : 계수가 적용되는 스탯 뒤의 쉼표(,)
                //effect_facter  : 적용되는 계수 뒤의 쉼표(,), 또는 괄호())
                //effect_hit_num : 타격 횟수 뒤의 쉼표(,),또는 괄호())
                int effect_stat, effect_facter, effect_hit_num;

                effect_stat = skill_effect.IndexOf(",", effect_start);

                if (skill_effect.IndexOf(",", effect_stat + 1) == -1)
                {
                    effect_facter = skill_effect.IndexOf(")", effect_stat);
                }
                else
                {
                    effect_facter = skill_effect.IndexOf(",", effect_stat + 1);
                }
                //
                if (skill_effect.IndexOf(",", effect_facter + 1) == -1)
                {
                    effect_hit_num = skill_effect.IndexOf(")", effect_facter);
                }
                else
                {
                    effect_hit_num = skill_effect.IndexOf(",", effect_facter + 1);
                }

                //stat    : 계수가 적용되는 스탯
                //facter  : 적용되는 계수
                //hit_num : 타격 횟수
                string stat = skill_effect.Substring(effect_start + 1, effect_stat - 1 - effect_start);
                string facter = skill_effect.Substring(effect_stat + 1, effect_facter - 1 - effect_stat);
                string hit_num = skill_effect.Substring(effect_facter + 1, effect_hit_num - 1 - effect_facter);

                Active_DMG(stat, facter, hit_num, user, target);
                break;
        }
    }
    //액티브 스킬 DMG 효과=========================================================================
    void Active_DMG(string stat, string facter, string hit_num, BattleStat user, BattleStat target)
    {
        switch (stat)
        {
            case "STR":
                for (int i = 0; i < int.Parse(hit_num); i++)
                {
                    target.now_HP += (int)((-1) * Mathf.Floor(user.student.st_STR) * int.Parse(facter));
                    Debug.Log((int)((-1) * Mathf.Floor(user.student.st_DEX) * int.Parse(facter)));
                }
                break;
            case "DEX":
                for (int i = 0; i < int.Parse(hit_num); i++)
                {
                    target.now_HP += (int)((-1) * Mathf.Floor(user.student.st_DEX) * int.Parse(facter));
                    Debug.Log((int)((-1) * Mathf.Floor(user.student.st_DEX) * int.Parse(facter)));
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
    public bool can_ATK;
    public float cool_ATK;
}
