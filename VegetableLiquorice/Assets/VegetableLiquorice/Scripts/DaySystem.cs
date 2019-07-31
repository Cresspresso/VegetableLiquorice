
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Singleton class that is persistent between scenes.
public class DaySystem : MonoBehaviour
{
	public Slider dayBar;
	public Slider moneyBar;
	public Slider weightBar;

	public EndGamePanel endGamePanel;

	public int minFinalWeight = 5;
	public int maxFinalWeight = 15;

	public int weightLossPerDay = 2;

	public float Weight
	{
		get { return weightBar.value; }
		set
		{
			weightBar.value = value;
			float clamped = weightBar.value;
			if (clamped == weightBar.minValue)
			{
				EndGame(EndGameOutcome.Underweight);
			}
			else if (clamped == weightBar.maxValue)
			{
				EndGame(EndGameOutcome.Overweight);
			}
		}
	}

	public float Money
	{
		get { return moneyBar.value; }
		set
		{
			if (value < -0.001f)
			{
				throw new InvalidOperationException("Money must be >= 0");
			}
			moneyBar.value = value;
		}
	}

	public float DayIndex
	{
		get { return dayBar.value; }
		set
		{
			dayBar.value = value;
		}
	}



	#region Singleton
	private static DaySystem _instance;
	public static DaySystem instance
	{
		get
		{
			if (!_instance)
			{
				_instance = FindObjectOfType<DaySystem>();
				if (!_instance)
				{
					Debug.LogError("DaySystem.instance is null");
				}
			}
			return _instance;
		}
		private set
		{
			if (_instance)
			{
				_instance = value;
			}
			else
			{
				Debug.LogError("DaySystem.instance cannot be null");
			}
		}
	}
	#endregion Singleton



	private void Awake()
	{
		#region Singleton
		if (instance != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		#endregion Singleton
	}

	private void Start()
	{
		Debug.Log("Created DaySystem.instance singleton.");
		ResetGame();
	}
	
	public void ResetGame()
	{
		Debug.Log("Game has been restarted.");

		endGamePanel.Hide();

		dayBar.value = 0;
		weightBar.value = weightBar.maxValue / 2;
		moneyBar.value = moneyBar.maxValue;

		SceneManager.LoadScene("Elijah");
	}
	
	public void NextDay()
	{
		bool wasLastDay = dayBar.value == dayBar.maxValue;

		if (!wasLastDay)
		{
			dayBar.value += 1;
			moneyBar.value = moneyBar.maxValue;

			Debug.Log("Day is now Day Index " + dayBar.value);
		}
		
		Weight -= weightLossPerDay;

		if (wasLastDay)
		{
			EndGame(CheckStatsForOutcome());
		}
	}

	public EndGameOutcome CheckStatsForOutcome()
	{
		if (weightBar.value < minFinalWeight)
		{
			return EndGameOutcome.Underweight;
		}
		else if (weightBar.value > maxFinalWeight)
		{
			return EndGameOutcome.Overweight;
		}
		else
		{
			return EndGameOutcome.Perfect;
		}
	}

	public void EndGame(EndGameOutcome outcome)
	{
		endGamePanel.Show(outcome);
	}
}
