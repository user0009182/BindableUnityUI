using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputField : MonoBehaviour
{
    TMPro.TMP_InputField inputField;
    ObservableProperty bindTarget;

    void Awake()
    {
        inputField = GetComponentInChildren<TMPro.TMP_InputField>();
        inputField.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<string>(OnValueChangedFromUI));
    }

    void OnValueChangedFromUI(string value)
    {
        bindTarget.Set(value, this);
    }

    internal void BindTo(ObservableProperty property)
    {
        bindTarget = property;
        bindTarget.OnChanged += BindTarget_OnChanged;
        BindTarget_OnChanged(property, null);
    }

    private void BindTarget_OnChanged(ObservableProperty property, object source)
    {
        if (source == (object)this)
            return;
        inputField.text = property.AsString();
    }
}

