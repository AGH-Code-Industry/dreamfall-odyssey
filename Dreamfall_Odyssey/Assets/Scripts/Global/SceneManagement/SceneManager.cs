using CoinPackage.Debugging;
using Global.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Global.SceneManagement {
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private SceneData[] scenes;
        private short _currentSceneIndex = -1;
        private bool _isLoadingScene;
        
        private readonly CLogger _logger = Loggers.LoggersList[Loggers.LoggerType.SCENE_SYSTEM];
        
        private void Awake() {
            _logger.Log($"Found {scenes.Length} scenes");
        }

        public void NextScene() {
            _logger.Log($"Next scene, current index: {_currentSceneIndex}");
            if (_isLoadingScene) {
                _logger.LogWarning("Scene is currently being loaded, skipping NextLevel call.");
                return;
            }

            if (_currentSceneIndex >= scenes.Length - 1) {
                _logger.LogWarning("No more scenes to load.");
                return;
            }
            
            _isLoadingScene = true;
            
            SceneData sceneToLoad = scenes[_currentSceneIndex + 1];
            _logger.Log($"Loading Scene {sceneToLoad}");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad.sceneName);

            _currentSceneIndex++;
            _isLoadingScene = false;
        }

        public void ReportReadiness(SceneData scene) {
            _logger.Log($"Scene {scene} is ready.");
        }
    }
}
