using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyFood : MonoBehaviour
{
	public int cost = 1;
	public int weight = 1;
    public int fullness = 1;

	public void Eat()
	{
		var sys = DaySystem.instance;
		if (sys.Money >= cost)
		{
			sys.Money -= cost;
			sys.Weight += weight;
            sys.Fullness += fullness;
		}
		else
		{
			Debug.LogWarning("Not enough money to eat.");
		}
	}
}
