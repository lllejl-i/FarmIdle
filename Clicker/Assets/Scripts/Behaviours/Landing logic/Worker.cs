using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlantingBehaviour
{
    public class Worker
    {
        private List<Action> actionsToDo = new List<Action>();
        public List<Action> Tasks => actionsToDo.ToList();
        public void AddAction(Action action)
        {
            if (!actionsToDo.Contains(action))
            {
                actionsToDo.Add(action);
            }
        }

        public void RemoveAction(Action action)
        {
            if (!actionsToDo.Contains(action))
            {
                actionsToDo.Add(action);
            }
        }

        public (float timeToNext, float harvestTime, float speedTime) GetAfkItems(Plant plant, float square, float timeToHarvest,
                                DateTime time, float startTime, bool isAutoBuy, float harvestMultiplier = 1, float growingSpeedMultiplier = 1,
                                float harvestMultiplierTime = 0, float speedMultiplierTime = 0)
        {
            //var harvest = plant.HarvestData;
            //float itemsCount = 0;
            //var timline = DateTime.Now - time;
            //var realTime = timline.TotalSeconds;
            //var harvestTime = (startTime > 0) ? startTime : timeToHarvest;
            //float spentTime = 0;
            //bool isEnoughtMoney = true;
            //Action action = () =>
            //{
            //    spentTime = 0;
            //    if (speedMultiplierTime >= harvestTime / growingSpeedMultiplier && speedMultiplierTime > 0)
            //        spentTime = harvestTime / growingSpeedMultiplier;
            //    else
            //        spentTime = (harvestTime - speedMultiplierTime * growingSpeedMultiplier) + speedMultiplierTime;

            //    ///ToDo: when we add wallet it's need to add pay for auto buy greenery
            //    if (isAutoBuy /* && moneys >= plant.Price */ && plant.AfterHarvestIndex == -1)
            //    {
            //        Debug.Log("Buy plant");
            //        //money -= plant.Price;
            //    }
            //    else
            //        isEnoughtMoney = false;

            //    itemsCount += harvest.count * ((harvestMultiplierTime >= spentTime) ? harvestMultiplier : 1);
            //    harvestMultiplierTime = (harvestMultiplierTime >= spentTime) ? harvestMultiplierTime - spentTime : 0;
            //    speedMultiplierTime = (speedMultiplierTime >= spentTime) ? speedMultiplierTime - spentTime : 0;
            //    realTime -= spentTime;
            //    harvestTime = timeToHarvest;
            //    Debug.Log($"Real time:{realTime}");
            //};

            //if (isAutoBuy || plant.AfterHarvestIndex != -1)
            //    while (realTime >= 0)
            //    {
            //        action();

            //        if (realTime < spentTime || !isEnoughtMoney)
            //            break;
            //    }
            //else
            //    action();

            //harvest.count = (int)(itemsCount * square);
            /////ToDo: Add harvest to Ambar

            return (startTime, harvestMultiplierTime, speedMultiplierTime);
        }
    }
}

