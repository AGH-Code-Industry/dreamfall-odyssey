using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Application {
    public class StartupManager : MonoBehaviour {
        private void Start() {
            // Unity loads all gameObjects associated with this class and after that we go to the menu
            // Objects that we want to be persistent during entire application must use DontDestroyOnLoad()
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}