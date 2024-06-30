using UnityEngine;

class detectInteractable : MonoBehaviour
{

    public GameObject raySpawnPoint;
    public GameObject interactableUI;
    public LayerMask interactable;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(raySpawnPoint.transform.position, raySpawnPoint.transform.forward, out hit, interactable))
        {
            Debug.Log("hitted interactable!");
            if (InsideRadius.inRadius)
            {
                Debug.Log("Activating interactable UI!");
                interactableUI.SetActive(true);
            } else if (!InsideRadius.inRadius)
            {
                Debug.Log("Deactivatin ninteractable UI!");
                interactableUI.SetActive(false);
            }
        }
    }
}