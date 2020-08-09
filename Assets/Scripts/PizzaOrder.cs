using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaOrder
{
    public float waitTime = 0f;
    public int orderId = 0;
    public bool isOrderComplete = false;
    public int moneyEarned = 0;
    public List<PizzaIngredient.PizzaInredientType> ingredients = new List<PizzaIngredient.PizzaInredientType>();

    public bool Match(List<PizzaIngredient.PizzaInredientType> otherIngredients)
    {
        if (ingredients == null || otherIngredients == null)
        {
            Debug.LogError("Ingredients are null?");
            return false;
        }

        if (otherIngredients.Count != ingredients.Count) return false;

        foreach (var i in otherIngredients)
        {
            if (ingredients.Contains(i) == false) return false;
        }

        return true;
    }
}
