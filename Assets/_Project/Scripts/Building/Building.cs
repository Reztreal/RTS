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

public class Building : Unit
{
    private BuildingState _state;
    private BuildingData _buildingData;

    private List<Material> _materials = new List<Material>();
    
    public Building(BuildingData buildingData) : base(buildingData)
    {
        _buildingData = buildingData;
        
        foreach (Material m in _transform.GetComponentInChildren<Renderer>().materials)
        {
            _materials.Add(m);
        }

        if (CanAffordUnit())
        {
            SetMaterials(BuildingState.VALID);
        }
        else
        {
            SetMaterials(BuildingState.INVALID);
        }
    }

    public void Place()
    {
        _state = BuildingState.PLACED;
        _transform.GetComponent<BoxCollider>().isTrigger = false;
        SetMaterials();
        
        foreach (ResourceValue resourceValue in _buildingData.unitCost)
        {
            Globals.RESOURCE_DATA.GetResource(resourceValue.resourceType.resourceName).amount -= resourceValue.amount;
        }
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
    public bool HasValidPlacement { get { return _state == BuildingState.VALID; } }
    public BuildingState State { get { return _state; } }
    public void SetState(BuildingState state) { _state = state; }
}
