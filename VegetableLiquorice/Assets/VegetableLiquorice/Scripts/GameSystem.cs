using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [HideInInspector]
    public int day;
    [HideInInspector]
    public int time;
    [HideInInspector]
    public int money;
    [HideInInspector]
    public int weight;
    [HideInInspector]
    public int happiness;

    public Button nextDayButton;
    public Button healthyButton;
    public Button unhealthyButton;
    public Button playWithKidButton;
    public Button restartButton;

    public GameObject cameraTable;
    public GameObject cameraFridge;

    [HideInInspector]
    [SerializeField]
    private GameObject cameraCurrent_;
    public GameObject cameraCurrent
    {
        get { return cameraCurrent_; }
        set
        {
            if (cameraCurrent_) { cameraCurrent_.SetActive(false); }
            cameraCurrent_ = value;
            if (cameraCurrent_) { cameraCurrent_.SetActive(true); }
        }
    }

    public GameObject drawingHappy;
    public GameObject drawingSad;
    public GameObject drawingOverweight;
    public GameObject drawingStarving;

    [HideInInspector]
    [SerializeField]
    private GameObject drawingCurrent_;
    public GameObject drawingCurrent
    {
        get { return drawingCurrent_; }
        set
        {
            if (drawingCurrent_) { drawingCurrent_.SetActive(false); }
            drawingCurrent_ = value;
            if (drawingCurrent_) { drawingCurrent_.SetActive(true); }
        }
    }

    public const int dayEnd = 8;
    public const int weightLow = 0;
    public const int weightHigh = 15;
    public const int happinessLow = 0;
    public const int happinessMax = 5;

    public const int healthyMoney = 1;
    public const int healthyTime = 2;
    public const int unhealthyMoney = 1;
    public const int unhealthyTime = 1;
    public const int playTime = 2;

    private void Start()
    {
        nextDayButton.onClick.AddListener(NextDay);
        healthyButton.onClick.AddListener(EatHealthy);
        unhealthyButton.onClick.AddListener(EatUnhealthy);
        playWithKidButton.onClick.AddListener(PlayWithKid);
        restartButton.onClick.AddListener(RestartGame);

        RestartGame();
    }

    public void RestartGame()
    {
        day = 1;
        time = 12;
        money = 7;
        weight = 8;
        happiness = 3;

        restartButton.interactable = false;

        cameraFridge.SetActive(false);
        cameraTable.SetActive(false);
        cameraCurrent = cameraTable;

        drawingHappy.SetActive(false);
        drawingSad.SetActive(false);
        drawingStarving.SetActive(false);
        drawingOverweight.SetActive(false);
        drawingCurrent = null;

        UpdateUI();
    }

    public void NextDay()
    {
        if (day < dayEnd)
        {
            day++;
            money += 2 + time;
            weight -= 1;
            happiness -= Mathf.FloorToInt((float)time / 2);

            time = 12;

            UpdateUI();

            if (day >= dayEnd
                || weight <= weightLow
                || happiness <= happinessLow)
            {
                EndGame();
            }
        }
        else
        {

        }
    }

    public void EatHealthy()
    {
        if (money >= healthyMoney && time >= healthyTime)
        {
            money -= healthyMoney;
            time -= healthyTime;
            weight += 2;

            UpdateUI();

            if (weight >= weightHigh)
            {
                EndGame();
            }
        }
        else
        {

        }
    }

    public void EatUnhealthy()
    {
        if (money >= unhealthyMoney && time >= unhealthyTime)
        {
            money -= unhealthyMoney;
            time -= unhealthyTime;
            weight += 3;
            happiness += 1;
            if (happiness > happinessMax) { happiness = happinessMax; }

            UpdateUI();

            if (weight >= weightHigh)
            {
                EndGame();
            }
        }
        else
        {

        }
    }

    public void PlayWithKid()
    {
        if (time >= playTime)
        {
            time -= playTime;
            happiness += 1;
            if (happiness > happinessMax) { happiness = happinessMax; }

            UpdateUI();
        }
        else
        {

        }
    }

    public void EndGame()
    {
        restartButton.interactable = true;

        cameraCurrent = cameraFridge;
        if (happiness < 3)
        {
            drawingCurrent = drawingSad;
        }
        else if (weight < 5)
        {
            drawingCurrent = drawingStarving;
        }
        else if (weight > 10)
        {
            drawingCurrent = drawingOverweight;
        }
        else
        {
            drawingCurrent = drawingHappy;
        }
    }

    public void UpdateUI()
    {
        healthyButton.image.color = money >= healthyMoney && time >= healthyTime ? Color.white : Color.red;
        unhealthyButton.image.color = money >= unhealthyMoney && time >= unhealthyTime ? Color.white : Color.red;
        playWithKidButton.image.color = time >= playTime ? Color.white : Color.red;
        nextDayButton.image.color = day < dayEnd ? Color.white : Color.red;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100),
            string.Format("day = {0}\nhours = {1}\nmoney = {2}\nweight = {3}\nhappiness = {4}", day, time, money, weight, happiness));
    }
}
