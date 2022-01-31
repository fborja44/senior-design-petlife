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
	private float maxValue;

	public void SetMaxNeeds(float Needs)
	{
		slider.maxValue = Needs;
		slider.value = Needs;
		maxValue = Needs;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetNeeds(float Needs)
	{
		slider.value = Needs > maxValue ? maxValue : Needs;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}