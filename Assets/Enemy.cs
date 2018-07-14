using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent pathfinder;
    public Transform target;
    public int roomIndex;
    public Vector3 startingPos;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startingPos = transform.position;
        DisablePathfinder();
    }
	
	// Update is called once per frame
	void Update () {
        pathfinder.SetDestination(target.position);       
	}

    public void EnablePathfinder() {
        pathfinder.updatePosition = true;
        pathfinder.updateRotation = true;
    }

    public void DisablePathfinder()
    {
        pathfinder.updatePosition = false;
        pathfinder.updateRotation = false;
    }
}
