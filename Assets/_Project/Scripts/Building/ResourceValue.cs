using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceValue
{
    public ResourceTypeSO resourceType;
    public int amount = 0;
    
    public ResourceValue(ResourceTypeSO resourceType, int amount)
    {
        this.resourceType = resourceType;
        this.amount = amount;
    }
}
