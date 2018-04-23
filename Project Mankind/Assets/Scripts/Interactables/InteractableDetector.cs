using System;
using UnityEngine;

public class InteractableDetector : MonoBehaviour {

    public Camera tpCam;
    private float range = 3.6f;

    public void Detect(bool interacting)
    {
        RaycastHit interactInfo;
        if(Physics.Raycast(tpCam.transform.position, tpCam.transform.forward, out interactInfo, range))
        {
            Debug.DrawLine(tpCam.transform.position, interactInfo.transform.position, Color.green);
            Interactable interactable = interactInfo.transform.GetComponent<Interactable>();
            if(interactable != null)
            {   
                
                Debug.Log("Press F");
                if (interacting)
                {
                    interactable.Action();
                }
            }
        }

    }
}
