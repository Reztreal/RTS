using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum BuildingState
{
    VALID,
    INVALID,
    PLACED
}

public class Building
{
    private BuildingData _buildingData;
    private Transform _transform;

    private BuildingState _state;

    private List<Material> _materials = new List<Material>();
    
    public Building(BuildingData buildingData)
    {
        _buildingData = buildingData;
        GameObject g = GameObject.Instantiate(_buildingData.buildingPrefab) as GameObject;
        _transform = g.transform;
        
        _state = BuildingState.VALID;
        foreach (Material m in g.GetComponentInChildren<Renderer>().materials)
        {
            _materials.Add(m);
        }
        SetMaterials();
    }
    
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }
    
    public void SetMaterials() {SetMaterials(_state);}

    public void SetMaterials(BuildingState state)
    {
        List<Material> materials;
        if (state == BuildingState.VALID)
        {
            Material m = Resources.Load("Materials/VALID") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(m);
            }
        }
        else if (state == BuildingState.INVALID)
        {
            Material m = Resources.Load("Materials/INVALID") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(m);
            }
        }
        else if (state == BuildingState.PLACED)
        {
            materials = _materials;
        }
        else
        {
            return;
        }
        _transform.GetComponentInChildren<Renderer>().materials = materials.ToArray();
    }
    
    public bool IsPlaced { get { return _state == BuildingState.PLACED; } }
    public Transform Transform { get { return _transform; } }
    
}
