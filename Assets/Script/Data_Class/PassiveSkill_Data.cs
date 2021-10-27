using UnityEngine;
[CreateAssetMenu(fileName = "PassiveSkill Data", menuName = "Scriptable Object/PassiveSkill Data", order = int.MaxValue)]
public class PassiveSkill_Data : ScriptableObject
{
    [SerializeField]
    public PassiveSkill passive_skill;
}
