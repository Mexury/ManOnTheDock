using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool doneWithState = true;
    public bool processingState = false;
    public Dictionary<string, State> states = new();
    public State currentState;
    public bool hasDestination = false;

    void Start()
    {
        states.Add("IDLE", new State("IDLE", 0f, 1f, 0f, 0f));
        states.Add("CHOOSING_DESTINATION", new State("CHOOSING_DESTINATION", 0f, 0f, 0f, 1f));
        //states.Add("WANDERING", new State("WANDERING", 1.5f, 6f, 0f, 1f));

        currentState = states["IDLE"];
    }

    public class State
    {
        public string name;
        public float minDuration = 4f;
        public float maxDuration = 10f;
        public float minDelay = 1f;
        public float maxDelay = 4f;

        public State(string name, float minDuration, float maxDuration, float minDelay, float maxDelay)
        {
            this.name = name;
            this.minDuration = minDuration;
            this.maxDuration = maxDuration;
            this.minDelay = minDelay;
            this.maxDelay = maxDelay;
        }

        public override string ToString()
        {
            return name;
        }
    }

    void Update()
    {
        if (!processingState && doneWithState)
        {
            ChooseRandomState();
        }
    }

    public void ChooseRandomState()
    {
        float randomDelay = UnityEngine.Random.Range(1f, 4f);
        float randomDuration = UnityEngine.Random.Range(4f, 10f);
        int randomNumber = UnityEngine.Random.Range(0, states.Count);
        State randomState = states.Values.ElementAt(randomNumber);

        StartCoroutine(ChangeState(randomState, randomDelay, randomDuration));
    }

    private IEnumerator ChangeState(State state, float delay, float duration)
    {
        processingState = true;

        yield return new WaitForSeconds(delay);
        currentState = state;
        processingState = false;
        //Debug.Log($"{delay}f | {duration}f | {state.name}");

        yield return new WaitForSeconds(duration);
        doneWithState = true;
    }
}
