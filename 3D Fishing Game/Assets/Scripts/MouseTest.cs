using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTest : MonoBehaviour
{
    // Public variables
    public Vector3 screenPos;
    public Vector3 worldPos;
    public LayerMask layerMask;

    // This is called MouseTest.cs but it's really just a script for the rod positioning.
    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane + 1;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            worldPos = hit.point;
        }

        worldPos.y = -1.7f;
        transform.position = worldPos;
    }
}
