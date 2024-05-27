using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractOilTank : MonoBehaviour
{
    public LayerMask interactableLayer; // The layer mask to filter interactable objects
    public TMP_Text promptText; // The UI text element for interaction prompts
    public float range = 5f; // The maximum range for raycasting

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; // Get the main camera
        promptText.gameObject.SetActive(false); // Hide the prompt initially
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, interactableLayer))
        {
            promptText.gameObject.SetActive(true); // Show the prompt
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact(); // Call the Interact method
                }
            }
        }
        else
        {
            promptText.gameObject.SetActive(false); // Hide the prompt
        }
    }
}
