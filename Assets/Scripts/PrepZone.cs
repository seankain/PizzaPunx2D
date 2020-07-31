using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrepZone : MonoBehaviour
{
    [SerializeField]
    private int stageNumber = 0;
    private List<PizzaIngredient> currentIngredients;
    private Pizza pizza;
    private bool passed = false;
    private List<GameObject> adulterants { get; set; }
    public List<PizzaIngredient.PizzaInredientType> RequiredIngredients;

    // Start is called before the first frame update
    void Awake()
    {
        RequiredIngredients = new List<PizzaIngredient.PizzaInredientType>();
        RequiredIngredients.Sort();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null) { return; }
        var ingredient = collision.gameObject.GetComponent<PizzaIngredient>();
        if (ingredient != null)
        {
            currentIngredients.Add(ingredient);
        }
        else
        {
            pizza = collision.gameObject.GetComponent<Pizza>();
            //not the pizza
            if (pizza == null)
            {
                adulterants.Add(collision.gameObject);
            }
        }
        if (pizza != null && !passed && HasRequiredIngredients())
        {
            Debug.Log($"Stage {stageNumber} passed.");
            pizza.ProgressStage(stageNumber);
            passed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null) { return; }
        var ingredient = collision.gameObject.GetComponent<PizzaIngredient>();
        if (ingredient == null)
        {
            pizza = collision.gameObject.GetComponent<Pizza>();
            //reset for next pizza
            if (pizza != null) { passed = false; }
            //Not pizza, not ingredient, is adulterant 
            else
            {
                adulterants.Remove(collision.gameObject);
            }
        }
    }

    private bool HasRequiredIngredients()
    {
        if (RequiredIngredients.Count == 0 && currentIngredients.Count == 0) return true;
        if (RequiredIngredients.Count != currentIngredients.Count) return false;

        var ingredients = currentIngredients.OrderBy(i => i.Ingredient).Select(i => i.Ingredient).ToArray();

        for (var i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != RequiredIngredients[i]) { return false; }
        }
        return true;
    }
}
