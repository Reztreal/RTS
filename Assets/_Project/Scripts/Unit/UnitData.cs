using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : ScriptableObject
{
    public string unitName;
    public GameObject unitPrefab;
    public int maxHealth;
    
    public List<ResourceValue> unitCost;
    
    public bool CanAffordUnit()
    {
        foreach (ResourceValue resourceValue in unitCost)
        {
            if (Globals.RESOURCE_DATA.GetResource(resourceValue.resourceType.resourceName).amount < resourceValue.amount)
            {
                return false;
            }
        }

        return true;
    }
}
