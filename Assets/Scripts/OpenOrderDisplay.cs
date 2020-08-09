using System.Collections.Generic;
using UnityEngine;

public class OpenOrderDisplay : MonoBehaviour
{
    GameManager gameManager;
    public GameObject OrderDisplayPrefab;
    public GameObject SpawnPoint;
    Dictionary<int, GameObject> OrderIcons;

    private void Start()
    {
        OrderIcons = new Dictionary<int, GameObject>();

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null) Debug.LogError("Game Manager is missing?");
    }

    public void AddOrder(int id)
    {
        var order = gameManager.orderManager.FindOrderById(id);
        var icon = Instantiate(OrderDisplayPrefab);
        icon.transform.position = SpawnPoint.transform.position;
        var ooi = icon.GetComponent<OpenOrderIcon>();
        ooi.SetOrder(order);
        OrderIcons.Add(id, icon);
    }

    public void RemoveOrder(int id)
    {
        if (OrderIcons.ContainsKey(id))
        {
            var go = OrderIcons[id];
            OrderIcons.Remove(id);
            Destroy(go);
        }
    }
}
