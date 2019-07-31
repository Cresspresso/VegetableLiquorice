using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyFood : MonoBehaviour
{
	public void Eat()
	{
		DaySystem.instance.bars.weightBar.value += 1;
	}
}
