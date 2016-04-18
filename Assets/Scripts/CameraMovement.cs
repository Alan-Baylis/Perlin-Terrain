using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private float movementSpeed = 8.0f;
    private Vector3 previousMousePosition;
	
	void Update () {
	    if (Input.GetKey(KeyCode.W)) {
            transform.position = transform.position + transform.forward * movementSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position = transform.position - transform.forward * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position = transform.position + transform.right * movementSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position = transform.position - transform.right * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftAlt)) {
            float dx = Input.mousePosition.x - previousMousePosition.x;
            float dy = Input.mousePosition.y - previousMousePosition.y;
            if (dy > 5.0f) {
                float xRot = transform.rotation.eulerAngles.x - 1.0f;
                Vector3 newRot = new Vector3(xRot, transform.rotation.eulerAngles.y, 0.0f);
                transform.localRotation = Quaternion.Euler(newRot);
            } else if (dy < -5.0f) {
                float xRot = transform.rotation.eulerAngles.x + 1.0f;
                Vector3 newRot = new Vector3(xRot, transform.rotation.eulerAngles.y, 0.0f);
                transform.localRotation = Quaternion.Euler(newRot);
            }

            if (dx > 5.0f) {
                float yRot = transform.rotation.eulerAngles.y + 1.0f;
                Vector3 newRot = new Vector3(transform.rotation.eulerAngles.x, yRot, 0.0f);
                transform.localRotation = Quaternion.Euler(newRot);
            } else if (dx < -5.0f) {
                float yRot = transform.rotation.eulerAngles.y - 1.0f;
                Vector3 newRot = new Vector3(transform.rotation.eulerAngles.x, yRot, 0.0f);
                transform.localRotation = Quaternion.Euler(newRot);
            }

        }

        previousMousePosition = Input.mousePosition;
    }
}
