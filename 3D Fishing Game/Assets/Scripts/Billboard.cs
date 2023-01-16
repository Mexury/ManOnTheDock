using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera Camera;

    private void Start()
    {
        Camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}