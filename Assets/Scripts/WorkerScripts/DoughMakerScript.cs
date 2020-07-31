using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoughMakerScript : MonoBehaviour
{
    private ParticleSystem myParticles;

    public PlacementSocket PizzaSocket;
    public GameObject PizzaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myParticles = GetComponentInChildren<ParticleSystem>();
    }

    public void Activate()
    {
        if (PizzaSocket.OccupiedBy == null)
        {
            if (myParticles) myParticles.Play();
            StartCoroutine(MakeDoughCo(2.5f));
        }
    }

    private IEnumerator MakeDoughCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        var pizza = Instantiate(PizzaPrefab);
        pizza.transform.position = transform.position;
        // Pizza auto-drops on creation
    }
}
