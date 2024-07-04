using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public GameObject buildingPrefab;
    public List<ResourceValue> buildingCost;
    
    public bool CanAffordBuilding()
    {
        foreach (ResourceValue resourceValue in buildingCost)
        {
            if (Globals.RESOURCE_DATA.GetResource(resourceValue.resourceType.resourceName).amount < resourceValue.amount)
            {
                return false;
            }
        }

        return true;
    }
}       
