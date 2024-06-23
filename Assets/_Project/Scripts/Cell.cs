using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector3 worldPosition;
    public Vector2Int gridPosition;
    
    public GridDirection bestDirection;
    
    public byte cost;
    public ushort bestCost;
    
    public float height => worldPosition.y;
    
    public Cell(Vector3 worldPosition, Vector2Int gridPosition)
    {
        this.worldPosition = worldPosition;
        this.gridPosition = gridPosition;
        
        cost = 1;
        bestCost = ushort.MaxValue;
        bestDirection = GridDirection.None;
    }
    
    public void IncreaseCost(byte amount)
    {
        if (cost == byte.MaxValue)
        {
            return;
        }
        
        if (cost + amount >= byte.MaxValue)
        {
            cost = byte.MaxValue;
        }
        else
        {
            cost += amount;
        }
    }
}
