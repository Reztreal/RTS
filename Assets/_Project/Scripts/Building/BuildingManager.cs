using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : UnitManager
{
    private Building _building;
    private BoxCollider _boxCollider;
    
    private int _collisionCount = 0;

    public void Initialize(Building building)
    {
        _boxCollider = GetComponent<BoxCollider>();
        _building = building;
    }
    
    public override bool IsAlive()
    {
        return _building.State == BuildingState.PLACED;
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
        
        bool isColliding = !HasValidPlacement();
        
        if (isColliding)
        {
            _building.SetMaterials(BuildingState.INVALID);
            _building.SetState(BuildingState.INVALID);
        }
        else if (_building.CanAffordUnit())
        {
            _building.SetMaterials(BuildingState.VALID);
            _building.SetState(BuildingState.VALID);
        }

        return isColliding;
    }

    public bool HasValidPlacement()
    {
        if (_collisionCount > 0) return false;

        // get 4 bottom corner positions
        Vector3 p = transform.position;
        Vector3 c = _boxCollider.center;
        Vector3 e = _boxCollider.size / 2f;
        float bottomHeight = c.y - e.y + 0.5f;
        Vector3[] bottomCorners = new Vector3[]
        {
            new Vector3(c.x - e.x, bottomHeight, c.z - e.z),
            new Vector3(c.x - e.x, bottomHeight, c.z + e.z),
            new Vector3(c.x + e.x, bottomHeight, c.z - e.z),
            new Vector3(c.x + e.x, bottomHeight, c.z + e.z)
        };
        // cast a small ray beneath the corner to check for a close ground
        // (if at least two are not valid, then placement is invalid)
        int invalidCornersCount = 0;
        foreach (Vector3 corner in bottomCorners)
        {
            if (!Physics.Raycast(
                    p + corner,
                    Vector3.up * -1f,
                    2f,
                    Globals.TERRAIN_LAYER_MASK
                ))
                invalidCornersCount++;
        }
        return invalidCornersCount < 3;
    }
}
