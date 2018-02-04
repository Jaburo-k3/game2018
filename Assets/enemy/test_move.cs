using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test_move : MonoBehaviour {
    /*
    public GameObject target;
    NavMeshAgent agent;
    int flame;


    void Start()
    {
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        agent.destination = target.transform.position;
        //agent.SetDestination(target.transform.position);
        Debug.Log(target.name);
    }

    void Update()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // NavMeshAgentに目的地をセット
            agent.destination = target.transform.position;
        }
    }
    */
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    void Update()
    {
        // Update destination if the target moves one unit
        /*
        if (Vector3.Distance(destination, target.position) > 0.1f)
        {
            destination = target.position;
            agent.destination = destination;
        }
        */
        destination = target.position;
        agent.destination = destination;
    }
}