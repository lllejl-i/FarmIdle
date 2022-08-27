using PlantingBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void OnSecondsChangedHandler(float seconds);

public class GreenHouse : MonoBehaviour, IPointerClickHandler
{
    public event OnSecondsChangedHandler OnSecondsChanged;
    [SerializeField]
    private GameObject itemLocker, plantItem, groundItem;
    private TextMeshProUGUI timer;

    private Worker worker;
    private Plant plant;
    private float square = 1;
    private float timeSpeedMultiplier = 1, harvestMultiplier = 1;
    private float harvestMultiplyDuration = 0, timeSpeedDuration = 0;
    private float time;
    private bool isAutoBuy = false;
    private bool isLocked = false;
    private int plantStage = 0;

    private List<float> allTimes = new List<float>();
    private float Time {
        get { return time; }
        set
        {
            if (time != value)
            {
                time = value;
                OnSecondsChanged?.Invoke(value);
            }
        } }
    public void Initialize(PlantData data, PlantType type, float currentTime, float square, string timeString,TextMeshProUGUI timer, 
                           bool isWorker = false, bool isAutoBuy = false, float timeSpeedDuration = 0, float timeSpeedMultiplier = 1,
                           float harvestMultiplyDuration = 0, float harvestMultiplier = 1)
    {
        Initialize(type, square, timer, isAutoBuy, false);
        SeedPlant(data, currentTime);
        DateTime time = DateTime.Parse(timeString);
        if (isWorker)
        {
            AddWorker();
            var nextTime = (plant.AfterHarvestIndex > 0) ? allTimes[plant.AfterHarvestIndex] : allTimes[0];
            var times = worker.GetAfkItems(plant, square, nextTime,
                time, currentTime, isAutoBuy, harvestMultiplier, timeSpeedMultiplier, harvestMultiplyDuration, timeSpeedDuration);

            worker.GetAfkItems(plant, 10, nextTime, DateTime.Now.AddMinutes(-1), 0, false);

            Time = times.timeToNext;
            timeSpeedDuration = times.speedTime;
            harvestMultiplyDuration = times.harvestTime;
        }
        
        AddHarvestMultiply(harvestMultiplier, harvestMultiplyDuration);
        AddTimeSpeedMultiply(timeSpeedMultiplier, timeSpeedDuration);
    }

    public void Initialize(PlantType type, float square, TextMeshProUGUI timer,
    bool isAutoBuy = false, bool isLocked = true)
    {
        this.timer = timer;
        groundItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ResourcesPath.GreenhousesImages + type.ToString());
        this.isLocked = isLocked;
        if (isLocked)
            itemLocker.SetActive(true);
        this.isAutoBuy = isAutoBuy;
        this.square = square;
    }

    #region Greenhouse plant behaviour
    public void SeedPlant(PlantData data, float currentTime = float.MaxValue)
    {
        allTimes = new List<float> { data.RipeningTime + data.GrowthTime, data.RipeningTime, 0 };
        int plantStage;
        if (currentTime > allTimes[1])
            plantStage = 0;
        else if (currentTime <= allTimes[1] && currentTime > 0)
            plantStage = 1;
        else
            plantStage = 2;


        plant = Plant.CreatePlant(data, plantStage);
        plant.OnStageChanging += SetSprite;
        SetSprite(plantStage);
        OnSecondsChanged += TimerChange;
        OnSecondsChanged += (float time) =>
        {
            if (time <= data.RipeningTime && this.plantStage == 0)
            {
                plant.GrowStage = 1;
            }
            else if (time <= 0 && this.plantStage == 1)
            {
                plant.GrowStage = 2;
            }
        };
        OnSecondsChanged += (float time) =>
            {
                if (worker?.Tasks.Count > 0 && time <= 0)
                {
                    StartCoroutine(Work());
                }
            };
        TimeController.Instance.OnSecondChanging += PerSecond;
        if (currentTime > allTimes[0])
            Time = allTimes[0];
        else
            Time = currentTime;
    }

    private void RemovePlant()
    {
        TimeController.Instance.OnSecondChanging -= PerSecond;
        OnSecondsChanged = null;
    }

    #endregion
    private void TimerChange(float time)
    {
        if (Time <= 0)
        {
            timer.enabled = false;
            return;
        }

        if (!timer.enabled)
            timer.enabled = true;

        timer.text = Helper.SecondsToMinutes(time);
    }

    private void GetHarvest()
    {
        var harvest = plant.GetHarvest();
        if (plantStage == -1)
        {
            RemovePlant();
            if (isAutoBuy)
                SeedPlant(plant.Data);
        }
        else
        {
            Time = allTimes[plantStage];
        }

        harvest.count = (int)(harvest.count * harvestMultiplier);
        ///ToDo: Add harvest to Ambar
    }

    private void PerSecond()
    {
        Time -= timeSpeedMultiplier;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isLocked)
        {
            if (plantStage == 2)
            {
                GetHarvest();
            }
            else
            {
                ///ToDo: Open greenhouse menu
            }
        }
        else
        {
            ///ToDo: Open buy greenhouse menu
        }
    }

    private void SetSprite(int index)
    {
        plantStage = index;
        plantItem.GetComponent<SpriteRenderer>().sprite = (index >= 0) ? plant?.GetSprite(index) : null;
    }

    #region BustersMethods
    public void AddTimeSpeedMultiply(float multiplier, float time)
    {
        StartCoroutine(TimeMultiplierTick(multiplier, time));
    }

    public void AddHarvestMultiply(float multiplier, float time)
    {
        StartCoroutine(HarvestMultiplierTick(multiplier, time));
    }

    private IEnumerator TimeMultiplierTick(float multiplier, float time)
    {
        timeSpeedMultiplier *= multiplier;
        timeSpeedDuration = time;

        while (timeSpeedDuration > 0)
        {
            yield return new WaitForSeconds(1);
            timeSpeedDuration -= 1;
        }
        timeSpeedDuration = 0;
        timeSpeedMultiplier /= multiplier;
    }

    private IEnumerator HarvestMultiplierTick(float multiplier, float time)
    {
        harvestMultiplier *= multiplier;
        harvestMultiplyDuration = time;
        while (harvestMultiplyDuration > 0)
        {
            yield return new WaitForSeconds(1);
            harvestMultiplyDuration -= 1;
        }
        harvestMultiplyDuration = 0;
        harvestMultiplier /= multiplier;
    }
    #endregion
    #region Worker Behaviour
    private IEnumerator Work()
    {
        foreach (var task in worker.Tasks)
        {
            yield return new WaitForSeconds(1);
            task.Invoke();
        }
       
    }

    private void AddWorker()
    {
        if (worker == null)
        {
            worker = new Worker();
            worker.AddAction(GetHarvest);
        }
    }

    private void RemoveWorker()
    {
        worker = null;
    }
    #endregion
}