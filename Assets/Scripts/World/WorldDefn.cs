using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType : ushort {
    Empty       = 0,
    Spawn       = 11, Wall = 12,        NoMansLand = 13,
    Barracks_L1 = 21, Barracks_L2 = 22, Barracks_L3 = 23,
    Farm_L1     = 31, Farm_L2= 32,      Farm_L3 = 33,
    Smithy_L1   = 41, Smithy_L2 = 42,   Smithy_L3 = 43
}

