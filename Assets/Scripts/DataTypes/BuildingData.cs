// Copyright (C)2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Building Data", order = 2)]
public class BuildingData : ScriptableObject {

    [SerializeField] public BuildingType Id        = BuildingType.Empty;
    [SerializeField] public string Name            = "Empty";

    // What I cost to upgrade
    [SerializeField] public bool CanUpgrade   = false;
    [SerializeField] public uint CircleCost   = 0;
    [SerializeField] public uint SquareCost   = 0;
    [SerializeField] public uint TriangleCost = 0;
}
