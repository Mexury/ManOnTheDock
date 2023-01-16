using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private StateManager stateManager;
    public Rigidbody rb;
    public Vector3 destination;
    Vector3 lakePos = new Vector3(0f, 0f, -6f);
    public bool isDestroying = false;
    public SpriteRenderer spriteRenderer;
    public int price = 5;

    void Start()
    {
        destination = lakePos;
        stateManager = GetComponent<StateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDestroying)
        {
            Vector3 vect3 = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 0.1f);
            vect3.y = 0;

            transform.position = vect3;

            if (stateManager == null) return;

            switch (stateManager.currentState.name)
            {
                default:
                case "IDLE":
                    break;
                case "CHOOSING_DESTINATION":
                    if (stateManager.hasDestination)
                    {
                        stateManager.currentState = stateManager.states["IDLE"];
                        stateManager.hasDestination = false;
                    }
                    else
                    {
                        float randX = Random.Range(-2f, 2.1f);
                        destination = new Vector3(lakePos.x + randX, 0, lakePos.z + Random.Range(-2.1f, 2.1f));
                        spriteRenderer.flipX = randX > 0;
                        stateManager.hasDestination = true;
                    }
                    break;
                case "WANDERING":
                    //if (!stateManager.hasDestination)
                    //{
                    //    stateManager.currentState = stateManager.states["CHOOSING_DESTIONATION"];
                    //}
                    //transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 0.1f);
                    //rb.MovePosition(new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)));
                    break;
            }
        }
    }
}
