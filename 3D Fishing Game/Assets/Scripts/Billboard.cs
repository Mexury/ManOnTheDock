using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera Camera;

    private void Start()
    {
        // Make sure the private camera variable is set
        Camera = Camera.main;
    }

    /*
     * We use the LateUpdate method to make sure every thing we do is after the camera or anything physics related has updated.
     * In this LateUpdate method, we give everything that has this script a rotation based on the camera, so it always looks towards the camera.
     * This is called a billboard sprite, hence the name of the script; "Billboard.cs"
     */
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}