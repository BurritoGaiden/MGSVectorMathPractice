using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIManager : MonoBehaviour {

    GameplayManager gm;
    CharController pc;
    CharInventory ci;
    RoomManager rm;

    public Text menuText;

    public string gameplayDebug;
    public string menuDebug;
    public string equipmentDebug;
    public string weaponDebug;
    public string transceiverDebug;

    void Start()
    {
        GameObject managerObject = GameObject.Find("GameManager");
        print(managerObject);
        gm = managerObject.GetComponent<GameplayManager>();
        pc = gm.pc;
        ci = pc.GetComponent<CharInventory>();
        rm = pc.GetComponent<RoomManager>();
    }

    // Update is called once per frame
    void Update() {
        gameplayDebug = "Current Weapon: " + ci.weapons[ci.weaponIndex].name;
        gameplayDebug += "\n" + "Current Equipment: " + ci.equipments[ci.equipIndex].name;
        //gameplayDebug += "\n" + "Alert Status: " + rm.roomArray[rm.currentRoomIndex];

        //--------------------

        string W, E, T;
        W = gm.GetComponent<MenuManager>().gameMenuIndex == 0 ? "X" : "";
        E = gm.GetComponent<MenuManager>().gameMenuIndex == 1 ? "X" : "";
        T = gm.GetComponent<MenuManager>().gameMenuIndex == 2 ? "X" : "";

        menuDebug = "Weapons " + W + "\n" + "Equip. " + E + "\n" + "Transc. " + T;

        //--------------------

        weaponDebug = "";
        for (int i = 0; i < ci.weapons.Count; i++)
        {
            weaponDebug += ci.weapons[i].name;
            if (i == ci.weaponIndex)
                weaponDebug += " X";
            weaponDebug += "\n";
        }

        //--------------------

        equipmentDebug = "";
        for (int i = 0; i < ci.equipments.Count; i++)
        {
            equipmentDebug += ci.equipments[i].name;
            if (i == ci.equipIndex)
                equipmentDebug += " X";
            equipmentDebug += "\n";
        }

        //--------------------

        transceiverDebug = "Radio Freq: " + GetComponent<MenuManager>().radioValue;
        if (GetComponent<MenuManager>().inConvo) {
            transceiverDebug += "\n" + GetComponent<MenuManager>().textList[GetComponent<MenuManager>().currentTextLine];
        }

        //--------------------

        switch (gm.thisPlayerControlState) {
            case PlayerControlState.Menu:
                menuText.text = menuDebug;
                break;
            case PlayerControlState.Player:
                menuText.text = gameplayDebug;
                break;
            case PlayerControlState.EquipMenu:
                menuText.text = equipmentDebug;
                break;
            case PlayerControlState.WeaponMenu:
                menuText.text = weaponDebug;
                break;
            case PlayerControlState.Radio:
                menuText.text = transceiverDebug;
                break;
        }
    }
}
