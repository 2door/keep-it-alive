using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed;
    public float fireRate;

    private bool shootLock = true;

    void Update() {
        if (Input.GetMouseButton(0) && shootLock) {
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
}
