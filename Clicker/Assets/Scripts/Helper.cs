using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static string SecondsToMinutes(float seconds)
    {
        return $"{((int)seconds / 60 < 10 ? $"0{(int)seconds / 60}" : (int)seconds / 60)}:{(((int)seconds % 60 < 10) ? $"0{(int)seconds % 60}" : (int)seconds % 60)}";
    }
}
