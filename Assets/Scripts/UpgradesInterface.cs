using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesInterface : MonoBehaviour
{
    GameState currentGameState;

    public Toggle Swarm;
    public Toggle Dough;
    public Toggle Supply;
    public Toggle Topper1;
    public Toggle Topper2;
    public Toggle Topper3;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = FindObjectOfType<GameState>();

        if (currentGameState == null)
        {
            Debug.LogError("How did we make it this far without a game state???");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState)
        {
            if (currentGameState.HasSwarm == true) Swarm.isOn = true;
            else Swarm.isOn = false;

            if (currentGameState.HasDoughMachine == true) Dough.isOn = true;
            else Dough.isOn = false;

            if (currentGameState.HasSupplyBot == true) Supply.isOn = true;
            else Supply.isOn = false;

            if (currentGameState.NumToppersReplaced > 0) Topper1.isOn = true;
            else Topper1.isOn = false;

            if (currentGameState.NumToppersReplaced > 1) Topper2.isOn = true;
            else Topper2.isOn = false;

            if (currentGameState.NumToppersReplaced > 2) Topper3.isOn = true;
            else Topper3.isOn = false;
        }
    }
}
