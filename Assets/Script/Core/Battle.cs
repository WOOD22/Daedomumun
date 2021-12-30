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
    //초기화 용도==================================================================================
    void Initialization(BattleStat user)
    {
        user.max_HP = (int)Mathf.Floor(user.student.stat.st_CON) * 10;
        user.now_HP = user.max_HP;
        user.max_SP = int.Parse(user.student.active_skill.need);
        user.now_SP = 0;
        user.ATK_cool = 2.5f - user.student.stat.st_DEX / 100;
        user.ATK_can = true;
        user.SP_cool = 2.5f - user.student.stat.st_INT / 100;
        user.SP_can = true;
    }
    //전투 시작시 초기화===========================================================================
    void OnEnable()
    {
        Initialization(red);
        Initialization(blue);
    }
    //전투 결과====================================================================================
    void Check_Result()
    {
        if(red.now_HP <= 0)
        {
            Debug.Log(blue.student.name + "승리");
            this.GetComponent<Battle>().enabled = false;
        }
        if (blue.now_HP <= 0)
        {
            Debug.Log(red.student.name + "승리");
            this.GetComponent<Battle>().enabled = false;
        }
    }
    //일반 공격====================================================================================
    void ATK_Can(BattleStat user, BattleStat target)
    {
        if (user.ATK_can == true)
        {
            red.ATK_can = false;
            StartCoroutine(ATK_Cooltime(user));
            ATK_Target(user, target);
            Check_Result();
        }
    }
    //액티브 스킬==================================================================================
    void SP_Can(BattleStat user, BattleStat target)
    {
        if (user.SP_can == true)
        {
            red.SP_can = false;
            StartCoroutine(SP_Cooltime(user));
            Charge_SP(user);
            if (user.now_SP >= user.max_SP)
            {
                Use_Active_Skill(user.student.active_skill.effect, user, target);
                Check_Result();
            }
        }
    }

    void Update()
    {
        ATK_Can(red, blue);
        ATK_Can(blue, red);

        SP_Can(red, blue);
        SP_Can(blue, red);
    }
    //일반 공격(데미지는 STR에 비례, LUK으로 추가 데미지 배율 랜덤 적용)===========================
    void ATK_Target(BattleStat user, BattleStat target)
    {
        int target_DMG = (-1) * (int)(user.student.stat.st_STR * (1 + Random.Range(0, user.student.stat.st_LUK) / 200));
        target.now_HP += target_DMG;
        Debug.Log(target.student.name + target.now_HP);
        user.ATK_can = false;
    }
    //일반 공격 속도(0 = 2.5 초당 1회, 100 = 1.5 초당 1회, 200 = 0.5 초당 1회)=================================
    IEnumerator ATK_Cooltime(BattleStat user)
    {
        yield return new WaitForSeconds(user.ATK_cool);
        user.ATK_can = true;
    }
    //SP 충전======================================================================================
    void Charge_SP(BattleStat user)
    {
        user.now_SP += (int)(red.student.stat.st_WIS / 10);
        user.SP_can = false;
    }
    //SP 충전 속도(0 = 초당)
    IEnumerator SP_Cooltime(BattleStat user)
    {
        yield return new WaitForSeconds(user.SP_cool);
        user.SP_can = true;
    }
    //액티브 스킬 발동(복수의 effect의 경우 &를 기준으로 나누어서 적용 ## & 는 문자열에서 제외한다)
    public void Use_Active_Skill(string skill_effect, BattleStat user, BattleStat target)
    {
        string active_effect;

        int effect_start = skill_effect.IndexOf("(");

        active_effect = skill_effect.Substring(0, effect_start);

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

                Active_DMG(stat, facter, hit_num, user, target);//DMG효과 발동
                user.now_SP = 0;
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
                    DMG(user.student.stat.st_STR);
                }
                break;
            case "DEX":
                for (int i = 0; i < int.Parse(hit_num); i++)
                {
                    DMG(user.student.stat.st_DEX);
                }
                break;
        }
        //DMG 효과 함수화
        void DMG(float facter_stat)
        {
            int dmg = (int)(Mathf.Floor(facter_stat) * float.Parse(facter) * (1 + Random.Range(0, user.student.stat.st_LUK) / 200));
            target.now_HP -= dmg;
            Debug.Log(dmg + target.student.name + "의 남은 HP" + target.now_HP);
        }
    }
}
//전투시 필요 스탯 클래스화========================================================================
[System.Serializable]
public class BattleStat
{
    public int max_HP;      //최대 체력
    public int now_HP;      //현재 체력
    public int max_SP;      //최대 SP
    public int now_SP;      //현재 SP
    public bool ATK_can;    //공격 가능?
    public float ATK_cool;  //공격 속도
    public bool SP_can;     //SP 충전 가능?
    public float SP_cool;   //SP 충전 속도
    public Student student; //파이터 데이터
}
