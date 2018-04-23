using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public float interactRadius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    public abstract void Action();

}
