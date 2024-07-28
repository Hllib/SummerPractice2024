using System.Collections.Generic;
using UnityEngine;

namespace HlStudio
{
    public static class Helpers
    {
        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }


        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary =
            new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }

        public static bool IsBetweenValues(float min, float max, float value, bool inclusive = false)
        {
            return inclusive ? value > min && value < max : value >= min && value <= max;
        }
    }
}