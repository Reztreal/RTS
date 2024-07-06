using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataLoader
{
    public static void LoadBuildingData()
    {
        Globals.SPAWN_POINTS = Resources.LoadAll<Transform>("Prefabs/SpawnPoints").ToList();
        Globals.BUILDING_DATA = Resources.Load<BuildingDatabaseSO>("ScriptableObjects/Buildings/BuildingDatabase");
        Globals.RESOURCE_DATA = Resources.Load<GameResourceDatabaseSO>("ScriptableObjects/GameResources/GameResourceDatabase");
    }
}
