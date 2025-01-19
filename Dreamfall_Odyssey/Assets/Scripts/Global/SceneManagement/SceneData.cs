using CoinPackage.Debugging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.SceneManagement {
    [CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
    public class SceneData : ScriptableObject {
        [Header("World Data")]
        public string worldName;
        public string author;

        [Header("References")]
        public string sceneName;

        public override string ToString() {
            return $"{worldName}" % Colorize.Cyan;
        }
    }
}
