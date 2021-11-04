// Copyright (C)2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text; // JSON stuff
using System.IO;
using Sirenix.OdinInspector; // Odin stuff
using Sirenix.Serialization;


[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/City Data", order = 1)]
[System.Serializable]
public class CityData : ScriptableObject {

    public string Name = "My City";
    public int Dimention = 9;      // grid of plots
    public int CellSize = 10;      // 10 scale units wide for each plot

    [ShowInInspector]
    public BuildingType[,] Map = {

        { BuildingType.Spawn, BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,       BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand,  BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.Wall,       BuildingType.Wall,        BuildingType.Wall,       BuildingType.Wall,       BuildingType.Wall,       BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.Wall,       BuildingType.Barracks_L1, BuildingType.Farm_L1,    BuildingType.Empty,      BuildingType.Wall,       BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.Wall,       BuildingType.Barracks_L1, BuildingType.Farm_L1,    BuildingType.Empty,      BuildingType.Wall,       BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.Wall,       BuildingType.Smithy_L3,   BuildingType.Smithy_L1,  BuildingType.Farm_L3,      BuildingType.Wall,       BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.Wall,       BuildingType.Wall,        BuildingType.Wall,       BuildingType.Wall,       BuildingType.Wall,       BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand,  BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.NoMansLand, BuildingType.Spawn},
        { BuildingType.Spawn, BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,       BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn,      BuildingType.Spawn},

    };
}

