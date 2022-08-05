using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesPath
{
    private static readonly string startFullPath = "Assets/Resources/";
    public static string Languages => "Languages";
    public static string LanguagesFullPath => startFullPath + Languages;
    public static string LangugesFileType => ".json";
}
