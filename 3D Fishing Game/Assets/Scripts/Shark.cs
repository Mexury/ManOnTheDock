using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    // Public variables that will be set using the editor
    public GameObject target;
    public Vector3 destination = new Vector3(0, 0, -6);
    public GameObject[] fishes;
    public string state = "WANDERING";
    public SpriteRenderer spriteRenderer;
    public GameManager gameManager;

    void Start()
    {
        // We're setting these variables in the Start method, because we can make sure everything already initialized in the other scripts.
        gameManager = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find all fish currently in the game.
        StartCoroutine(FindFishes());
    }

    void Update()
    {
        // Move towards the destination, but set the y axis to 0, so the shark won't fly.
        // The destination will be set below, if it is not set, the destination will be at a default location.
        Vector3 vect3 = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 1f);
        vect3.y = 0;

        // Flip the sprite if the target is to the left or right.
        spriteRenderer.flipX = transform.position.x - destination.x > transform.position.x;

        // If the shark is NOT in the wandering state, it will move to the destination.
        // The wandering state is not being used though, so it will always be false.
        if (state != "WANDERING")
        {
            transform.position = vect3;
        }

        switch (state)
        {
            default:
                if (target == null)
                {
                    StartCoroutine(FindFishes());
                    return;
                }
                break;
            case "WANDERING":
                break;
            case "ATTACKING":
                if (target == null)
                {
                    // If there is no target while the shark is trying to attack, the shark will attempt to find another target.
                    StartCoroutine(FindFishes());
                    return;
                }
                // If there is a target and the shark's distance to the target is less than 0.1 unit, it will trigger the destination_reached state.
                if (Vector3.Distance(transform.position, destination) < 0.1f)
                {
                    state = "DESTINATION_REACHED";

                    // Then to make sure and also give some leeway, the shark checks again for distance to the target, but this time if it's less than 0.2 units.
                    if (Vector3.Distance(target.transform.position, destination) < 0.2f)
                    {
                        // If it's close enough it will do the following
                        // - Destroy the target GameObject
                        // - Set the target variable to null, so it can't be found.
                        // - Set the state to wandering
                        // - Reset its hunger
                        // - Add a shark kill (died) to the StatsManager.
                        // - Remove a fish from the Alive property in the StatsManager.
                        Destroy(target);
                        state = "WANDERING";
                        StartCoroutine(GetHungry());
                        target = null;
                        StatsManager.Died++;
                        StatsManager.Alive--;
                    }
                }

                break;
        }
    }
    
    // A coroutine that waits for 3 seconds before trying to find fishes to target again.
    IEnumerator GetHungry()
    {
        state = "HUNGRY";
        Debug.Log("Hungry!");
        yield return new WaitForSeconds(3f);
        StartCoroutine(FindFishes());
    }

    IEnumerator FindFishes()
    {
        // This will make sure there can't be multiple searches going on at the same time.
        StopCoroutine(FindFishes());
        // This will find every gameobject with the tag fish, thus making it a fish. (because there is nothing else with that tag.)
        fishes = GameObject.FindGameObjectsWithTag("Fish");

        // If the target exists, it will set the previous target's sprite renderer color to white, thus giving it no overlay to indicate it being attacked,
        // because it no longer is.
        if (target != null)
        {
            target.GetComponent<SpriteRenderer>().color = Color.white;
        }
        // If at least 1 fish was found in the search
        if (fishes.Length > 0)
        {
            // It will pick 1 random fish from the available fishes
            // It will add a red overlay, to indicate it being targeted.
            // The destination will get set so the shark knows where to go
            // And it will set its state to attacking so the update method knows what to do next.
            target = fishes[Random.Range(0, fishes.Length)];
            
            target.GetComponent<SpriteRenderer>().color = Color.red;
            
            destination = target.transform.position;
            state = "ATTACKING";
        }

        // If the shark is neither attacking nor wandering, it will attempt to find more fishes.
        if (state != "ATTACKING" && state != "WANDERING")
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(FindFishes());
        }
    }
}
