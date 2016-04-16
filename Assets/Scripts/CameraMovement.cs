using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed = 8.0f;
	
	void Update () {
	    if (Input.GetKey(KeyCode.W)) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
