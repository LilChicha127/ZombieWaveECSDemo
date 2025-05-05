﻿using System;
using UnityEngine;

namespace InfimaGames.LowPolyShooterPack
{
    public static class Log
    {
        public static void kill(object toKill)
        {
            //Null check.
            if (toKill == null)
                toKill = "Null";

            string message = "You have been warned that: " + toKill;
            Internal_Log(message, LogType.Error);
        }

        public static void ReferenceError(MonoBehaviour behaviour, GameObject gameObject)
        {
           
        }
        private static void Internal_Log(string message, LogType type)
        {

        }
    }
}