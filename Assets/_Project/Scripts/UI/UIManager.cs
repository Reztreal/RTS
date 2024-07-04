using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Building UI")]
    public GameObject buildingButtonPrefab;
    public Transform buildingMenu;
    
    [Header("Resource UI")]
    public GameObject resourceUIPrefab;
    public Transform resourceMenu;
    
    private BuildingPlacer _buildingPlacer;

    private void OnEnable()
    {
        EventManager.AddListener("UpdateResourceUI", OnUpdateResourceUI);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateResourceUI", OnUpdateResourceUI);
    }

    private void OnUpdateResourceUI()
    {
        UpdateResources();
    }

    private void UpdateResources()
    {
        foreach (GameResource resource in Globals.RESOURCE_DATA.resourceTypeList)
        {
            SetResourceText(resource.resourceType.resourceName, resource.amount);
        }
    }
    
    private void SetResourceText(string resourceName, int amount)
    {
        resourceMenu.Find(resourceName).Find("ResourceAmount").GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
    }

    private void Awake()
    {
        _buildingPlacer = GetComponent<BuildingPlacer>();

        for (int i = 0; i < Globals.RESOURCE_DATA.resourceTypeList.Count; i++)
        {
            GameObject resourceUI = GameObject.Instantiate(
                resourceUIPrefab,
                resourceMenu
            );
            GameResource resource = Globals.RESOURCE_DATA.GetResource(i);
            resourceUI.name = resource.resourceType.resourceName;
            resourceUI.transform.Find("ResourceImage").GetComponent<Image>().sprite = resource.resourceType.resourceSprite;
            resourceUI.transform.Find("ResourceAmount").GetComponent<TMPro.TextMeshProUGUI>().text = resource.amount.ToString();
        }

        for (int i = 0; i < Globals.BUILDING_DATA.buildingDataList.Count; i++)
        {
            GameObject button = GameObject.Instantiate(
                buildingButtonPrefab,
                buildingMenu
            );
            BuildingData data = Globals.BUILDING_DATA.GetBuildingData(i);
            button.name = data.buildingName;
            button.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = data.buildingName;
            Button b = button.GetComponent<Button>();
            AddBuildingButtonListener(b, i);
        }
    }

    private void AddBuildingButtonListener(Button b, int i)
    {
        b.onClick.AddListener(() => _buildingPlacer.SelectBuildingToPlace(i));
    }
}
