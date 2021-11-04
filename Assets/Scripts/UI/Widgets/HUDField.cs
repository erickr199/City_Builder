// COpyright (C) 2021 Vancouver Film School, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDField : MonoBehaviour {

    // The field refererence
    protected TextMeshProUGUI Field;
    [SerializeField] private uint _value;

    private void Awake() {

        Field = GetComponent<TextMeshProUGUI>();
        SetValue( _value );
    }

    public virtual void SetValue( uint newValue ) {

        _value = newValue;
        Field.text = $"{_value}";
    }
}
