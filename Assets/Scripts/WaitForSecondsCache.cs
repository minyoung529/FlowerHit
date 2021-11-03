using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal static class WaitForSecondsCache
{
    class FloatComparer : IEqualityComparer<float>
    {
        public bool Equals(float x, float y)
        {
            return x == y;
        }

        public int GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    private static readonly Dictionary<float, WaitForSeconds> timeInterval
        = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if(timeInterval.TryGetValue(seconds,out wfs))
        {
            timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        }

        return wfs;
    }
}
