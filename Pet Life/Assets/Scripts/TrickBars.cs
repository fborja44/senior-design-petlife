using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//From Brackeys video: https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

public class TrickBars : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text text;
    private float minValue;

    public void SetMinTrick(int Trick)
    {
        slider.minValue = 0;
        slider.value = Trick;
        minValue = Trick;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetTrick(int Trick)
    {
        slider.value = Trick < minValue ? minValue : Trick;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}