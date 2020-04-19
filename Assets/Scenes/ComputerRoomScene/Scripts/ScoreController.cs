using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int enemyPointValue;
    public GameObject scoreDisplay;
    public GameObject gameOverScore;
    public GameEvent enemyDeathEvent;

    private int score;
    private GameEventListener enemyDeathListener;

    void Start() {
        score = 0;

        enemyDeathListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        enemyDeathListener.SetupListener(enemyDeathEvent, IncreaseScore);
    }

    private void IncreaseScore() {
        score += enemyPointValue;
        scoreDisplay.GetComponent<Text>().text = $"Score: {(int) score}";
        gameOverScore.GetComponent<Text>().text = $"Score: {(int) score}";
    }
}
