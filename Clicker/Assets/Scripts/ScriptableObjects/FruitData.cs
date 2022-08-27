using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitData : ScriptableObject
{
    private Dictionary<Freshness, int> freshnessPrice = new Dictionary<Freshness, int>();
    public int ID { get; private set; }
    public string Name { get; private set; }
    public PlantType PlantType { get; private set; }
    public Sprite Sprite { get; private set; }
    public int GetFruitsFreshnessPrice(Freshness freshness)
    {
        if (freshnessPrice.ContainsKey(freshness))
            return freshnessPrice[freshness];
        else
            throw new System.Exception("It`s not type of freshness like this");
    }
    public FruitData(int id, Sprite sprite, string plantName, int rottenPrice, int normalPrice, int freshPrice)
    {
        ID = id;
        Name = plantName;
        freshnessPrice.Add(Freshness.Rotten, rottenPrice);
        freshnessPrice.Add(Freshness.Normal, normalPrice);
        freshnessPrice.Add(Freshness.Fresh, freshPrice);
        Sprite = sprite;
    }
}
