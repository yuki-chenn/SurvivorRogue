using System;
using UnityEngine;

namespace Survivor.Utils
{
    class CommonUtil
    {
        public static string SecondsToTimeFormat(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
}
