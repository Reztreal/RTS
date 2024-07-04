using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameResource
{
    public ResourceTypeSO resourceType;
    public int amount;

    public GameResource(ResourceTypeSO resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }

    public void UpdateAmount(int amount)
    {
        int newAmount = this.amount + amount;
        if (newAmount < 0)
        {
            this.amount = 0;
        }
        else
        {
            this.amount = newAmount;
        }
    }
}
