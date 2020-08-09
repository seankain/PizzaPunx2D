using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pizza;

public class PizzaSpriteManager : MonoBehaviour
{
    public Sprite RawDoughSprite;

    public Sprite SaucedSprite;
    public Sprite BurnedSprite;
    public Sprite ToppedCheeseSprite;
    public Sprite ToppedVeggieSprite;
    public Sprite ToppedPepperoniSprite;
    public Sprite ToppedComboSprite;
    public Sprite ToppedWrongSprite;

    public Sprite BakedDoughSprite;
    public Sprite BakedSauceSprite;
    public Sprite BakedCheeseSprite;
    public Sprite BakedVeggieSprite;
    public Sprite BakedPepperoniSprite;
    public Sprite BakedComboSprite;
    public Sprite BakedWrongSprite;

    public Sprite GetPizzaSprite(List<PizzaIngredient.PizzaInredientType> ingredients, bool isCooked)
    {
        if (isCooked)
        {
            switch (Pizza.getPizzaType(ingredients))
            {
                case PizzaType.dough:
                    return BakedDoughSprite;

                case PizzaType.sauced:
                    return BakedSauceSprite;

                case PizzaType.cheese:
                    return BakedCheeseSprite;

                case PizzaType.veggie:
                    return BakedVeggieSprite;

                case PizzaType.meat:
                    return BakedPepperoniSprite;

                case PizzaType.combo:
                    return BakedComboSprite;

                case PizzaType.wrong:
                    return BakedWrongSprite;
            }
        }
        else
        {
            switch (getPizzaType(ingredients))
            {
                case PizzaType.dough:
                    return RawDoughSprite;

                case PizzaType.sauced:
                    return SaucedSprite;

                case PizzaType.cheese:
                    return ToppedCheeseSprite;

                case PizzaType.veggie:
                    return ToppedVeggieSprite;

                case PizzaType.meat:
                    return ToppedPepperoniSprite;

                case PizzaType.combo:
                    return ToppedComboSprite;

                case PizzaType.wrong:
                    return ToppedWrongSprite;
            }
        }

        return ToppedWrongSprite;
    }
}
