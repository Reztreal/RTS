using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : UnitManager
{
    private Character _character;

    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        _character = unit as Character;
    }
}
