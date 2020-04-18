using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuBehavior : MonoBehaviour
{

    public GameEvent gameOverEvent;
    public GameObject gameOverMenu;

    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
    }

    private void GameOver() {
        StartCoroutine(ShowMenu());
    }

    private IEnumerator ShowMenu() {
        yield return new WaitForSeconds(1);
        gameOverMenu.SetActive(true);
    }    
}
