using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PizzaIngredient;

[Serializable]
public class IngredientTouple
{
    public PizzaInredientType key;
    public GameObject value;
}

public class IngredientSpawner : MonoBehaviour
{
    public float CheckFrequency = 1.0f;
    public List<IngredientTouple> prefabDict;


    PlacementSocket relatedSocket;
    float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        relatedSocket = GetComponent<PlacementSocket>();
        if (relatedSocket == null)
        {
            Debug.LogError("Placement soced needed on the ingredient spawner");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= CheckFrequency)
        {
            elapsed = 0f;
            SpawnIngredient();
        }
    }

    public void SpawnIngredient()
    {
        if (relatedSocket.OccupiedBy == null)
        {
            var thingToSpawn = PickThingToSpawn();
            if (thingToSpawn.HasValue)
            {
                var entries = prefabDict.Where(a => a.key == thingToSpawn).Count();

                if (entries != 0)
                {
                    var food = Instantiate(prefabDict.Where(a => a.key == thingToSpawn).First().value);
                    food.transform.position = transform.position;
                }
                else
                {
                    Debug.LogError($"Tried to spawn {thingToSpawn.ToString()} but it wasn't in the dictionary");
                }
            }
        }
    }

    public PizzaInredientType? PickThingToSpawn()
    {
        var allThings = FindObjectsOfType<PizzaIngredient>();
        
        var sauces = allThings.Where(a => a.Ingredient == PizzaInredientType.Sauce);

        var numSauce = allThings.Where(a => a.Ingredient == PizzaInredientType.Sauce).Count();
        if (numSauce == 0) return PizzaInredientType.Sauce;

        var numCheese = allThings.Where(a => a.Ingredient == PizzaInredientType.Cheese).Count();
        if (numCheese == 0) return PizzaInredientType.Cheese;

        return null;
    }
}
