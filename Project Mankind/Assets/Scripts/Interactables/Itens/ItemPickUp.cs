using UnityEngine;

public class ItemPickUp : Interactable {

    public Item item;

    public override void Action()
    {
        PickUp();
    }

    private void PickUp()
    {
        
        if (Inventory.instance.Add(item))
        {
            Debug.Log("Player pegou: " + item.name);
            Destroy(gameObject);
        }
       
    }

}
