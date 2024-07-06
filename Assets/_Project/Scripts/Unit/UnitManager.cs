using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject selectionIndicator;
    public GameObject healthBar;
    
    public MeshRenderer meshRenderer;
    
    private Renderer _healthBarRenderer;
    private MaterialPropertyBlock _healthBarMaterialPropertyBlock;
    private int _healthBarPropertyID;
    

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
        healthBar.SetActive(false);
        
        _healthBarRenderer = healthBar.GetComponent<Renderer>();
        _healthBarMaterialPropertyBlock = new MaterialPropertyBlock();
        _healthBarPropertyID = Shader.PropertyToID("_Health");
    }

    private void Start()
    {
        Debug.Log(transform.position);
        SetHealthBarPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetHealth(0.3f);
        }
    }

    private void OnMouseDown()
    {
        if (IsAlive())
        {
            Select(true);
        }
    }

    public virtual bool IsAlive()
    {
        return true;
    }
    
    
    public void Select() {Select(false);}

    public void Select(bool clearSelection)
    {
        if (Globals.SELECTED_UNITS.Contains(this)) return;

        if (clearSelection)
        {
            List<UnitManager> selectedUnits = Globals.SELECTED_UNITS.ToList();
            foreach (UnitManager unit in selectedUnits)
            {
                unit.Deselect();
            }
        }
        
        Globals.SELECTED_UNITS.Add(this);
        selectionIndicator.SetActive(true);
        healthBar.SetActive(true);
    }

    public void Deselect()
    {
        if (!Globals.SELECTED_UNITS.Contains(this)) return;
        Globals.SELECTED_UNITS.Remove(this);
        selectionIndicator.SetActive(false);
        healthBar.SetActive(false);
    }

    public void SetHealthBarPosition()
    {
        Vector3 pos = transform.position;

        pos.y = 6;
        
        healthBar.transform.position = pos;
    }

    public virtual void SetHealth(float health)
    {
        _healthBarRenderer.GetPropertyBlock(_healthBarMaterialPropertyBlock);
        _healthBarMaterialPropertyBlock.SetFloat(_healthBarPropertyID, health);
        _healthBarRenderer.SetPropertyBlock(_healthBarMaterialPropertyBlock);
    }
    
    public virtual void RemoveHealth(float health)
    {
        _healthBarRenderer.GetPropertyBlock(_healthBarMaterialPropertyBlock);
        _healthBarMaterialPropertyBlock.SetFloat(_healthBarPropertyID, _healthBarMaterialPropertyBlock.GetFloat(_healthBarPropertyID) - health);
        _healthBarRenderer.SetPropertyBlock(_healthBarMaterialPropertyBlock);
    }
}
