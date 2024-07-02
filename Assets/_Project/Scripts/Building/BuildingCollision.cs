using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    private Building _building;
    private BoxCollider _boxCollider;
    
    private int _collisionCount = 0;

    private void Update()
    {
        Debug.Log(_collisionCount);
    }

    public void Initialize(Building building)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _building = building;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain")) {return;}
        _collisionCount++;
        CheckPlacement();
    }
    
    private void OnTriggerExit(Collider other)
    {   
        if (other.CompareTag("Terrain")) {return;}
        _collisionCount--;
        CheckPlacement();
    }

    private bool CheckPlacement()
    {
        if (_building == null) return false;
        if (_building.IsPlaced) return false;
        
        bool isColliding = IsColliding;
        
        if (isColliding)
        {
            _building.SetMaterials(BuildingState.INVALID);
        }
        else
        {
            _building.SetMaterials(BuildingState.VALID);
        }

        return isColliding;
    }

    public bool IsColliding
    {
        get { return _collisionCount > 0; }
    }
}
