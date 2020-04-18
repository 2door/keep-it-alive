using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    // Update is called once per frame
    void Update() {
        Vector2 pos = transform.position;

        if(Input.GetKey("up") || Input.GetKey("w")) {
            Debug.Log("up");
            pos.y += speed * Time.deltaTime;
        }

        if(Input.GetKey("down") || Input.GetKey("s")) {
            Debug.Log("down");
            pos.y -= speed * Time.deltaTime;
        }

        if(Input.GetKey("left") || Input.GetKey("a")) {
            Debug.Log("left");
            pos.x -= speed * Time.deltaTime;
        }

        if(Input.GetKey("right") || Input.GetKey("d")) {
            Debug.Log("right");
            pos.x += speed * Time.deltaTime;
        }

        Debug.Log(pos);

        transform.position = pos;
    }
}
