using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : UnitManager
{
    private Character _character;

    public override void Initialize(Unit unit)
    {
        base.Initialize(unit);
        _character = unit as Character;
    }
    
    private void Update()
    {
        if (Globals.SELECTED_UNITS.Count > 0 && Input.GetMouseButtonUp(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, Globals.TERRAIN_LAYER_MASK))
            {
                foreach (UnitManager unitManager in Globals.SELECTED_UNITS)
                {
                    if (unitManager is CharacterManager characterManager)
                    {
                        characterManager.MoveTo(hit.point);
                    }
                }
            }
        }
    }

    public void MoveTo(Vector3 destination)
    {
        _character.Transform.GetComponent<NavMeshAgent>().SetDestination(destination);
    }
}
