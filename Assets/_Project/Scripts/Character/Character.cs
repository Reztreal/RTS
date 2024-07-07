using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    private CharacterData _characterData;
    
    public Character(CharacterData characterData) : base(characterData)
    {
        _characterData = characterData;
    }
}
