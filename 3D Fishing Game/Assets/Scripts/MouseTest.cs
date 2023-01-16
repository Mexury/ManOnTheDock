using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTest : MonoBehaviour
{
    public Vector3 screenPos;
    public Vector3 worldPos;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("new pos");
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
