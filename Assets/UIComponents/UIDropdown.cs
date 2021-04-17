using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIDropdown : MonoBehaviour
{
    TMPro.TMP_Dropdown dropdown;
    ObservableProperty bindTarget;

    void Awake()
    {
        dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(OnValueChangedFromUI));
    }

    void OnValueChangedFromUI(int itemIndex)
    {
        var newValue = bindTarget.Choices[itemIndex];
        bindTarget.Set(newValue, this);
    }

    internal void BindTo(ObservableProperty property)
    {
        bindTarget = property;
        bindTarget.OnChanged += BindTarget_OnChanged;
        bool wholeNumbers = property.Value.GetType() == typeof(int);

        if (property.Choices != null)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(property.Choices.ToList());
        }

        BindTarget_OnChanged(bindTarget, null);

        //var propertyType = property.Value.GetType();
        //if (propertyType.IsEnum)
        //{
        //    dropdown.ClearOptions();
        //    dropdown.AddOptions(propertyType.GetEnumNames().ToList());
        //}
  
        //slider.minValue = System.Convert.ToSingle(property.Min);
        //slider.maxValue = System.Convert.ToSingle(property.Max);
        //slider.wholeNumbers = wholeNumbers;
        //if (wholeNumbers)
        //    slider.value = (int)bindTarget.Value;
        //else
        //    slider.value = (float)bindTarget.Value;
    }

    private void BindTarget_OnChanged(ObservableProperty property, object source)
    {
        if (source == (object)this)
            return;
        var newValue = property.AsString();
        int index = System.Array.IndexOf(property.Choices, newValue);
        dropdown.value = index;
    }
}

