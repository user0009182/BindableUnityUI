using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILabel : MonoBehaviour
{
    public string formatString;

    TMPro.TMP_Text label;
    ObservableProperty bindTarget;

    void Awake()
    {
        label = GetComponentInChildren<TMPro.TMP_Text>();
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
        if (TextFormatter != null)
        {
            label.text = TextFormatter(property.Value);
        }
        else if (!string.IsNullOrEmpty(formatString))
        {
            label.text = string.Format(formatString, property.Value);
        }
        else
        { 
            label.text = property.AsString();
        }
    }

    internal System.Func<object, string> TextFormatter { get; set; }
}
