using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class enemyController_v3 : MonoBehaviour
{
    public float lookRadius = 4f;
    public float maxLookLength = 20f;

    Transform player;
    public GameObject playerObject;
    NavMeshAgent agent;
    public GameObject wallCheck;
    public float minDistanceToLastPlayer = 1.6f;
    public float maxDistance = 4f;

    public GameObject goal;
    public GameObject lastPlayerPos;
    public GameObject rayPivot;
    public bool seesPlayer;

    public int damage = 1;
    public float attackInterval = 1f;
    private bool isAttacking = false;

    void Start()
    {
        player = playerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (seesPlayer == false)
            {
                agent.SetDestination(goal.transform.position);
            }
            RayPivotPointToPlayer();

            float lastPlayerDistance = Vector3.Distance(lastPlayerPos.transform.position, agent.transform.position);
            float distance = Vector3.Distance(player.position, agent.transform.position);
            if (lastPlayerDistance <= minDistanceToLastPlayer && distance >= 6)
            {
                seesPlayer = false;
            }

            RaycastHit hit;
            if (Physics.Raycast(rayPivot.transform.position, rayPivot.transform.forward, out hit, maxLookLength))
            {
                if (hit.transform.gameObject == playerObject && distance <= maxDistance)
                {
                    Debug.Log(hit.transform.gameObject + " SawPlayer! " + playerObject);
                    SawPlayer();
                }
                else if (hit.transform.gameObject != playerObject && !seesPlayer && distance > maxDistance)
                {
                    Debug.Log(hit.transform.gameObject + " Tried " + playerObject);
                    GoForGoal();
                }
                else if (distance <= 4)
                {
                    GoForPlayer();
                }
            }

            if (distance <= agent.stoppingDistance)
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackPlayer());
                }
                FacePlayer();
            }
            else
            {
                if (isAttacking)
                {
                    StopCoroutine(AttackPlayer());
                    isAttacking = false;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(agent.transform.position, maxDistance);
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
        lastPlayerPos.transform.position = playerObject.transform.position;
        agent.SetDestination(lastPlayerPos.transform.position);
        seesPlayer = true;
    }

    void GoForGoal()
    {
        agent.SetDestination(goal.transform.position);
    }

    void GoForPlayer()
    {
        lastPlayerPos.transform.position = playerObject.transform.position;
        agent.SetDestination(player.transform.position);
    }

    public IEnumerator AttackPlayer()
    {
        isAttacking = true;
        while (true)
        {
            PlayerStats.health -= damage;
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
