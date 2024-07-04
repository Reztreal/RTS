using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DataLoader.LoadBuildingData();
        ResetResources();
    }

    public void ResetResources()
    {
        foreach (GameResource resource in Globals.RESOURCE_DATA.resourceTypeList)
        {
            resource.amount = 200;
        }
    }
}
