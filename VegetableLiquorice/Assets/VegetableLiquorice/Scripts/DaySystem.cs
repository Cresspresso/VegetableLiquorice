
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton class that is persistent between scenes.
public class DaySystem : MonoBehaviour
{
	public BarManager bars;



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

		bars.dayBar.value = 0;
		bars.weightBar.value = 5;

		SceneManager.LoadScene("Elijah");
	}
	
	public void NextDay()
	{
		bool wasLastDay = bars.dayBar.value == bars.dayBar.maxValue;

		if (!wasLastDay)
		{
			bars.dayBar.value += 1;
			Debug.Log("Day is now Day Index " + bars.dayBar.value);
		}
		
		bars.weightBar.value -= 2;

		if (wasLastDay)
		{
			StartCoroutine(EndGame());
		}
	}

	private IEnumerator EndGame()
	{
		Debug.Log("Game has ended."); // TODO
		yield return new WaitForEndOfFrame();
		SceneManager.LoadScene("ElijahEndScene");
	}
}
