using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals
{
    public static List<Transform> SPAWN_POINTS = new List<Transform>();
    
    public static GameResourceDatabaseSO RESOURCE_DATA;
    public static BuildingDatabaseSO BUILDING_DATA;
    
    public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();
    
    public static int TERRAIN_LAYER_MASK = 1 << 8;  
}
