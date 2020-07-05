using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlacementSocket : MonoBehaviour
{
    public UnityEvent OnActivate;

    public void Activate()
    {
        if (OnActivate != null) OnActivate.Invoke();
    }
}
