using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Globals
{
    public static NavMeshSurface NAV_MESH_SURFACE;
    
    public static List<Transform> SPAWN_POINTS = new List<Transform>();
    
    public static GameResourceDatabaseSO RESOURCE_DATA;
    public static BuildingDatabaseSO BUILDING_DATA;
    
    public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();
    
    public static int TERRAIN_LAYER_MASK = 1 << 8;
    public static int UNIT_LAYER_MASK = 1 << 9;

    public static void UpdateNavMeshSurface()
    {
        NAV_MESH_SURFACE.UpdateNavMesh(NAV_MESH_SURFACE.navMeshData);
    }
}
