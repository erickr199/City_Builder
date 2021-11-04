// Copyright (C)2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CityManager : MonoBehaviour
{
    /*
    Usage:
    Attach me to a base map prefab and let me manage the collection of
    land plots and buildings within those plots.

    My children should all be prefabs that have a BuildingType or PlotType
    to attach the int and a ResourceProducer to generate resources
    */
    [Header("City Zoning Data")]
    [SerializeField] private CityData _cityData; // <-- this is what we save...
    // [SerializeField] private bool _enableSave = false;

    [Space(15)]
    // List arrays are prefabs unless suffixed with Data, then they are Scriptable Objects
    [SerializeField] private List<GameObject> _plotPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> _buildingPrefabs = new List<GameObject>();

    // The live city (the plots)
    private GameObject[,] _generatedCityPrefabs;

    // Stash of Scriptable Object Data for each TYPE of building
    public Dictionary<BuildingType, GameObject> PlotInfo = new Dictionary<BuildingType, GameObject>();
    public Dictionary<BuildingType, GameObject> BuildingInfo = new Dictionary<BuildingType, GameObject>();


    public int Size { get => _cityData.Dimention; }
    private Vector3 _offsetFromOrigin;


    private void Awake()
    {
        // Initialize the default production resources
        if (_cityData == null) {
            Debug.Log("CityManager: Cannot have null City Data Attribute");
            return;
        }

        // -150% of Grid size Add to world positions
        float offset = -0.5f * _cityData.Dimention * _cityData.CellSize;
        _offsetFromOrigin = new Vector3( offset, 0f, offset );

        // Fill PlotInfo and BuildingInfo
        // Ordering in the same order as the BuildingEnum
        _buildingPrefabs.ForEach( bldg => {
            // for each building prefab, grab its data rec,
            // extract the id (enum) and use the enum as a key
            var data = bldg.GetComponent<DataConnect>().Data;
            if (!BuildingInfo.ContainsKey( data.Id ))
                BuildingInfo.Add( data.Id, bldg );
        });

        _plotPrefabs.ForEach( plot => {
            // now add all the plots with their IDs
            var data = plot.GetComponent<DataConnect>().Data;
            if (!PlotInfo.ContainsKey( data.Id ))
                PlotInfo.Add( data.Id, plot );
        });


        // Generate city plots and buildings lists
        GenerateCityPrefabs( _cityData.Dimention, _cityData.CellSize );
    }


    private void GenerateCityPrefabs(int citySize, int cellSize) {

        Vector3      position;
        BuildingType type;

        _generatedCityPrefabs = new GameObject[citySize, citySize];

        for(int x = 0; x < citySize; x++)
        {
            for(int z = 0; z < citySize; z++)
            {
                position = GetWorldPosition(x, z);
                type = _cityData.Map[x,z];

                _generatedCityPrefabs[x,z] = Instantiate( _plotPrefabs[0], position, Quaternion.identity, transform );
                Debug.Log("City.CreateCity: Plot created");

                // Now find the EmptyField Component, replace wtih prefab
                Builder building = _generatedCityPrefabs[x,z].GetComponentInChildren<Builder>();
                building.Build( type, x, z );
            }
        }
    }


    public bool AdjacentType( BuildingType adjacentTypeToTest, int x, int z, bool zDirection = true )
    {
        // Test the two adjacent city cells for the adjacentTypeToTest
        // if the zDirection is true test +/- 1 z unit else test +/- x unit

        bool adjacent = false;

        int axis = x;
        if (zDirection)
            axis = z;

        if ((axis > 0) || (axis < Size))
        {
            // OK range - 1
            BuildingType idMin = _cityData.Map[axis-1, z];
            BuildingType idMax = _cityData.Map[axis+1, z];
            if (zDirection) {
                idMin = _cityData.Map[x, axis-1];
                idMax = _cityData.Map[x, axis+1];
            }
            adjacent = ((idMin == adjacentTypeToTest) || (idMax == adjacentTypeToTest));
        }

        return adjacent;
    }


    private Vector3 GetWorldPosition(int x, int z )
    {
        return new Vector3(x+0.5f, 0, z+0.5f) * _cityData.CellSize + _offsetFromOrigin;
    }
}


