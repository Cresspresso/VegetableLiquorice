using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndGameOutcome
{
	Perfect,
	Overweight,
	Underweight,
}

public class EndGamePanel : MonoBehaviour
{
	public GameObject perfect;
	public GameObject overweight;
	public GameObject underweight;

	public void Show(EndGameOutcome outcome)
	{
		switch (outcome)
		{
			case EndGameOutcome.Perfect:
				{
					perfect.SetActive(true);
				}
				break;
			case EndGameOutcome.Overweight:
				{
					overweight.SetActive(true);
				}
				break;
			case EndGameOutcome.Underweight:
				{
					underweight.SetActive(true);
				}
				break;
		}
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);

		perfect.SetActive(false);
		overweight.SetActive(false);
		underweight.SetActive(false);
	}
}
