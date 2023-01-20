using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Public variables
    private StateManager stateManager;
    public Rigidbody rb;
    public Vector3 destination;
    Vector3 lakePos = new Vector3(0f, 0f, -6f);
    public bool isDestroying = false;
    public SpriteRenderer spriteRenderer;
    public int price = 5;

    void Start()
    {
        // We set these variables to ensure that other scripts are initialized.
        destination = lakePos;
        stateManager = GetComponent<StateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // If the fish is not getting destroyed
        if (!isDestroying)
        {
            // Move the fish to the new destination but set the y axis to 0 so the fish doesn't fly away.
            Vector3 vect3 = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 0.1f);
            vect3.y = 0;

            transform.position = vect3;

            // If a statemanager can't be found the fish doesn't have behaviour.
            if (stateManager == null) return;

            switch (stateManager.currentState.name)
            {
                default:
                case "IDLE":
                    break;
                case "CHOOSING_DESTINATION":
                    // If there is a destination, it will remove it because it's choosing a new destination.
                    if (stateManager.hasDestination)
                    {
                        stateManager.currentState = stateManager.states["IDLE"];
                        stateManager.hasDestination = false;
                    }
                    else
                    {
                        // Go towards the lake +- a random value between -2 and 2 units.
                        // Also flip the sprite when needed.
                        // Set new destination
                        float randX = Random.Range(-2f, 2.1f);
                        destination = new Vector3(lakePos.x + randX, 0, lakePos.z + Random.Range(-2.1f, 2.1f));
                        spriteRenderer.flipX = randX > 0;
                        stateManager.hasDestination = true;
                    }
                    break;
            }
        }
    }
}
