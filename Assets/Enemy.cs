using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public NavMeshAgent pathfinder;
    public Transform target;
    public int roomIndex;
    public Vector3 startingPos;
    public Quaternion startingRot;
    public bool active;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startingPos = transform.position;
        startingRot = transform.rotation;
        pathfinder.SetDestination(transform.position);
        //active = false;
        //DisablePathfinder();
    }
	
	// Update is called once per frame
	void Update () {
        
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
