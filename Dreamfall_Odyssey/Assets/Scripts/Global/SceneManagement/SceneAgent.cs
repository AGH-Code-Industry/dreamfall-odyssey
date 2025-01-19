using System;
using CoinPackage.Debugging;
using Global.Logging;
using UnityEngine;
using Colorize = CoinPackage.Debugging.Colorize;

namespace Global.SceneManagement {
    public class SceneAgent : MonoBehaviour {
        [SerializeField] private GameObject sceneContent;
        [SerializeField] private SceneData sceneData;

        private SceneManager _sceneManager;

        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.SCENE_SYSTEM];

        private void Awake() {
            _logger.Log($"Scene agent waking up: {this}");
            
            _sceneManager = FindFirstObjectByType<SceneManager>();
            
            // Checks
            if (!sceneContent || !sceneContent) {
                _logger.LogError($"Scene content or data not found: {this}");
                throw new Exception($"Scene content or data not found: {this}");
            }
            
            if (!_sceneManager) {
                throw new Exception($"Scene manager not found: {this}");
            }

            _sceneManager.ReportReadiness(sceneData);
        }

        // public void Activate() {
        //     sceneContent.SetActive(true);
        // }
        //
        // public void Deactivate() {
        //     sceneContent.SetActive(false);
        // }

        public override string ToString() {
            return $"[SceneAgent({sceneData})]" % Colorize.Cyan;
        }
    }
}
