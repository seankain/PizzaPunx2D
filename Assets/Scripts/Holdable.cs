using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Holdable : MonoBehaviour
{
    public float TimeToSocket = 0.2f;
    public PlacementSocketType ItemType;
    public PlacementSocket CurrentSocket;
    public string HeldSortingLayer;
    public string DroppedSortingLayer;

    public UnityEvent OnPickup;
    public UnityEvent OnDrop;

    private SpriteRenderer ren;
    private GameManager gameManager;

    public void Start()
    {
        ren = GetComponentInChildren<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        Drop();
    }

    public void Pickup()
    {
        if (OnPickup != null) { OnPickup.Invoke(); }
        Debug.Log("Picked Up");
        CurrentSocket.Release();
        CurrentSocket = null;
        ren.sortingLayerName = HeldSortingLayer;
    }

    public void Drop()
    {
        if (OnDrop != null) { OnDrop.Invoke(); }
        Debug.Log("Dropped");

        var targetSocket = FindNearestSocket();
        if (targetSocket != null)
        {
            targetSocket.OccupiedBy = gameObject;
            targetSocket.Activate();
            CurrentSocket = targetSocket;
            CurrentSocket.Activate();
            ren.sortingLayerName = DroppedSortingLayer;
            StartCoroutine(DriftToSpotCo(targetSocket.transform.position));
        }
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

    private PlacementSocket FindNearestSocket()
    {
        PlacementSocket[] properSockets = gameManager.PizzaSockets.Where(a => a.OccupiedBy == null).ToArray();
        if (ItemType == PlacementSocketType.Ingredient) properSockets = gameManager.IngredientSockets.Where(a => a.OccupiedBy == null).ToArray();

        PlacementSocket currentSocket = null;
        var currentSocketDistance = float.MaxValue;

        for (var i = 0; i < properSockets.Length; i++)
        {
            var dist = Vector2.Distance(properSockets[i].transform.position, transform.position);
            if (dist < currentSocketDistance)
            {
                currentSocket = properSockets[i];
                currentSocketDistance = dist;
            }
        }

        return currentSocket;
    }
}
