using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Ordering
{
    Less,
    Equal,
    Greater,
}

[RequireComponent(typeof(Slider))]
public class StatBar : MonoBehaviour
{
    public Slider slider { get { return GetComponent<Slider>(); } }

    public float minValue { get { return slider.minValue; } set { slider.minValue = value; } }
    public float maxValue { get { return slider.maxValue; } set { slider.maxValue = value; } }
    public float value {
        get { return slider.value; }
        set {
            slider.value = value;
            value = slider.value; // get clamped value
            bool isBad = value < minWinValue || value > maxWinValue;
            handle.color = isBad ? badColor : goodColor;
        }
    }

    public float minWinValue = 5;
    public float maxWinValue = 15;
    public float gainPerDay = 0;
    public Image handle;
    public Color goodColor = Color.white;
    public Color badColor = Color.red;

    private void OnValidate()
    {
        minWinValue = Mathf.Clamp(minWinValue, minValue, maxValue);
        maxWinValue = Mathf.Clamp(maxWinValue, minValue, maxValue);
    }

    public bool IsPerfect()
    {
        return value >= minWinValue && value <= maxWinValue;
    }

    public Ordering WinOrdering
    {
        get
        {
            if (value < minWinValue) { return Ordering.Less; }
            else if (value > maxWinValue) { return Ordering.Greater; }
            else { return Ordering.Equal; }
        }
    }

    public Ordering OnEndDay()
    {
        value += gainPerDay;

        if (value <= minValue) { return Ordering.Less; }
        else if (value >= maxValue) { return Ordering.Greater; }
        else { return Ordering.Equal; }
    }
}
