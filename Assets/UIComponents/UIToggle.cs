using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    Toggle toggle;
    ObservableProperty bindTarget;

    void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(OnValueChangedFromUI));
    }

    void OnValueChangedFromUI(bool newValue)
    {
        bindTarget.Set(newValue, this);
    }

    internal void BindTo(ObservableProperty property)
    {
        bindTarget = property;
        bindTarget.OnChanged += BindTarget_OnChanged;
        BindTarget_OnChanged(bindTarget, null);
    }

    private void BindTarget_OnChanged(ObservableProperty property, object source)
    {
        if (source == (object)this)
            return;
        var newValue = property.AsBool();
        toggle.isOn = newValue;
    }
}

