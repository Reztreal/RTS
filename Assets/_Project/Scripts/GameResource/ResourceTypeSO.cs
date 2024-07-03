using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceTypeSO")]
public class ResourceTypeSO : ScriptableObject
{
    public string resourceName;
    public Sprite resourceSprite;
}
