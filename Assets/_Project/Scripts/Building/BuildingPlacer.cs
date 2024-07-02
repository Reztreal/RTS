using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingDatabaseSO _buildingDatabase;
    private Building _buildingToPlace = null;
    private BuildingData _buildingData = null;

    private void Start()
    {
        _buildingData = _buildingDatabase.buildingDataList[0];
        PrepareBuildingToPlace();
    }

    private void Update()
    {
        if (_buildingToPlace != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _buildingToPlace = null;
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000f, Globals.TERRAIN_LAYER_MASK))
                {
                    _buildingToPlace.SetPosition(hit.point);
                }
            }
        }
    }

    public void PrepareBuildingToPlace()
    {
        Building building = new Building(_buildingData);
        building.Transform.GetComponent<BuildingCollision>().Initialize(building);
        _buildingToPlace = building;
    }
}
