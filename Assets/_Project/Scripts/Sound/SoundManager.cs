using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameSoundParameters gameSoundParameters;

    private void OnEnable()
    {
        EventManager.AddListener("PlaySoundByName", OnPlaySoundByName);
    }
    
    private void OnDisable()
    {
        EventManager.RemoveListener("PlaySoundByName", OnPlaySoundByName);
    }
    
    private void OnPlaySoundByName(object data)
    {
        string soundName = (string) data;

        // Get all fields from GameSoundParameters
        FieldInfo[] fields = typeof(GameSoundParameters).GetFields();
        AudioClip clip = null;
        
        // Find the field with the same name as the soundName
        foreach (FieldInfo field in fields)
        {
            if (field.Name == soundName)
            {
                clip = (AudioClip) field.GetValue(gameSoundParameters);
                break;
            }
        }

        if (clip == null)
        {
            Debug.LogError("Sound not found: " + soundName);
            return;
        }
        
        audioSource.PlayOneShot(clip);
    }
}
