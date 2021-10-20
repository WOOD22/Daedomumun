using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
public class Skill_Data : ScriptableObject
{
    [SerializeField]
    public Skill skill;
}
