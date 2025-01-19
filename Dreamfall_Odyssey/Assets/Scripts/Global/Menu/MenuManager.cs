using Global.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private Button startButton;

    void Awake() {
        SceneManager sceneManager = FindFirstObjectByType<SceneManager>();
        
        startButton.onClick.AddListener(() => {
            sceneManager.NextScene();
        });
    }
}
