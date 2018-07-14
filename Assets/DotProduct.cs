using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProduct : MonoBehaviour {
    /*
    public Transform enemy;
    float dotProduct;
    MoveEnemy moveEnemy;
    Vector3 vectorToEnemy;
    float inRange = 2.0f;

    // Use this for initialization
    void Start () {
        moveEnemy = enemy.GetComponent<MoveEnemy>();
	}
	
	// Update is called once per frame
	void Update () {
        CalculateDotProduct();
        CheckForAttack();
	}

    void CalculateDotProduct()
    {
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.blue);

        vectorToEnemy = enemy.position - transform.position;
        Debug.DrawRay(transform.position, vectorToEnemy * 20f, Color.red);

        dotProduct = Mathf.Round(Vector3.Dot(transform.forward.normalized, vectorToEnemy.normalized));
        Debug.Log(dotProduct);

        if (dotProduct == 0) //ENEMY IS BESIDE THE PLAYER
        {

        }
        else if (dotProduct == 1) //enemy is IN FRONT of the player
        {
            if (moveEnemy.playerLookingAway != false)
            {
                moveEnemy.playerLookingAway = false;
            }

        }
        else if (dotProduct == -1) //enemy is BEHIND the player
        {
            if (moveEnemy.playerLookingAway != true)
            {
                moveEnemy.playerLookingAway = true;
            }
        }
    }

    void CheckForAttack()
    {
        float vectorToEnemeySqr = vectorToEnemy.sqrMagnitude;

        if(vectorToEnemeySqr <= inRange * inRange)
        {
            int dot = Mathf.RoundToInt(dotProduct);

            switch(dot)
            {
                case 0: //ENEMY IS BESIDE
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        moveEnemy.TakeDamage(10);
                    }
                   
                    break;

                case 1: //ENEMY IS IN FRONT
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        moveEnemy.TakeDamage(80);
                    }
                   
                    break;

                case -1: //ENEMY IS BEHIMD

                    break;
            }
        }
			
    }
    */
}
