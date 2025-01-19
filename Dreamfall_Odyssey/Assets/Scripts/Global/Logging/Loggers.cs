using System.Collections.Generic;
using CoinPackage.Debugging;

namespace Global.Logging {
    public static class Loggers {
        public static readonly Dictionary<LoggerType, CLogger> LoggersList;

        public enum LoggerType {
            UTILS,
            SCENE_SYSTEM
        }

        static Loggers() {
            LoggersList = new Dictionary<LoggerType, CLogger>();
            
            LoggersList.Add(
                LoggerType.UTILS,
                new CLogger(LoggerType.UTILS) {
                    LogEnabled = true
                }
            );
            
            LoggersList.Add(
                LoggerType.SCENE_SYSTEM,
                new CLogger(LoggerType.SCENE_SYSTEM) {
                    LogEnabled = true
                }
            );
        }
    }
}