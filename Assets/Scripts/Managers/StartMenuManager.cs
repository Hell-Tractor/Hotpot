using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
