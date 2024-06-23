using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }
    
    public List<Unit> units = new List<Unit>();
    public LayerMask unitLayerMask;
    public LayerMask groundLayerMask;
    
    public FlowField flowField;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, unitLayerMask))
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                {
                    units.Add(unit);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
            {
                Cell destinationCell = GridManager.Instance.flowField.GetCellAtWorldPosition(hit.point);
                
                GridManager.Instance.flowField.Reset();
                GridManager.Instance.flowField.CreateIntegrationField(destinationCell);
                GridManager.Instance.flowField.CreateFlowField();
                MoveUnits(hit.point);
            }
        }
    }
    
    public void MoveUnits(Vector3 destination)
    {
        foreach (Unit unit in units)
        {
            unit.Move(destination);
        }
    }
}
