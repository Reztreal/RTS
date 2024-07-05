using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UnitSelection : MonoBehaviour
{
    private Vector3 mouseDragStartPosition;
    private bool isDragging = false;

    private void Update()
    {
        if (Globals.SELECTED_UNITS.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeselectAllUnits();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform.transform.CompareTag("Terrain"))
                    {
                        DeselectAllUnits();
                    }
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            mouseDragStartPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        if (isDragging && mouseDragStartPosition != Input.mousePosition)
        {
            SelectUnitsDrag();
        }
    }

    private void SelectUnitsDrag()
    {
        Bounds bounds = SelectionUtil.GetViewportBounds(Camera.main, mouseDragStartPosition, Input.mousePosition);
        GameObject[] selectableUnits = GameObject.FindGameObjectsWithTag("Unit");
        
        bool inBounds = false;
        foreach (GameObject unit in selectableUnits)
        {
            inBounds = bounds.Contains(Camera.main.WorldToViewportPoint(unit.transform.position));
            
            if (inBounds)
            {
                unit.GetComponent<UnitManager>().Select();
            }
            else
            {
                unit.GetComponent<UnitManager>().Deselect();
            }
        }
        
    }

    public void DeselectAllUnits()
    {
        List<UnitManager> selectedUnits = new List<UnitManager>(Globals.SELECTED_UNITS);

        foreach (UnitManager unit in selectedUnits)
        {
            unit.Deselect();
        }
    }

    private void OnGUI()
    {
        if (isDragging)
        {
            Rect rect = SelectionUtil.GetScreenRect(mouseDragStartPosition, Input.mousePosition);
            SelectionUtil.DrawScreenRectBorder(rect, 1, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
