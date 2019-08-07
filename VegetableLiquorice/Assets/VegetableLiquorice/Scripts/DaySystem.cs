
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Singleton class that is persistent between scenes.
public class DaySystem : MonoBehaviour
{
	public StatBar dayBar;
	public StatBar moneyBar;
	public StatBar weightBar;
    public StatBar fullnessBar;

	public EndGamePanel endGamePanel;



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
		moneyBar.value = moneyBar.maxValue;
        weightBar.value = weightBar.maxValue / 2;
        fullnessBar.value = fullnessBar.maxValue / 2;
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

        var a = weightBar.OnEndDay();
        var b = fullnessBar.OnEndDay();
        switch (a)
        {
            case Ordering.Equal: break;
            case Ordering.Less: EndGame(EndGameOutcome.Underweight); break;
            case Ordering.Greater: EndGame(EndGameOutcome.Overweight); break;
        }
        switch (b)
        {
            case Ordering.Equal: break;
            case Ordering.Less: EndGame(EndGameOutcome.Starving); break;
            case Ordering.Greater: EndGame(EndGameOutcome.Overfull); break;
        }

		if (wasLastDay)
		{
			EndGame(CheckStatsForOutcome());
		}
	}

	public EndGameOutcome CheckStatsForOutcome()
    {
		switch (weightBar.WinOrdering)
        {
            case Ordering.Equal: break;
            case Ordering.Less: return EndGameOutcome.Underweight;
            case Ordering.Greater: return EndGameOutcome.Overweight;
        }
        switch (fullnessBar.WinOrdering)
        {
            case Ordering.Equal: break;
            case Ordering.Less: return EndGameOutcome.Starving;
            case Ordering.Greater: return EndGameOutcome.Overfull;
        }

        return EndGameOutcome.Perfect;
	}

	public void EndGame(EndGameOutcome outcome)
	{
		endGamePanel.Show(outcome);
	}
}
