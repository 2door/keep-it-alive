using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public float projectileSpeed;
    public float fireRate;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public GameEvent gameOverEvent;

    private bool shootLock = true;
    private bool gameOver = false;

    void Start() {
        GameEventListener gameOverListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        gameOverListener.SetupListener(gameOverEvent, GameOver);
    }

    void Update() {
        if (Input.GetMouseButton(0) && shootLock && !gameOver) {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot() {
        shootLock = false;
        GameObject projectileInstance = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * projectileSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(fireRate);
        shootLock = true;
    }

    private void GameOver() {
        gameOver = true;
    }
}
