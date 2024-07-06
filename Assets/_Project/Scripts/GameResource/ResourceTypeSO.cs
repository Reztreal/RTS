using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceType", menuName = "ScriptableObjects/Resources/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string resourceName;
    public Sprite resourceSprite;
}
