
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySystem : MonoBehaviour
{
	public int numDays = 7;
	public int currentDay = 0;



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
	}
	
	public void ResetGame()
	{
		Debug.Log("Game has been restarted.");
		currentDay = 0;
		SceneManager.LoadScene("Elijah");
	}

	public void NextDay()
	{
		currentDay = Mathf.Clamp(currentDay + 1, 0, numDays);
		if (currentDay == numDays)
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
