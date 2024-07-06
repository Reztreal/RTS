using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DataLoader.LoadBuildingData();
        ResetResources();
        // SpawnTownCenters();
    }
    
    public void SpawnTownCenters()
    {
        foreach (Transform spawnPoint in Globals.SPAWN_POINTS)
        {
            BuildingData buildingData = Globals.BUILDING_DATA.GetBuildingData("TownCenter");
            Building building = new Building(buildingData);
            building.Transform.GetComponent<BuildingManager>().Initialize(building);
            building.SetPosition(spawnPoint.position);
            building.Place();
        }
    }

    public void ResetResources()
    {
        Globals.RESOURCE_DATA.resourceTypeList[0].amount = 800;
        Globals.RESOURCE_DATA.resourceTypeList[1].amount = 1300;
        Globals.RESOURCE_DATA.resourceTypeList[2].amount = 800;

    }
}
