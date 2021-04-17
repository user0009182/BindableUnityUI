using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ObservableProperty
{
    public object Value { get; protected set; }

    public event Action<ObservableProperty, object> OnChanged;

    public object Max { get; protected set; }
    public object Min { get; protected set; }

    public string[] Choices { get; protected set; }

    public void Set(object value, object source = null)
    {
        this.Value = value;
        OnChanged?.Invoke(this, source);
    }

    internal float AsFloat()
    {
        return Convert.ToSingle(Value);
    }

    internal string AsString()
    {
        return Convert.ToString(Value);
    }

    internal bool AsBool()
    {
        return Convert.ToBoolean(Value);
    }
}

class ObservableProperty<T> : ObservableProperty
{
    public ObservableProperty(T initialValue)
    {
        base.Value = initialValue;
    }

    public ObservableProperty(T initialValue, string[] choices)
    {
        base.Value = initialValue;
        base.Choices = choices;
    }

    public ObservableProperty(T initialValue, T minValue, T maxValue)
    {
        base.Value = initialValue;
        Min = minValue;
        Max = maxValue;
    }

    public void Set(T value, object source = null)
    {
        base.Set(value, source);
    }

    public new T Value
    {
        get
        {
            return (T)base.Value;
        }
        set
        {
            Set(value);
        }
    }
}
