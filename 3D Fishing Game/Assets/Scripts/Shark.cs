using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public GameObject target;
    public Vector3 destination = new Vector3(0, 0, -6);
    public GameObject[] fishes;
    public string state = "WANDERING";
    public SpriteRenderer spriteRenderer;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FindFishes());
    }

    void Update()
    {
        // move to destination
        // check if close enough to destination
        // check if the fish has been caught, if so, choose another fish
        // if not, choose another fish

        Vector3 vect3 = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 1f);
        vect3.y = 0;

        spriteRenderer.flipX = transform.position.x - destination.x > transform.position.x;

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
                    StartCoroutine(FindFishes());
                    return;
                }
                if (Vector3.Distance(transform.position, destination) < 0.1f)
                {
                    // close enough
                    state = "DESTINATION_REACHED";
                    if (Vector3.Distance(target.transform.position, destination) < 0.2f)
                    {
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

    IEnumerator GetHungry()
    {
        state = "HUNGRY";
        Debug.Log("Hungry!");
        yield return new WaitForSeconds(3f);
        StartCoroutine(FindFishes());
    }

    IEnumerator FindFishes()
    {
        StopCoroutine(FindFishes());
        fishes = GameObject.FindGameObjectsWithTag("Fish");

        if (target != null)
        {
            target.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (fishes.Length > 0)
        {
            // can choose fish
            target = fishes[Random.Range(0, fishes.Length)];
            target.GetComponent<SpriteRenderer>().color = Color.red;
            destination = target.transform.position;
            state = "ATTACKING";
        }

        if (state != "ATTACKING" && state != "WANDERING")
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(FindFishes());
        }
    }
}
