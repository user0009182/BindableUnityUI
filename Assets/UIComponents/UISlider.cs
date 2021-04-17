using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    Slider slider;
    ObservableProperty bindTarget;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(OnValueChangedFromUI));
    }

    void OnValueChangedFromUI(float value)
    {
        bindTarget.Set(value, this);
    }

    internal void BindTo(ObservableProperty property)
    {
        bindTarget = property;
        bindTarget.OnChanged += BindTarget_OnChanged;
        bool wholeNumbers = property.Value.GetType() == typeof(int);
        slider.minValue = System.Convert.ToSingle(property.Min);
        slider.maxValue = System.Convert.ToSingle(property.Max);
        slider.wholeNumbers = wholeNumbers;
        if (wholeNumbers)
            slider.value = (int)bindTarget.Value;
        else
            slider.value = (float)bindTarget.Value;
        BindTarget_OnChanged(property, null);
    }

    private void BindTarget_OnChanged(ObservableProperty property, object source)
    {
        if (source == (object)this)
            return;
        slider.value = System.Convert.ToSingle(property.Value);
    }
}

