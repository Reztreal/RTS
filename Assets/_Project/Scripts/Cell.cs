using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPosition;
    public Vector2Int gridPosition;
    
    public GridDirection bestDirection;
    
    public float cost;
    public float bestCost;
    public float height => worldPosition.y;
    
    public Cell(Vector3 worldPosition, Vector2Int gridPosition)
    {
        this.worldPosition = worldPosition;
        this.gridPosition = gridPosition;

        cost = 1;
        bestCost = float.MaxValue;
        bestDirection = GridDirection.None;
    }
    
    public void IncreaseCost(float amount)
    {
        if (cost == float.MaxValue)
        {
            return;
        }
        
        if (cost + amount >= float.MaxValue)
        {
            cost = float.MaxValue;
        }
        else
        {
            cost += amount;
        }
    }
}
