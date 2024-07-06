using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    protected UnitData _unitData;
    protected Transform _transform;

    private string _uid;
    
    protected int _currentHealth;
    
    public Unit(UnitData unitData)
    {
        _unitData = unitData;
        
        _uid = System.Guid.NewGuid().ToString();
        
        _currentHealth = _unitData.maxHealth;
        GameObject g = GameObject.Instantiate(_unitData.unitPrefab) as GameObject;
        _transform = g.transform;
    }
    
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }
    
    public Transform Transform { get { return _transform; } }
    public bool CanAffordUnit() { return _unitData.CanAffordUnit(); }
    public string UID { get { return _uid; } }
}
