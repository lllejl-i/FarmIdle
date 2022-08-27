using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Field : MonoBehaviour
{
    private static Field instance;
    public static Field Instance => instance;

    [SerializeField]
    private Vector2 startPosition;
    [SerializeField]
    private Vector2 maxPosition;
    [SerializeField]
    private Vector2 positonOffset;
    [SerializeField]
    private Transform worldCoordsCanvas;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private GreenHouse greenHouseTemplate;
    private List<GreenHouse> houses = new List<GreenHouse>();
    private Vector2 lastPosition;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void AddGreenHouse(PlantType type, float square, bool isWorker, bool isActive, bool isAutoBuy)
    {
        var nextPosition = FindCurrentPosition();
        if (lastPosition.x != int.MinValue)
        {
            var timer = Instantiate(timeText, worldCoordsCanvas);
            timer.transform.position = lastPosition;
            var greenHouse = Instantiate(greenHouseTemplate);
            greenHouse.gameObject.transform.position = lastPosition;
            greenHouse.Initialize(type, square, timer, isActive, isAutoBuy);
            lastPosition = nextPosition;
        }
    }

    public void AddGreenHouse(PlantData plant, PlantType type, float square, float time, string lastData, bool isWorker, bool isAutoBuy)
    {
        var nextPosition = FindCurrentPosition();
        if (lastPosition.x != int.MinValue)
        {
            var timer = Instantiate(timeText, worldCoordsCanvas);
            timer.transform.position = lastPosition;
            var greenHouse = Instantiate(greenHouseTemplate);
            greenHouse.gameObject.transform.position = lastPosition;
            greenHouse.Initialize(plant, type, time, square, lastData, timer, isWorker, isAutoBuy);
            lastPosition = nextPosition;
        }
    }

    private Vector2 FindCurrentPosition()
    {
        if (lastPosition.x < maxPosition.x)
            return new Vector2(lastPosition.x + positonOffset.x, lastPosition.y);
        else if (lastPosition.y > maxPosition.y)
            return new Vector2(startPosition.x, lastPosition.y + positonOffset.y);
        else
            return new Vector2(int.MinValue, int.MinValue);
    }
}
