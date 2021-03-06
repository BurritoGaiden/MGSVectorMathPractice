﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public float xInput, zInput;
    public float walkSpeed = 2, runSpeed = 6;

    public bool left, up, right, down;

    CharacterController controller;
    public GameObject animRefObject;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<CharInventory>().weaponCoolDown != GetComponent<CharInventory>().weaponWaitTime)
            return;

        //Get Player Input
        InputLogic();
        
        //Turn the input into a "direction"
        Vector3 inputDir = new Vector3(xInput, 0, zInput);
        inputDir = inputDir.normalized;

        if (inputDir != Vector3.zero)
        {
            //Turn Character to face input direction
            transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
        }

        
        MovementLogic(inputDir);

 
	}

    void InputLogic() {
        if (left) {
            xInput = -1;
        } else if (right) {
            xInput = 1;
        } else xInput = 0;
        if (up)
        {
            zInput = 1;
        }
        else if (down)
        {
            zInput = -1;
        }
        else zInput = 0;
    }

    void MovementLogic(Vector3 inputDir) {
        bool running = Input.GetKey(KeyCode.LeftShift);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;

        //Move Character forward
        Vector3 velocity = transform.forward * speed;
        controller.Move(velocity * Time.deltaTime);
    }   
}
