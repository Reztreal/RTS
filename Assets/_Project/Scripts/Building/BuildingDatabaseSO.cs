using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingDatabase", menuName = "Building Database")]
public class BuildingDatabaseSO : ScriptableObject
{
    public List<BuildingData> buildingDataList;
}
