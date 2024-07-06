using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDatabase", menuName = "ScriptableObjects/Building/BuildingDatabase")]
public class BuildingDatabaseSO : ScriptableObject
{
    public List<BuildingData> buildingDataList;
    
    public BuildingData GetBuildingData(int index)
    {
        return buildingDataList[index];
    }
    
    public BuildingData GetBuildingData(string buildingName)
    {
        foreach (BuildingData buildingData in buildingDataList)
        {
            if (buildingData.unitName == buildingName)
            {
                return buildingData;
            }
        }

        return null;
    }
}
