using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Sprite[] small;
    public Sprite[] large;

    bool isSpawningFish = false;
    public GameObject prefab;

    void Start()
    {
        StatsManager.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // Handles victory and loss states based on how many fish caught or dead
        if (StatsManager.Caught >= 50)
        {
            // Victory
            CustomSceneManager.GoToVictoryScreen();
        }
        if (StatsManager.Died >= 50)
        {
            // Loss
            CustomSceneManager.GoToLoseScreen();
        }

        // Set the score text in the game
        float newScore = StatsManager.Died == 0 ? StatsManager.Score : (StatsManager.Score * (StatsManager.Caught / StatsManager.Died));
        text.text = $"Fish: {StatsManager.Alive}\nCaught: {StatsManager.Caught}\n Dead: {StatsManager.Died}\nScore: {Mathf.CeilToInt(newScore) * 6}";

        // Check if you left click on a fish to catch it
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    if (raycastHit.transform.gameObject.CompareTag("Fish"))
                    {
                        // Update the statsmanager
                        Fish fish = raycastHit.transform.gameObject.GetComponent<Fish>();
                        StatsManager.Score += fish.price;
                        StatsManager.Alive--;
                        StatsManager.Caught += 1;

                        // Destroy fish
                        Destroy(raycastHit.transform.gameObject);
                    }
                }
            }
        }

        // DEBUG: Check if you right click on a fish to kill it (like the shark)
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    if (raycastHit.transform.gameObject.CompareTag("Fish"))
                    {
                        // Update the statsmanager
                        Fish fish = raycastHit.transform.gameObject.GetComponent<Fish>();
                        StatsManager.Alive--;
                        StatsManager.Died += 1;

                        // Destroy fish
                        Destroy(raycastHit.transform.gameObject);
                    }
                }
            }
        }

        if (!isSpawningFish)
        {
            // Spawns fish if not already
            StartCoroutine(SpawnFish());
        }
    }

    private IEnumerator SpawnFish()
    {
        // Spawn a random fish with random delay between spawning
        
        float delay = Random.Range(0.2f, 2f);
        isSpawningFish = true;

        // Randomize fish being large and or heavy for worth increase.
        bool isLarge = Random.Range(0, 101) <= 8;
        bool isHeavy = Random.Range(0, 101) == 22;
        
        float worth = 5f;
        worth *= isLarge ? 1.5f : 1f;
        worth *= isHeavy ? 1.2f : 1f;
        worth += isHeavy ? 2 : 0;
        worth += isLarge ? 3 : 1;

        yield return new WaitForSeconds(delay);

        isSpawningFish = false;

        if (StatsManager.Alive != 6)
        {
            Debug.Log("SPAWNED FISH");
            StatsManager.Alive++;
            //spawn code

            // Create a new fish and set the price which was calculated above.
            GameObject fish = Instantiate(prefab, new Vector3(0, 0, -6f), Quaternion.identity);
            Fish _fish = fish.GetComponent<Fish>();
            _fish.price = Mathf.CeilToInt(worth);

            // Spawn at a random position
            fish.transform.position = new Vector3(0 + Random.Range(-1.5f, 1.51f), 0, -6f + Random.Range(-1.5f, 1.51f));

            // Randomly decide which sprite it picks based on random values and also values picked above.
            fish.GetComponent<SpriteRenderer>().sprite = isLarge ? large[Random.Range(0, large.Length)] : small[Random.Range(0, small.Length)];

        }
    }
}
