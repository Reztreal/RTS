using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSoundParameters", menuName = "ScriptableObjects/Sound/GameSoundParameters")]
public class GameSoundParameters : ScriptableObject
{
    [Header("Ambient Sounds")] 
    public AudioClip onBuildingPlacedSound;
    
    [Header("Character Sounds")]
    public AudioClip onSettlerSelectedWithDragSound;
}
