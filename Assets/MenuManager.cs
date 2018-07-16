using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameplayManager gm;

    public bool radioConversationAvailable;
    public float radioValue;
    public bool inConvo;
    public Dictionary<float, float> radiosWithDialogue = new Dictionary<float, float>();

    public bool up, down, back, forth, left, right;

    public int gameMenuIndex;
    public int equipMenuIndex;
    public int weapMenuIndex;

    public TextAsset textFile;
    public List<string> textList;
    public int currentTextLine;

    void Start() {
        gm = GetComponent<GameplayManager>();
    }

    void Update() {
        weapMenuIndex = gm.pc.GetComponent<CharInventory>().weaponIndex;
        equipMenuIndex = gm.pc.GetComponent<CharInventory>().equipIndex;

        WhichMenuCheck();
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
}
