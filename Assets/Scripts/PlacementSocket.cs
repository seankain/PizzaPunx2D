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

    public void Activate()
    {
        if (OnActivate != null) OnActivate.Invoke();
    }
}
