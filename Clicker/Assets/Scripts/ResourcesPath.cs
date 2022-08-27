using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesPath
{
    private static readonly string startFullPath = "Assets/Resources/";
    private static readonly string images = "Images/";
    
    public static string Languages => "Languages";
    public static string LangugesFileType => ".json";
    public static string PlantImages => images + "Plants";
    public static string FruitsImages => images + "Fruits";
    public static string GreenhousesImages => images + "Greenhouses";
    public static string PlantAnimators => "Animators/Plants";
}
