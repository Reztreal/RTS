using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataLoader
{
    public static void LoadBuildingData()
    {
        Globals.BUILDING_DATA = Resources.Load<BuildingDatabaseSO>("ScriptableObjects/Buildings/BuildingDatabase");
        Globals.RESOURCE_DATA = Resources.Load<GameResourceDatabaseSO>("ScriptableObjects/GameResources/GameResourceDatabase");
    }
}
