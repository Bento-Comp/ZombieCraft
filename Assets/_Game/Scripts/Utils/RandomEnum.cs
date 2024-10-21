using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomEnum
{
    public static T RandomEnumValue<T>()
    {
        System.Random _R = new System.Random();
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(_R.Next(v.Length));
    }
}
