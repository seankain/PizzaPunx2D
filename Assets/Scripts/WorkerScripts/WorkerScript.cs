using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerScript : MonoBehaviour
{
    private ParticleSystem myParticles;

    public PlacementSocket PizzaSocket;
    public PlacementSocket IngredientSocket;
    public int ToStage;

    bool isCurrentlyWorking = false;

    // Start is called before the first frame update
    void Start()
    {
        myParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void Activate()
    {
        if (PizzaSocket.OccupiedBy != null && IngredientSocket.OccupiedBy != null && isCurrentlyWorking == false)
        {
            var ingredient = IngredientSocket.OccupiedBy.GetComponentInChildren<PizzaIngredient>();

            if (myParticles) myParticles.Play();
            IngredientSocket.Consume(1.5f);
            StartCoroutine(ProgressPizzaCo(2.0f, ingredient.Ingredient));
        }
    }

    private IEnumerator ProgressPizzaCo(float wait, PizzaIngredient.PizzaInredientType ingredient)
    {
        var ps = PizzaSocket.OccupiedBy.GetComponentInChildren<Pizza>();
        if (ps == null) Debug.LogError("Pizza script missing?");
        var i = ps.GetComponent<Interactable>();
        isCurrentlyWorking = true;
        i.isLocked = true;

        yield return new WaitForSeconds(wait);

        ps.AddIngredient(ingredient);
        i.isLocked = false;
        isCurrentlyWorking = false;
        Activate(); // Try to activate again, in case an ingredient was placed while working
    }
}
