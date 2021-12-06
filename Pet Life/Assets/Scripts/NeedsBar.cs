using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//From Brackeys video: https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

public class NeedsBar : MonoBehaviour
{

	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxNeeds(int Needs)
	{
		slider.maxValue = Needs;
		slider.value = Needs;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetNeeds(int Needs)
	{
		slider.value = Needs;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}