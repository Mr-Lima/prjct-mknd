using UnityEngine;
using UnityEngine.Networking;

//SCRIPT PARA DESABILITAR TD QUE NÃO FOR DO LOCAL PLAYER
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

    void Start()
    {
        //disable de todos os components da lista
        if (!isLocalPlayer)
        {
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else //disable da camera de cenario para local player
        {
            sceneCamera = Camera.main;
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
    }

    void OnDisable() //reativando camera caso logoff
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }

}
