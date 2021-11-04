using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingSelect : MonoBehaviour
{
    [SerializeField] private GameObject   _errorPopup;
    [SerializeField] private GameObject[] _popupList;

    private Builder       _savedPlot;
    private RectTransform _popupPos;

    // Start is called before the first frame update
    void Start()
    {
        _errorPopup = _popupList[0];
        HideAll();
    }

    public GameObject Reveal( int panelId, GameObject selectedPlot )
    {
        HideAll();
        _popupPos = _popupList[panelId].GetComponent<RectTransform>();
        _popupPos.anchoredPosition = Mouse.current.position.ReadValue();
        _savedPlot = selectedPlot.GetComponentInParent<Builder>();

        _popupList[panelId].SetActive(true);

        return _popupList[panelId];
    }

    public void HideAll()
    {
        foreach (GameObject popUp in _popupList)
            popUp.SetActive(false);
    }

    public void BuildBarracks()
    {
        if (_savedPlot.Build( BuildingType.Barracks_L1 )) {
            HideAll();
            return;
        }
        DisplayError();
    }

    public void BuildFarm()
    {
        _savedPlot.Build( BuildingType.Farm_L1 );
        HideAll();
    }

    public void BuildSmithy()
    {
        _savedPlot.Build( BuildingType.Smithy_L1 );
        HideAll();
    }

    public void UpgradeBuilding()
    {
        _savedPlot.Upgrade();
        HideAll();
    }

    public void DisplayError() {

        _errorPopup.SetActive(true);
        _errorPopup.GetComponentInChildren<TextMeshPro>().text = _savedPlot.ErrorMessage;
    }

    public void HandleError()
    {
        _errorPopup.SetActive(false);
        HideAll();
    }
}
