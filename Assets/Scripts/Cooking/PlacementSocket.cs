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
    public UnityEvent OnContentArrive;

    public void Release()
    {
        OccupiedBy = null;
        if (OnRelease != null) OnRelease.Invoke();
    }

    public void Activate()
    {
        if (OnActivate != null) OnActivate.Invoke();
    }

    public virtual void ContentArrive(Holdable thingArriving)
    {
        if (OnContentArrive != null) OnContentArrive.Invoke();
    }

    public virtual void Consume(float delay)
    {
        if (OccupiedBy != null)
        {
            var i = OccupiedBy.GetComponent<Interactable>();
            i.isLocked = true;
            Destroy(OccupiedBy, delay);
            OccupiedBy = null;
        }
    }
}
