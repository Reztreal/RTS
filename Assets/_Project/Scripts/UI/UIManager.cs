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
    public Transform buildingInfoPanel;
    private Transform buildingInfoPanelResources;
    
    [Header("Resource UI")]
    public GameObject resourceUIPrefab;
    public Transform resourceMenu;
    
    private BuildingPlacer _buildingPlacer;

    private void OnEnable()
    {
        EventManager.AddListener("UpdateResourceUI", OnUpdateResourceUI);
        EventManager.AddListener("HoverBuildingButton", OnHoverBuildingButton);
        EventManager.AddListener("UnhoverBuildingButton", OnUnhoverBuildingButton);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener("UpdateResourceUI", OnUpdateResourceUI);
        EventManager.RemoveListener("HoverBuildingButton", OnHoverBuildingButton);
        EventManager.RemoveListener("UnhoverBuildingButton", OnUnhoverBuildingButton);
    }
    
    private void OnHoverBuildingButton(object data)
    {
        BuildingData buildingData = data as BuildingData;
        buildingInfoPanel.gameObject.SetActive(true);
        SetInfoPanel(buildingData);
    }
    
    private void OnUnhoverBuildingButton()
    {
        buildingInfoPanel.gameObject.SetActive(false);
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

    private void SetInfoPanel(BuildingData data)
    {
        if (data.unitName != "")
        {
            buildingInfoPanel.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = data.unitName;
        }
        
        foreach (Transform child in buildingInfoPanelResources)
        {
            Destroy(child.gameObject);
        }

        if (data.unitCost.Count > 0)
        {
            GameObject gameObject;
            Transform transform;

            foreach (ResourceValue resourceValue in data.unitCost)
            {
                gameObject = GameObject.Instantiate(resourceUIPrefab, buildingInfoPanelResources);
                transform = gameObject.transform;
                transform.Find("ResourceImage").GetComponent<Image>().sprite = resourceValue.resourceType.resourceSprite;
                if (Globals.RESOURCE_DATA.GetResource(resourceValue.resourceType.resourceName).amount < resourceValue.amount)
                {
                    transform.Find("ResourceAmount").GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>" + resourceValue.amount + "</color>";
                }
                else
                {
                    transform.Find("ResourceAmount").GetComponent<TMPro.TextMeshProUGUI>().text = resourceValue.amount.ToString();
                }
            }
        }
    }

    private void Awake()
    {
        _buildingPlacer = GetComponent<BuildingPlacer>();
        buildingInfoPanelResources = buildingInfoPanel.Find("ResourceCost");

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
            button.name = data.unitName;
            button.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = data.unitName;
            Button b = button.GetComponent<Button>();
            b.GetComponent<BuildingButton>().Initialize(data);
            AddBuildingButtonListener(b, i);
        }
    }

    private void AddBuildingButtonListener(Button b, int i)
    {
        b.onClick.AddListener(() => _buildingPlacer.SelectBuildingToPlace(i));
    }
}
