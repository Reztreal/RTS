using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameResourceDatabaseSO")]
public class GameResourceDatabaseSO : ScriptableObject
{
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
}
