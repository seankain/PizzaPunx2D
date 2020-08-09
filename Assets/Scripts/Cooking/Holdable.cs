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
    public PlacementSocket preferredSocket;
    public string HeldSortingLayer;
    public string DroppedSortingLayer;

    public UnityEvent OnPickup;
    public UnityEvent OnDrop;

    private SpriteRenderer ren;
    private GameManager gameManager;
    

    public void Awake()
    {
        ren = GetComponentInChildren<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Start()
    {
        Drop();
    }

    public void SetPreferredSocket(PlacementSocket s)
    {
        preferredSocket = s;
    }

    public void Pickup()
    {
        if (OnPickup != null) { OnPickup.Invoke(); }
        CurrentSocket.Release();
        CurrentSocket = null;
        ren.sortingLayerName = HeldSortingLayer;
    }

    public void Drop()
    {
        if (OnDrop != null) { OnDrop.Invoke(); }

        var targetSocket = FindNearestSocket();
        if (preferredSocket != null && preferredSocket.OccupiedBy == null)
        {
            targetSocket = preferredSocket;
            preferredSocket = null;
        }

        if (targetSocket != null)
        {
            targetSocket.OccupiedBy = gameObject;
            targetSocket.Activate();
            CurrentSocket = targetSocket;
            CurrentSocket.Activate();
            ren.sortingLayerName = DroppedSortingLayer;
            StartCoroutine(DriftToSpotCo(targetSocket));
        }
    }

    IEnumerator DriftToSpotCo(PlacementSocket targetSocket)
    {
        var elapsed = 0f;
        var startPosition = transform.position;

        while (elapsed < TimeToSocket)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetSocket.transform.position, elapsed / TimeToSocket);
            yield return null;
        }

        transform.position = targetSocket.transform.position;
        targetSocket.ContentArrive(this);
    }

    private PlacementSocket FindNearestSocket()
    {
        var sockets = gameManager.PizzaSockets.Where(a => a.OccupiedBy == null);
        if (sockets.Count() == 0)
        {
            Debug.LogWarning("No sockets!");
            return null;
        }

        PlacementSocket[] properSockets = sockets.ToArray();
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
