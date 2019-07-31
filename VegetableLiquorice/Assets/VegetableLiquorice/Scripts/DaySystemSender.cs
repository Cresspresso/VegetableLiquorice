
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystemSender : MonoBehaviour
{
	public void NextDay()
	{
		DaySystem.instance.NextDay();
	}

	public void ResetGame()
	{
		DaySystem.instance.ResetGame();
	}
}
