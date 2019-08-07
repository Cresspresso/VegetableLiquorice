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

    public Image clockFill;
    public Image calendarFill;

    public Transform coinPrefab;
    public Transform coinStackOrigin;
    public float coinHeight = 0.2f;
    public float coinAltOffset = 0.05f;

    [SerializeField]
    [HideInInspector]
    private List<Transform> coins;

    public List<GameObject> happyBalloons;
    public List<GameObject> sadBalloons;


    public GameObject kidSkinnyIdle;
    public GameObject kidNormalIdle;
    public GameObject kidFatIdle;

    public GameObject kidSkinnyEating;
    public GameObject kidNormalEating;
    public GameObject kidFatEating;

    public GameObject kidSkinnyPlaying;
    public GameObject kidNormalPlaying;
    public GameObject kidFatPlaying;

    public enum Anim
    {
        Idle,
        Eating,
        Playing,
    }
    [HideInInspector]
    public Anim anim;



    public const int dayEnd = 8;
    public const int timeMax = 12;

    public const int weightInstantLoseLow = 0;
    public const int weightInstantLoseHigh = 15;
    public const int weightWinLow = 5;
    public const int weightWinHigh = 10;

    public const int happinessInstantLoseLow = 0;
    public const int happinessMax = 5;
    public const int happinessWinLow = 3;

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

        for (int i = 0; i < 30; i++)
        {
            var v = Vector3.up * i * coinHeight;
            if (i % 2 == 0)
            {
                v += Vector3.right * coinAltOffset;
            }
            coins.Add(Instantiate(
                coinPrefab,
                coinStackOrigin.TransformPoint(v),
                coinStackOrigin.rotation,
                coinStackOrigin));
        }

        anim = Anim.Idle;

        RestartGame();
    }

    public void RestartGame()
    {
        day = 1;
        time = timeMax;
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

            time = timeMax;

            anim = Anim.Idle;

            UpdateUI();

            if (day >= dayEnd
                || weight <= weightInstantLoseLow
                || happiness <= happinessInstantLoseLow)
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

            anim = Anim.Eating;

            UpdateUI();

            if (weight >= weightInstantLoseHigh)
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

            anim = Anim.Eating;

            UpdateUI();

            if (weight >= weightInstantLoseHigh)
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

            anim = Anim.Playing;

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
        if (happiness < happinessWinLow)
        {
            drawingCurrent = drawingSad;
        }
        else if (weight < weightWinLow)
        {
            drawingCurrent = drawingStarving;
        }
        else if (weight > weightWinHigh)
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

        clockFill.fillAmount = (float)time / timeMax;
        calendarFill.fillAmount = (float)day / (dayEnd - 1);

        for (int i = 0; i < coins.Count; ++i)
        {
            coins[i].gameObject.SetActive(i < money);
        }

        for (int i = 0; i < happyBalloons.Count; ++i)
        {
            happyBalloons[i].SetActive(i < happiness);
        }
        for (int i = 0; i < sadBalloons.Count; ++i)
        {
            sadBalloons[i].SetActive(i >= happiness);
        }



        bool isLow = weight < weightWinLow;
        bool isHigh = weight > weightWinHigh;
        switch (anim)
        {
            case Anim.Eating:

                kidSkinnyIdle.SetActive(false);
                kidNormalIdle.SetActive(false);
                kidFatIdle.SetActive(false);

                kidSkinnyPlaying.SetActive(false);
                kidNormalPlaying.SetActive(false);
                kidFatPlaying.SetActive(false);

                if (isLow)
                {
                    kidNormalEating.SetActive(false);
                    kidFatEating.SetActive(false);

                    kidSkinnyEating.SetActive(true);
                }
                else if (isHigh)
                {
                    kidSkinnyEating.SetActive(false);
                    kidNormalEating.SetActive(false);

                    kidFatEating.SetActive(true);
                }
                else
                {
                    kidSkinnyEating.SetActive(false);
                    kidFatEating.SetActive(false);

                    kidNormalEating.SetActive(true);
                }

                break;
            case Anim.Playing:

                kidSkinnyIdle.SetActive(false);
                kidNormalIdle.SetActive(false);
                kidFatIdle.SetActive(false);

                kidSkinnyEating.SetActive(false);
                kidNormalEating.SetActive(false);
                kidFatEating.SetActive(false);

                if (isLow)
                {
                    kidNormalPlaying.SetActive(false);
                    kidFatPlaying.SetActive(false);

                    kidSkinnyPlaying.SetActive(true);
                }
                else if (isHigh)
                {
                    kidSkinnyPlaying.SetActive(false);
                    kidNormalPlaying.SetActive(false);

                    kidFatPlaying.SetActive(true);
                }
                else
                {
                    kidSkinnyPlaying.SetActive(false);
                    kidFatPlaying.SetActive(false);

                    kidNormalPlaying.SetActive(true);
                }

                break;
            case Anim.Idle:
            default:

                kidSkinnyEating.SetActive(false);
                kidNormalEating.SetActive(false);
                kidFatEating.SetActive(false);

                kidSkinnyPlaying.SetActive(false);
                kidNormalPlaying.SetActive(false);
                kidFatPlaying.SetActive(false);

                if (isLow)
                {
                    kidNormalIdle.SetActive(false);
                    kidFatIdle.SetActive(false);

                    kidSkinnyIdle.SetActive(true);
                }
                else if (isHigh)
                {
                    kidSkinnyIdle.SetActive(false);
                    kidNormalIdle.SetActive(false);

                    kidFatIdle.SetActive(true);
                }
                else
                {
                    kidSkinnyIdle.SetActive(false);
                    kidFatIdle.SetActive(false);

                    kidNormalIdle.SetActive(true);
                }

                break;
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 100),
            string.Format("day = {0}\nhours = {1}\nmoney = {2}\nweight = {3}\nhappiness = {4}", day, time, money, weight, happiness));
    }
#endif
}
