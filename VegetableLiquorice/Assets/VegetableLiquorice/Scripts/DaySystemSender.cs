
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for passing messages through buttons to the singleton DaySystem.
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
