using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    new public string name = "New Item";
    public Sprite icon = null;
    public float weight;
    public bool isDefaultItem = false;


}
