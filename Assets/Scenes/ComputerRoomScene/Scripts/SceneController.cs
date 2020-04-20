using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    
    public GameEvent pauseEvent;
    public GameEvent unpauseEvent;

    private bool paused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (paused) {
                Unpause();
            } else {
                Pause();
            }
        }
    }

    public void LoadGameScene() {
        SceneManager.LoadScene("ComputerRoomScene");
    }

    private void Pause() {
        paused = true;
        pauseEvent.Raise();
    }

    private void Unpause() {
        paused = false;
        unpauseEvent.Raise();
    }
}
