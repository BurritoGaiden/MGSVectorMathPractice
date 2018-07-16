using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public AlertState[] roomArray = new AlertState[30];
    public GameObject[] objects;
    public int currentRoomIndex;

    public GameObject cam;

    public static List<Enemy> enemies = new List<Enemy>();

    public Dictionary<float, GameObject> importantObjects = new Dictionary<float, GameObject>();

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        RoomTriggering.GateTrigger += LevelTransition;
        enemies.Add(GameObject.Find("Enemy").GetComponent<Enemy>());
        CamController.alertStatus += alert;
    }

    public void LevelTransition(GameObject trigger)
    {
        if (roomArray[currentRoomIndex] == AlertState.alert)
        {
            roomArray[currentRoomIndex] = 0;
        }
        currentRoomIndex = trigger.GetComponent<TransitionScript>().destinationRoomIndex;
        cam.transform.position = trigger.GetComponent<TransitionScript>().cameraDestination;
        //playerCharacter.transform.position = trigger.GetComponent<TransitionScript>().playerDestination;

        for (int i = 0; i < enemies.Count; i++)
        {

            //enemies[i].DisablePathfinder();
            enemies[i].transform.position = enemies[i].startingPos;
            enemies[i].transform.rotation = enemies[i].startingRot;
            enemies[i].pathfinder.SetDestination(enemies[i].startingPos);

        }
    }


    void DisableItemAfterPickup(GameObject item)
    {
        item.GetComponent<Renderer>().enabled = false;
        item.GetComponent<Collider>().enabled = false;
    }

    //Have a function that unloads the current room
    void UnloadRoomObjects()
    {

    }

    //Have a function that loads the next room
    void LoadRoomObjects()
    {

    }
    //Have the function change everything in the room to the state it needs to be 
    void SetRoomToProperState()
    {

    }
    //Have a function that loads the Player into the room
    void SetPlayerInRoom()
    {

    }

    public void alert()
    {
        //Enemies in this room will attack the Player
        //This room will spawn enemies at all of its entrances while alert
        roomArray[currentRoomIndex] = AlertState.alert;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].roomIndex == currentRoomIndex)
            {
                enemies[i].pathfinder.SetDestination(enemies[i].target.position);
            }
        }
    }
}

public enum AlertState
{
    alert, guard
}


