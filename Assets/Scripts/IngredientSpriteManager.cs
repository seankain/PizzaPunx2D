using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class IngredientToSpriteTuple
{
    public PizzaIngredient.PizzaInredientType name;
    public Sprite sprite;
}

public class IngredientSpriteManager : MonoBehaviour
{
    public List<IngredientToSpriteTuple> ingredients;

    public Sprite GetSprite(PizzaIngredient.PizzaInredientType ingredient)
    {
        var i = ingredients.Where(a => a.name == ingredient).FirstOrDefault();
        if (i != null)
        {
            return i.sprite;
        }
        else
        {
            Debug.LogError("Ingredient not found?");
            return null;
        }
    }
}
