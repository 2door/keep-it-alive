using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{

    public float top;
    public float bot;
    public float right;
    public float left;
    public float spawnRate;
    public float minRadius;
    public float maxRadius;
    public GameObject enemySpawnPrefab;
    public GameObject enemyPrefab;
    public GameEvent gameOverEvent;
    public GameEvent pauseEvent;
    public GameEvent unpauseEvent;

    private GameObject[] spawners;
    private bool spawnLock = true;
    private bool gameOver = false;
    public bool paused = false;

    // Start is called before the first frame update
    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
        GameEventListener pauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        pauseEventListener.SetupListener(pauseEvent, Pause);
        GameEventListener unpauseEventListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        unpauseEventListener.SetupListener(unpauseEvent, Unpause);
    }

    // Update is called once per frame
    void Update() {
        if (spawnLock && !gameOver && !paused) {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn() {
        spawnLock = false;

        //Pick a random direction.
        Vector3 direction = Vector3.zero;
        while (direction == Vector3.zero) {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }

        //Create you spawn Vector
        direction = direction.normalized;
        float magnitude = Random.Range(minRadius, maxRadius);
        Vector2 spawnVector = direction * magnitude;

        //Use Spawn Vector in relation to computer position to get spawn position.
        Vector2 spawnPosition = Vector2.zero + spawnVector;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        yield return new WaitForSeconds(spawnRate);
        spawnLock = true;
    }

    private void GameOver() {
        gameOver = true;
    }

    private void Pause() {
        paused = true;
    }

    private void Unpause() {
        paused = false;
    }
}
