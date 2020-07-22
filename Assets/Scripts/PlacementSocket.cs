using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlacementSocketType { Ingredient, Pizza }

public class PlacementSocket : MonoBehaviour
{
    public PlacementSocketType SocketType = PlacementSocketType.Ingredient;
    public GameObject OccupiedBy;
    public UnityEvent OnActivate;
    public UnityEvent OnRelease;

    public void Release()
    {
        OccupiedBy = null;
        if (OnRelease != null) OnRelease.Invoke();
    }

    public void Activate()
    {
        if (OnActivate != null) OnActivate.Invoke();
    }

    public void Consume(float delay)
    {
        if (OccupiedBy != null)
        {
            Destroy(OccupiedBy, delay);
            OccupiedBy = null;
        }
    }
}
