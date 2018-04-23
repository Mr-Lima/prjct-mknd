using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Sigleton
    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Mais de um inventario");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public float carryCapacity = 1000f;
    float currentCapacity = 0;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(currentCapacity + item.weight > carryCapacity)
        {
            Debug.Log("Inventario cheio");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        currentCapacity += item.weight;
        return true;
    }

    public void Remove(Item item)
    {
        currentCapacity -= item.weight;
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
