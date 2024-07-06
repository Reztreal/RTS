using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    protected UnitData _unitData;
    protected Transform _transform;

    private string _uid;
    
    protected int _currentHealth;

    private List<Skill> _skills;
    
    public Unit(UnitData unitData)
    {
        _unitData = unitData;
        
        _uid = System.Guid.NewGuid().ToString();
        
        _currentHealth = _unitData.maxHealth;
        GameObject g = GameObject.Instantiate(_unitData.unitPrefab) as GameObject;
        _transform = g.transform;
        
        _skills = new List<Skill>();
        Skill skill;
        foreach (SkillData skillData in _unitData.skills)
        {
            skill = g.AddComponent<Skill>();
            skill.Initialize(skillData, g);
            _skills.Add(skill);
        }
    }
    
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }
    
    public void TriggerSkill(int index, GameObject target = null)
    {
        _skills[index].Trigger(target);
    }
    
    public List<Skill> Skills { get => _skills; }
    public Transform Transform { get { return _transform; } }
    public bool CanAffordUnit() { return _unitData.CanAffordUnit(); }
    public string UID { get { return _uid; } }
    public UnitData UnitData { get { return _unitData; } }
}
