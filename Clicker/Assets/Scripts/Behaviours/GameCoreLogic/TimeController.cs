using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void TimerChanging(); 

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }
    public event TimerChanging OnSecondChanging;
    public event TimerChanging OnDayChanging;

    [SerializeField]
    [Min(0.01f)]
    [Header("The number of minutes of real time in which a day passes")]
    private float realMinutesInDay;

    private bool isTimeLeft = false;
    private float timeLeftInMinutes = 0;
    private bool isNeedToBreakCycle = false;
    private int day = 0;
    private int seconds = 0;

    public float TimePerDay => realMinutesInDay;
    public int Day 
    { 
        get => day;
        private set 
        { 
            if(day != value)
            {
                day = value;
                OnDayChanging?.Invoke();
            }
        } 
    }
    public Season CurrentSeason { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        OnSecondChanging += () => { seconds++; };
        //read time and data from file
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        float time = 0;
        if (isTimeLeft)
        {
            time = timeLeftInMinutes*60;
            isTimeLeft = false;
        }
        else
        {
            time = realMinutesInDay * 60;
        }

        
        float currentTime = 0;
        while (currentTime < time)
        {
            //if (isNeedToBreakCycle)
            //{
            //    isNeedToBreakCycle = false;
            //    yield break;
            //}
            yield return new WaitForSeconds(1);
            currentTime++;
            OnSecondChanging?.Invoke();
        }
        Day++;
        yield return StartCoroutine(Timer());
    }
}

public enum Season
{
    Spring,
    Summer,
    Autman,
    Winter
}
