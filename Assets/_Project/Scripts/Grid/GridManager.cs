using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    public Terrain terrain;
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    
    public FlowField flowField { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        flowField = new FlowField(gridSize, cellRadius, terrain);
        flowField.CreateGrid();
    }

    // private void OnDrawGizmos()
    // {
    //     GUIStyle style = new GUIStyle(GUI.skin.label);
    //     style.fontSize = 10;
    //     if (flowField != null && flowField.grid != null)
    //     {
    //         for (int x = 0; x < flowField.gridSize.x; x++)
    //         {
    //             for (int y = 0; y < flowField.gridSize.y; y++)
    //             {
    //                 Cell cell = flowField.grid[x, y];
    //                 Gizmos.color = Color.yellow;
    //                 Gizmos.DrawWireCube(cell.worldPosition, Vector3.one * cellRadius);
    //
    //                 // Draw the best direction of the cell as an arrow
    //                 Vector3 direction = new Vector3(cell.bestDirection.Vector.x, 0, cell.bestDirection.Vector.y);
    //                 if (direction != Vector3.zero)
    //                 {
    //                     UnityEditor.Handles.color = Color.red;
    //                     UnityEditor.Handles.ArrowHandleCap(0, cell.worldPosition, Quaternion.LookRotation(direction), cellRadius * 1.5f, EventType.Repaint);
    //                 }
    //
    //                 // Draw the best cost of the cell as a label
    //                 UnityEditor.Handles.color = Color.red;
    //                 UnityEditor.Handles.Label(cell.worldPosition, cell.bestCost.ToString(), style);
    //             }
    //         }
    //     }
    // }
}
