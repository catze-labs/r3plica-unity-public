using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    static bool _isDebugMode = false;

    public static void Log(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.Log(obj.ToString());
    }

    public static void LogWarning(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.LogWarning(obj.ToString());
    }

    public static void LogError(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.LogError(obj.ToString());
    }
}
