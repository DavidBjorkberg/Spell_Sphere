using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCam;
    public LayerMask interactLayer;
    public Text interactText;

    void Update()
    {
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, 5, interactLayer))
        {
            interactText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<Interactable>().Interact();
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
