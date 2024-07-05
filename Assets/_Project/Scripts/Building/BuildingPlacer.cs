using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingDatabaseSO _buildingDatabase;
    private Building _buildingToPlace = null;
    private BuildingData _buildingData = null;

    private void Update()
    {
        if (_buildingToPlace != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CancelBuildingPlacement();
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000f, Globals.TERRAIN_LAYER_MASK))
                {
                    _buildingToPlace.SetPosition(hit.point);
                }

                if (Input.GetMouseButtonDown(0) && _buildingToPlace.HasValidPlacement && !EventSystem.current.IsPointerOverGameObject() && _buildingData.CanAffordBuilding())
                {
                    PlaceBuilding();
                }
            }
        }
    }
    
    public void SelectBuildingToPlace(int index)
    {
        PrepareBuildingToPlace(index);
    }

    public void PrepareBuildingToPlace(int index)
    {
        if (_buildingToPlace != null && !_buildingToPlace.IsPlaced)
        {
            Destroy(_buildingToPlace.Transform.gameObject);
        }
        _buildingData = Globals.BUILDING_DATA.buildingDataList[index];
        
        Building building = new Building(_buildingData);
        building.Transform.GetComponent<BuildingManager>().Initialize(building);
        _buildingToPlace = building;
    }
    
    public void CancelBuildingPlacement()
    {
        Destroy(_buildingToPlace.Transform.gameObject);
        _buildingToPlace = null;
    }

    public void PlaceBuilding()
    {
        _buildingToPlace.Place();
        _buildingToPlace = null;

        EventManager.TriggerEvent("UpdateResourceUI");
    }
}
