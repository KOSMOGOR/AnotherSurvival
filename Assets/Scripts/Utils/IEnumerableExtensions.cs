using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class IEnumerableExtensions
{
    public static T RandomSelectWeight<T>(this IEnumerable<T> values, Func<T, float> weightFunc)
    {
        float[] weights = values.Select(value => weightFunc.Invoke(value)).ToArray();
        if (weights.Length == 0) return default;
        float totalWeight = weights.Sum();
        float selectedWeight = Random.Range(0f, totalWeight);
        float checkedWeight = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            checkedWeight += weights[i];
            if (selectedWeight <= checkedWeight) return values.ElementAt(i);
        }
        return values.ElementAt(0);
    }
}