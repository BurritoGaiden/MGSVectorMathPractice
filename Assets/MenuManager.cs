using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameplayManager gm;

    public bool radioConversationAvailable;
    public bool inConvo;
    public Dictionary<float, float> radiosWithDialogue = new Dictionary<float, float>();

    public bool up, down, back, forth, left, right;

    public int gameMenuIndex;
    public int equipMenuIndex;
    public int weapMenuIndex;
    public float transceiverFreq;

    public TextAsset textFile;
    public List<string> textList;
    public int currentTextLine;

    public MenuState thisMenuState = MenuState.GameMenu;

    void Start() {
        gm = GetComponent<GameplayManager>();
    }

    void Update() {
        if (gm.GetComponent<GameplayManager>()){
            if (gm.pc && gm.thisPlayerControlState == PlayerControlState.Menu) { 
                weapMenuIndex = gm.pc.GetComponent<CharInventory>().weaponIndex;
                equipMenuIndex = gm.pc.GetComponent<CharInventory>().equipIndex;

                switch (thisMenuState) {
                    case MenuState.GameMenu:
                        GameMenuLogic();
                        break;
                    case MenuState.EquipMenu:
                        EquipmentMenuLogic();
                        break;
                    case MenuState.WeaponMenu:
                        WeaponMenuLogic();
                        break;
                    case MenuState.TransceiverMenu:
                        TransceiverMenuLogic();
                        break;
                }
                //WhichMenuCheck();
            }
        }
    }

    public void WhichMenuCheck() {
        switch (gm.thisPlayerControlState) {
            case PlayerControlState.EquipMenu:
                MenuManagement(ref gm.pc.GetComponent<CharInventory>().equipIndex, gm.pc.GetComponent<CharInventory>().equipments.Count);
                break;
            case PlayerControlState.Menu:
                MenuManagement(ref gameMenuIndex, 3);
                break;
            case PlayerControlState.WeaponMenu:
                MenuManagement(ref gm.pc.GetComponent<CharInventory>().weaponIndex, gm.pc.GetComponent<CharInventory>().weapons.Count);
                break;
        }
    }

    public void MenuManagement(ref int indexToBeModified, int maxIndexInt) {
        //state to be changed
        if (up)
        {
            if (indexToBeModified > 0) indexToBeModified--;
            else if (indexToBeModified == 0) indexToBeModified = maxIndexInt - 1;
        }
        else if (down)
        {
            if (indexToBeModified < maxIndexInt - 1) indexToBeModified++;
            else if (indexToBeModified == maxIndexInt - 1) indexToBeModified = 0;
        }

        //Backing out
        else if (back)
        {
            if (gm.thisPlayerControlState == PlayerControlState.Menu)
            {
                gm.thisPlayerControlState = PlayerControlState.Player;
            }
            else {
                gm.thisPlayerControlState = PlayerControlState.Menu;
            }
        }

        //Forthing
        else if (forth)
        {
            if (gm.thisPlayerControlState == PlayerControlState.Menu) {
                if (gameMenuIndex == 0)
                {
                    gm.thisPlayerControlState = PlayerControlState.WeaponMenu;
                }
                else if (gameMenuIndex == 1)
                {
                    gm.thisPlayerControlState = PlayerControlState.EquipMenu;
                }
                else if (gameMenuIndex == 2)
                {
                    gm.thisPlayerControlState = PlayerControlState.Radio;
                }
            }
        }
    }
    /*
    public void RadioMenuLogic()
    {
        if (!inConvo)
        {
            //Allow the Player to change the frequency
            if (Input.GetKeyDown(KeyCode.A))
            {
                radioValue -= .1f;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                radioValue += .1f;
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                for (int i = 0; i < radiosWithDialogue.Count; i++)
                {
                    if (radiosWithDialogue.ContainsKey(radioValue))
                    {
                        inConvo = true;
                        print(radiosWithDialogue[radioValue].ToString());
                        readTextFile(radiosWithDialogue[radioValue].ToString());
                        currentTextLine = 0;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                gm.thisPlayerControlState = PlayerControlState.Menu;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTextLine < (textList.Count - 1))
                {
                    currentTextLine++;

                }
                else if (currentTextLine == (textList.Count - 1))
                {
                    inConvo = false;
                    currentTextLine = 0;
                    textList = null;
                }
            }

        }        
    }
    */
    public void SwitchToRadioConversation(string whichFile)
    {
        readTextFile(whichFile);
        currentTextLine = 0;
    }

    public void readTextFile(string nameOfFileToRead)
    {
        textFile = Resources.Load(nameOfFileToRead) as TextAsset;
        textList = textFile.text.Split('\n').ToList();
    }

    public void GameMenuLogic() {
        //up moves the gameindex up
        //down moves the index down
        //U moves the game back to gameplay
        //I moves the game to the selected option

        if (up)
        {
            if (gameMenuIndex > 0) gameMenuIndex--;
            else if (gameMenuIndex == 0) gameMenuIndex = 2;
        }
        else if (down)
        {
            if (gameMenuIndex < 2) gameMenuIndex++;
            else if (gameMenuIndex == 2) gameMenuIndex = 0;
        }
        else if (left)
        {
            //tell game to go back to gameplay
        }
        else if (right) {
            //tell the game to go to whatever menu you have queued up
            ForwardMenuMove();
        }
    }

    public void ForwardMenuMove() {
        if (gameMenuIndex == 0) {
            thisMenuState = MenuState.WeaponMenu;
        } else if (gameMenuIndex == 1) {
            thisMenuState = MenuState.EquipMenu;
        } else if (gameMenuIndex == 2) {
            thisMenuState = MenuState.TransceiverMenu;
        }
    }

    public void BackwardMenuMove() {

    }

    public void WeaponMenuLogic() {
        //up moves the weaponindex up
        //down moves the weaponindex down
        //U moves the game back to the main menu
    }

    public void EquipmentMenuLogic() {
        //up moves the equipindexup
        //down moves the equipindexdown
        //U moves the game back to the main menu
    }

    public void TransceiverMenuLogic() {
        //left tunes the transceiver down
        //right turnes the transceiver up
        //up checks that frequency for a convo
        //U moves the game back to the main menu
    }
}

public enum MenuState{ GameMenu, EquipMenu, WeaponMenu, TransceiverMenu, Null}
public enum TransceiverMenuState { TransceiverNull, TransceiverSend, TransceiverReceiving}
