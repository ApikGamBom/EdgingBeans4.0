using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float interactableDistance = 1f;
    public LayerMask interactableMask;
    public Transform raySpawnPoint;
    public GameObject interactableMessage;

    void Start()
    {
        interactableMessage.SetActive(false);
    }

    void Update()
    {
        if (Physics.Raycast(raySpawnPoint.position, raySpawnPoint.forward, interactableDistance, interactableMask) && PlayerStats.oilCount > 0)
        {
            interactableMessage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (PlayerStats.oilCount > 0)
                {
                    PlayerStats.currentTankCount += PlayerStats.oilCount;
                    PlayerStats.oilCount = 0;
                }
            }
        } else
            interactableMessage.SetActive(false);
    }
}
