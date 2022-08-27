using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlantData : ScriptableObject
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public int Price { get; private set; }
    public int FruitsPerSquareMeter { get; private set; }
    public float GrowthTime { get; private set; }//seconds
    public float RipeningTime { get; private set; }//seconds
    public PlantType Type { get; private set; }
    public FruitData Fruit { get; private set; }
    public List<Sprite> Sprites { get; private set; }
    public Animator Animator { get; private set; }

    public PlantData(int id, string name, int price, int type, float growthTime, float ripeningTime,
                     int fruitsPerSquareMeter, int rottenPrice, int normalPrice, int freshPrice)
    {
        ID = id;
        Name = name;
        Price = price;
        FruitsPerSquareMeter = fruitsPerSquareMeter;
        GrowthTime = growthTime;
        RipeningTime = ripeningTime;
        Type = (PlantType)type;
        Fruit = new FruitData(id, Resources.Load<Sprite>(Path.Combine(ResourcesPath.FruitsImages, name)), name, rottenPrice, normalPrice, freshPrice);
        Sprites = Resources.LoadAll<Sprite>(Path.Combine(ResourcesPath.PlantImages, name)).ToList();
        Animator = Resources.Load<Animator>(Path.Combine(ResourcesPath.PlantAnimators, name));
    }
}

public enum Freshness
{
    Rotten, 
    Normal,
    Fresh
}

public enum PlantType
{
    Mushroom,
    Berry,
    Greenery
}
