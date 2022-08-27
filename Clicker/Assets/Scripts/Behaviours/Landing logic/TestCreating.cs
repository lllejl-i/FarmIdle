using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCreating : MonoBehaviour
{
    [Header("Test plant data")]
    [SerializeField]
    private int id;
    [SerializeField]
    string name;
    [SerializeField]
    int price;
    [SerializeField] 
    int type;
    [SerializeField]
    float growthTime;
    [SerializeField]
    float ripeningTime;
    [SerializeField]
    int fruitsPerSquareMeter;
    [SerializeField]
    int rottenPrice;
    [SerializeField]
    int normalPrice;
    [SerializeField]
    int freshPrice;

    [Header("Test greenhouse data")]
    [SerializeField]
    PlantType greenhouseType;
    [SerializeField]
    int plantStage;
    [SerializeField]
    float square;
    [SerializeField]
    float time;
    [SerializeField]
    bool isWorker; 
    [SerializeField]
    bool isAutoBuy; 
    [SerializeField]
    bool isActive;

    

    private void Start()
    {
        var plant = new PlantData(id, name, price, type, growthTime, ripeningTime, fruitsPerSquareMeter, rottenPrice, normalPrice, freshPrice);
        //Field.Instance.AddGreenHouse(plant, greenhouseType, square, time, true, false);
        Field.Instance.AddGreenHouse(plant, greenhouseType, square, time, DateTime.Now.AddHours(-1).ToString(), true, true);
        //Field.Instance.AddGreenHouse(plant, greenhouseType, square, time, false, false);
        //Field.Instance.AddGreenHouse(plant, greenhouseType, square, time, false, true);

    }
}
