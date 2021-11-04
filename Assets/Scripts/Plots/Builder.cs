// Copyright (C)2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    /*
    Usage:
    Attach me to a Footprint in a land plot and I will help
    replace the building or empty plot with another building
    */
    [SerializeField] protected GameObject[] buildingList;

    // Testing Dictionary for BuilingList
    // protected Dictionary<int, GameObject> BuildingList = new Dictionary<int, GameObject>();
    private int _cityX;
    private int _cityZ;

    // Find City manager and save it
    protected CityManager   City;
    protected GameObject    curBuilding;
    protected BuildingType  curBuildingType;

    public bool Occupied {get; protected set;}
    public int  CityX {get => _cityX; protected set { _cityX = value; } }
    public int  CityZ {get => _cityZ; protected set { _cityZ = value; } }

    public void SetOccupied( bool occupied = true ) { Occupied = occupied; }
    public void SetCityX( int x ) { CityX = x; }
    public void SetCityZ( int z ) { CityZ = z; }

    private string _errMsg = "";
    public string ErrorMessage { get { return _errMsg; } protected set { _errMsg = value; }}



    public void Awake() {

        City = FindObjectOfType<CityManager>();
        //curBuilding = gameObject.GetComponentInChildren<GameObject>();
    }

    public void Build( BuildingType id, int x, int z) {
        // Use this version for new buildings
        CityX = x;
        CityZ = z;

        if (!CheckBuildingId( id )) {
            ErrorMessage = "PlotBuilding: Build failed - Building ID is out of range";
            return;
        }

        var rotation = Quaternion.identity;
        if (id == BuildingType.Wall)
            rotation = AdjustWall( id, x, z );

        curBuildingType = id;
        curBuilding = Instantiate( City.BuildingInfo[id], transform.position, rotation, transform );
        if (id != BuildingType.Empty)
            SetOccupied();


    }

    private Quaternion AdjustWall(BuildingType id, int x, int z)
    {
        if (City.AdjacentType( BuildingType.NoMansLand, x, z )) {
            // we have a wall adjacent to no mans land in z so rotate it

            // TODO: AdjacentType( BuildingType.NoMansLand, x, z, false ) checks x
            // if its also got an adjacent X no mans land its a corner, so replace the whole thing
            // with a corner piece.
            return Quaternion.Euler( 0, 90, 0 );
        }

        return Quaternion.identity;
    }


    public bool Build( BuildingType id ) {
        // Use this for upgrades
        if (!CheckBuildingId( id )) {
            ErrorMessage = "Builder: Failed Upgrade - No more upgrades to this type of building";
            return false;
        }

        // Check the CanUpgrade flag
        var buildingData = curBuilding.GetComponentInChildren<DataConnect>().Data;
        if ((buildingData == null) || (!buildingData.CanUpgrade))
            return false;

        curBuildingType = id;

        // Future: To destroy,
        //      trigger dust particles
        //      translate building below surface
        Destroy( curBuilding );

        // Future: Instantiate below surface
        //      translate to surface
        //      fade out dust particles
        curBuilding = Instantiate( City.BuildingInfo[id], transform.position, Quaternion.identity, transform);
        SetOccupied();
        return true;
    }


    public void Upgrade() {
        // Build the next thing in sequence if we can
        Build( curBuildingType + 1 );
    }


    private bool CheckBuildingId( BuildingType id ) {

        if (City.BuildingInfo.ContainsKey( id ))
            return true; // OK

        ErrorMessage = "PlotBase: BuildingId out of range";
        return false; // Uh oh
    }
}
