using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Holdable : MonoBehaviour
{
    public UnityEvent OnPickup;
    public UnityEvent OnDrop;
    public float TimeToSocket = 0.2f;

    public void Pickup()
    {
        if (OnPickup != null) OnPickup.Invoke();
        Debug.Log("Picked Up");
    }

    public void Drop(PlacementSocket[] potentialPlaces)
    {
        if (OnDrop != null) OnDrop.Invoke();
        Debug.Log("Dropped");

        var targetSocket = FindNearestSocket(potentialPlaces);
        StartCoroutine(DriftToSpotCo(targetSocket.transform.position));
    }

    IEnumerator DriftToSpotCo(Vector3 targetPosition)
    {
        var elapsed = 0f;
        var startPosition = transform.position;

        while (elapsed < TimeToSocket)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / TimeToSocket);
            yield return null;
        }

        transform.position = targetPosition;
    }

    private PlacementSocket FindNearestSocket(PlacementSocket[] potentialPlaces)
    {
        PlacementSocket currentSocket = null;
        var currentSocketDistance = float.MaxValue;

        for (var i = 0; i < potentialPlaces.Length; i++)
        {
            var dist = Vector2.Distance(potentialPlaces[i].transform.position, transform.position);
            if (dist < currentSocketDistance)
            {
                currentSocket = potentialPlaces[i];
                currentSocketDistance = dist;
            }
        }

        return currentSocket;
    }
}
