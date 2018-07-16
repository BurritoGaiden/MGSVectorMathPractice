using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameplayManager : MonoBehaviour {

    [SerializeField]
    public PlayerControlState thisPlayerControlState;

    public CharController pc;
    public MenuManager mm;
    public DebugUIManager ui;
    public RoomManager rm;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.Find("Snake").GetComponent<CharController>();
        mm = GetComponent<MenuManager>();
        ui = GetComponent<DebugUIManager>();
        rm = GetComponent<RoomManager>();
        StartCoroutine(GameScript());
    }

    // Update is called once per frame
    void Update()
    {
        switch (thisPlayerControlState)
        {
            case PlayerControlState.Player:
                PlayerControlLogic();

                if (Input.GetKeyUp(KeyCode.U)) {
                    SwitchToGameMenu();
                }
                break;
            case PlayerControlState.Menu:
                GameMenuLogic();

                if (Input.GetKeyUp(KeyCode.U)) {
                    if (thisPlayerControlState == PlayerControlState.Menu)
                    {
                        SwitchToGamePlay();
                    }
                    else if (thisPlayerControlState == PlayerControlState.EquipMenu || thisPlayerControlState == PlayerControlState.WeaponMenu) {
                        SwitchToGameMenu();
                    }
                }
                break;
            case PlayerControlState.Radio:
                GameMenuLogic();

                if (Input.GetKey(KeyCode.U)) {
                    SwitchToGameMenu();
                }
                break;
        }
    }

    public void GameMenuLogic()
    {
        mm.up = Input.GetKeyUp(KeyCode.W);
        mm.down = Input.GetKeyUp(KeyCode.S);
        mm.back = Input.GetKeyUp(KeyCode.U);
        mm.forth = Input.GetKeyUp(KeyCode.I);
        mm.left = Input.GetKey(KeyCode.A);
        mm.right = Input.GetKey(KeyCode.D);
    }

    public void PlayerControlLogic()
    {
        pc.up = Input.GetKey(KeyCode.W);
        pc.down = Input.GetKey(KeyCode.S);
        pc.left = Input.GetKey(KeyCode.A);
        pc.right = Input.GetKey(KeyCode.D);
        pc.GetComponent<CharInventory>().weaponUse = Input.GetKey(KeyCode.I);
    }   

    public void SwitchToGameMenu()
    {
        pc.left = false;
        pc.right = false;
        pc.up = false;
        pc.down = false;
        mm.left = false;
        mm.right = false;
        mm.up = false;
        mm.down = false;
        mm.back = false;
        mm.forth = false;
        thisPlayerControlState = PlayerControlState.Menu;
    }

    public void SwitchToGamePlay()
    {
        pc.left = false;
        pc.right = false;
        pc.up = false;
        pc.down = false;
        mm.left = false;
        mm.right = false;
        mm.up = false;
        mm.down = false;
        mm.back = false;
        mm.forth = false;
        thisPlayerControlState = PlayerControlState.Player;
    }

    IEnumerator GameScript()
    {
        pc.GetComponent<CharInventory>().weapons.Add(new Weapon("SOCOM MK2", WeaponType.ranged, WeaponKind.USP, 5, AmmoType.Nine));
        thisPlayerControlState = PlayerControlState.Player;

        yield return new WaitForSeconds(3f);

        mm.radioConversationAvailable = true;
        mm.radiosWithDialogue.Add(120.85f, 01f);
        mm.radioValue = 120.85f;

        while (true)
        {
            if (thisPlayerControlState == PlayerControlState.Radio && GetComponent<MenuManager>().inConvo)
            {
                if (mm.currentTextLine == (mm.textList.Count - 1))
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        mm.radiosWithDialogue.Remove(120.85f);
        mm.radiosWithDialogue.Add(120.85f, 02f);

        while (true)
        {
            if (thisPlayerControlState == PlayerControlState.Radio && mm.inConvo)
            {
                if (mm.currentTextLine == (mm.textList.Count - 1))
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

public enum PlayerControlState
{
    Radio, Menu, Player, Null, EquipMenu, WeaponMenu, Notification
}
