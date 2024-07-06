using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameResourceDatabase", menuName = "ScriptableObjects/GameResource/GameResourceDatabase")]
public class GameResourceDatabaseSO : ScriptableObject
{
    
    // might make more sense to use a hashset here
    public List<GameResource> resourceTypeList;

    public GameResource GetResource(int index)
    {
        return resourceTypeList[index];
    }
    
    public GameResource GetResource(string resourceName)
    {
        foreach (GameResource resource in resourceTypeList)
        {
            if (resource.resourceType.resourceName == resourceName)
            {
                return resource;
            }
        }
        return null;
    }

    public GameResource GetResource(ResourceTypeSO resourceType)
    {
        foreach (GameResource resource in resourceTypeList)
        {
            if (resource.resourceType == resourceType)
            {
                return resource;
            }
        }

        return null;
    }
}
