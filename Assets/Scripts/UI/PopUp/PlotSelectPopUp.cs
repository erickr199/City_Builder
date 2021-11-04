// Copyright (C)2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlotSelectPopUp : MonoBehaviour
{

    private enum PopupPanel {
        Error = 0,
        Building = 1,
        Upgrade = 2
    };
    [SerializeField] private BuildingSelect UI;


    private void OnPopUp( InputValue value )
    {
        RaycastHit hit;

        // var mouseWorldPos = Camera.main.ScreenToWorldPoint( Mouse.current.position );
        var ray = Camera.main.ScreenPointToRay( Mouse.current.position.ReadValue() );
        if (!Physics.Raycast( ray, out hit, Mathf.Infinity )) {
            UI.HideAll();
            return;
        }

        // We hit something and the thing we hit is in the variable hit
        GameObject selectedObj = hit.collider.gameObject;

        // Make sure this target has a place to build
        Builder placeToBuild;
        if (!selectedObj.transform.parent.TryGetComponent<Builder>( out placeToBuild )) {
            UI.HideAll();
            return;
        }

        // check for valid building area
        if (!selectedObj.CompareTag("BuildingArea")) {
            UI.HideAll();
            return;
        }

        var buildingData = selectedObj.transform.parent.GetComponentInChildren<DataConnect>().Data;
        if (buildingData.CanUpgrade)
        {
            // Assume we are building a new building
            var popupId = PopupPanel.Building;
            if (placeToBuild.Occupied) // If its occupied, switch to an upgrade
                popupId = PopupPanel.Upgrade;

            // Show the menu
            UI.Reveal((int)popupId, selectedObj );
        }
    }
}
