using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        EventManager.TriggerEvent("UpdateResourceUI");
    }

    public void InstantiateCharacter(GameObject source)
    {
        foreach (ResourceValue resourceValue in unitData.unitCost)
        {
            Globals.RESOURCE_DATA.GetResource(resourceValue.resourceType.resourceName).amount -= resourceValue.amount;
        }
        
        BoxCollider boxCollider = source.GetComponent<BoxCollider>();
        Vector3 position = new Vector3(
            source.transform.position.x + boxCollider.size.x * 0.7f,
            source.transform.position.y,
            source.transform.position.z + boxCollider.size.z * 0.7f
        );

        position = FindValidPosition(position, boxCollider.size);
        
        CharacterData characterData = unitData as CharacterData;
        Character character = new Character(characterData);
        
        character.Transform.GetComponent<NavMeshAgent>().Warp(position);
        character.Transform.GetComponent<CharacterManager>().Initialize(character);
    }

    public Vector3 FindValidPosition(Vector3 startPos, Vector3 size, float checkRadius = 1.0f, int maxChecks = 10)
    {
        int checks = 0;
        Vector3 position = startPos;

        while (checks < maxChecks)
        {
            if (!(Physics.OverlapSphere(position, checkRadius, Globals.UNIT_LAYER_MASK).Length > 0))
            {
                return position;
            }

            checks++;
            position += new Vector3(-checkRadius, 0, 0);
        }

        return startPos;
    }
}
