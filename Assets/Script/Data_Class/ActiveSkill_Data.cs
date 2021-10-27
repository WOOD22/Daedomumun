using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkill Data", menuName = "Scriptable Object/ActiveSkill Data", order = int.MaxValue)]
public class ActiveSkill_Data : ScriptableObject
{
    [SerializeField]
    public ActiveSkill active_skill;
}
