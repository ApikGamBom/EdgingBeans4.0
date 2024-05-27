using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController_v2 : MonoBehaviour
{
    public float lookRadius = 4f;
    public float maxLookLength = 20f;

    Transform player;
    public GameObject playerObject;
    NavMeshAgent agent;
    public GameObject wallCheck;
    public float minDistanceToLastPlayer = 1.6f;

    public GameObject goal;
    public GameObject lastPlayerPos;
    public GameObject rayPivot;
    public bool seesPlayer;

    void Start ()
    {
        player = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
    if (!PauseMenu.isPaused)
    {
        if (seesPlayer == false) {
            agent.SetDestination(goal.transform.position);
        }
        RayPivotPointToPlayer();

        float playerDistance = Vector3.Distance(lastPlayerPos.transform.position, agent.transform.position);
        float distance = Vector3.Distance(player.position, agent.transform.position);
        if (playerDistance <= minDistanceToLastPlayer && distance >= 2) {
            seesPlayer = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(rayPivot.transform.position, rayPivot.transform.forward, out hit, maxLookLength))
        {
            if (hit.transform.gameObject == playerObject)
            {
                Debug.Log(hit.transform.gameObject + " SawPlayer! " + playerObject);
                SawPlayer();
            } else if (hit.transform.gameObject != playerObject && seesPlayer == false) {
                Debug.Log(hit.transform.gameObject + " Tried " + playerObject);
                GoForGoal();
            }
        }
        
        if (distance <= agent.stoppingDistance)
        {
            Debug.Log("Attacking " + playerObject.transform.name); //Attack player

            FacePlayer();
        }

    }
    }
    void FacePlayer()
    {
        Vector3 direction = (player.position - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void RayPivotPointToPlayer()
    {
        Vector3 direction = (player.position - rayPivot.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        rayPivot.transform.rotation = Quaternion.Slerp(rayPivot.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void SawPlayer()
    {
        float distance = Vector3.Distance(player.position, agent.transform.position);
        lastPlayerPos.transform.position = playerObject.transform.position;
        agent.SetDestination(lastPlayerPos.transform.position);
        seesPlayer = true;
    }
    void GoForGoal()
    {
        agent.SetDestination(goal.transform.position);
    }
}