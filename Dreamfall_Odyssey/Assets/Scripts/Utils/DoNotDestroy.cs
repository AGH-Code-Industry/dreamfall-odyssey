using System;
using System.Collections;
using System.Collections.Generic;
using CoinPackage.Debugging;
using Global.Logging;
using UnityEngine;

namespace Utils {
    public class DoNotDestroy : MonoBehaviour {
        private static readonly CLogger Logger = Loggers.LoggersList[Loggers.LoggerType.UTILS];
        private void Awake() {
            Logger.Log($"Object {gameObject % Colorize.Yellow} will not be destroyed on load.", gameObject);
            DontDestroyOnLoad(this);
        }
    }
}

