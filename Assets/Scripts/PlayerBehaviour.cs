using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject aCamera;
    CharacterController controller;
    float speed = 4;
    float angularSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // connects controller to player's controller
    }

    // Update is called once per frame
    void Update()
    {
        float dx, dz;
        float rotationAboutY, rotationAboutX;

        // simple motion
        // transform.Translate(new Vector3(0,0,0.01f));
        // Input.GetAxis("Mouse Y") returns the distance of mouse from the center
        rotationAboutX = -Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        rotationAboutY = Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0,rotationAboutY, 0));
        // rotates only camera
        aCamera.transform.Rotate(new Vector3(rotationAboutX,0, 0));

        // Input.GetAxis("Horizontal") returns -1 for left arrow, 0 for none, 1 for right arrow
        dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 motion = new Vector3(dx, -1, dz); // In Local coordinates

        motion = transform.TransformDirection(motion); // transforms local coordinates to global coordinates

        controller.Move(motion); // in global coordinates
    }
}
