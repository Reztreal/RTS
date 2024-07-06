using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    INSTANTIATE_CHARACTER
}


[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public string code;
    
    public string skillDescription;
    public Sprite skillIcon;
    public float skillCooldown;
    public float skillDuration;
    
    public UnitData unitData;
    public SkillType skillType;
    
    public void Trigger(GameObject source, GameObject target = null)
    {
        switch (skillType)
        {
            case SkillType.INSTANTIATE_CHARACTER:
                InstantiateCharacter(source);
                break;
            default:
                break;
        }
    }

    public void InstantiateCharacter(GameObject source)
    {
        BoxCollider boxCollider = source.GetComponent<BoxCollider>();
        Vector3 position = new Vector3(
            source.transform.position.x + boxCollider.size.x * 0.7f,
            source.transform.position.y,
            source.transform.position.z + boxCollider.size.z * 0.7f
        );
        
        CharacterData characterData = unitData as CharacterData;
        Character character = new Character(characterData);
        character.SetPosition(position);
        character.Transform.GetComponent<CharacterManager>().Initialize(character);
    }
}
