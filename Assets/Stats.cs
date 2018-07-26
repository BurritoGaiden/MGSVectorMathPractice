using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage) {
        health -= damage;
        print(gameObject.name + " took " + damage + " damage");
        if (health <= 0) {
            print(gameObject.name + " has no more health. Dead.");
            gameObject.SetActive(false);
        }
    }
}
