using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormatGameText
{


    public static string FormatValue(float value)
    {
        float valueToDisplay = 0f;
        string suffixe = "";

        if (value / 1000000000f >= 1f)
        {
            valueToDisplay = value / 1000000000f;
            suffixe = " B";
        }
        else if (value / 1000000 >= 1f)
        {
            valueToDisplay = value / 1000000f;
            suffixe = " M";
        }
        else if (value / 1000 >= 1f)
        {
            valueToDisplay = value / 1000f;
            suffixe = " K";
        }
        else
        {
            valueToDisplay = value;
        }

        return valueToDisplay.ToString("F0") + suffixe;
        /*
        if (valueToDisplay / 10f < 1)
            return valueToDisplay.ToString("F2") + suffixe;
        else if (valueToDisplay / 100f < 1)
            return valueToDisplay.ToString("F1") + suffixe;
        else
            return valueToDisplay.ToString("F0") + suffixe;
        */
    }


}
