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

    private GameObject[] spawners;
    private bool spawnLock = true;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);

        spawners = new GameObject[4];
        spawners[0] = Instantiate(enemySpawnPrefab, new Vector2(left, top), Quaternion.identity);
        spawners[1] = Instantiate(enemySpawnPrefab, new Vector2(right, top), Quaternion.identity);
        spawners[2] = Instantiate(enemySpawnPrefab, new Vector2(left, bot), Quaternion.identity);
        spawners[3] = Instantiate(enemySpawnPrefab, new Vector2(right, bot), Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        if (spawnLock && !gameOver) {
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn() {
        spawnLock = false;
        // foreach (GameObject spawnerInstance in spawners) {
        //     Vector3 randomness = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), 0f);
        //     Instantiate(enemyPrefab, spawnerInstance.transform.position + randomness, Quaternion.identity);
        // }

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
}
