using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    [SerializeField]
    PlayerControlState thisPlayerControlState;

    public GameObject playerCharacter;

    public Text radioText;
    public float radioValue;
    public Text radioValueText;
    public bool inConvo;
    //public static List<KeyValuePair<float, float>> radiosWithDialogue = new List<KeyValuePair<float, float>>();
    public Dictionary<float, float> radiosWithDialogue;

    public TextAsset textFile;
    public static List<string> textArray;
    public int currentTextLine;

    public int gameMenuIndex;

    public bool radioConversationAvailable;

	// Use this for initialization
	void Start () {
        radiosWithDialogue = new Dictionary<float, float>();
        StartCoroutine(GameScript());        
	}

    public void SwitchToRadioConversation(string whichFile) {
        readTextFile(whichFile);
        currentTextLine = 0;

        thisPlayerControlState = PlayerControlState.Null;
    }

    public void readTextFile(string nameOfFileToRead) {
        textFile = Resources.Load(nameOfFileToRead) as TextAsset;
        textArray = textFile.text.Split('\n').ToList();
    }
	
	// Update is called once per frame
	void Update () {
        if (thisPlayerControlState == PlayerControlState.Player)
        {
            PlayerControlLogic();
        }
        else if (thisPlayerControlState == PlayerControlState.Radio)
        {
            RadioMenuLogic();
        }
        else if (thisPlayerControlState == PlayerControlState.Menu) {
            //Changing Choice
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (gameMenuIndex > 0) gameMenuIndex--;
                else if (gameMenuIndex == 0) gameMenuIndex = 2;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (gameMenuIndex < 2) gameMenuIndex++;
                else if (gameMenuIndex == 2) gameMenuIndex = 0;
            }

            //Backing out
            else if (Input.GetKeyDown(KeyCode.U))
            {
                thisPlayerControlState = PlayerControlState.Player;
            }
            
            //Choosing
            else if (Input.GetKeyDown(KeyCode.I)) {
                if (gameMenuIndex == 2) {
                    thisPlayerControlState = PlayerControlState.Radio;
                }
            }
        }
	}

    public void PlayerControlLogic() {
        if (playerCharacter != null)
        {
            playerCharacter.GetComponent<CharController>().left = Input.GetKey(KeyCode.A);
            playerCharacter.GetComponent<CharController>().right = Input.GetKey(KeyCode.D);
            playerCharacter.GetComponent<CharController>().up = Input.GetKey(KeyCode.W);
            playerCharacter.GetComponent<CharController>().down = Input.GetKey(KeyCode.S);
            if (Input.GetKeyDown(KeyCode.U))
            {
                playerCharacter.GetComponent<CharController>().left = false;
                playerCharacter.GetComponent<CharController>().right = false;
                playerCharacter.GetComponent<CharController>().up = false;
                playerCharacter.GetComponent<CharController>().down = false;
                thisPlayerControlState = PlayerControlState.Menu;
                gameMenuIndex = 0;
            }
        }
    }

    public void RadioMenuLogic() {
        if (radioText != null)
        {
            if (!inConvo)
            {
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
                            print("We'll see if the read made it");
                            currentTextLine = 0;
                            radioText.text = textArray[currentTextLine];
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.U)) {
                    thisPlayerControlState = PlayerControlState.Menu;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (currentTextLine < (textArray.Count - 1))
                    {
                        currentTextLine++;
                        radioText.text = textArray[currentTextLine];
                    }
                    else if (currentTextLine == (textArray.Count - 1)) {
                        inConvo = false;
                        currentTextLine = 0;
                        radioText.text = "";
                        textArray = null;
                    }
                }
            }
        }
    }

    IEnumerator GameScript() {
        thisPlayerControlState = PlayerControlState.Player;

        yield return new WaitForSeconds(3f);

        radioConversationAvailable = true;
        radiosWithDialogue.Add(120.85f, 01f);
        radioValue = 120.85f;

        while (true)
        {
            if(thisPlayerControlState == PlayerControlState.Radio && inConvo)
            { 
                if (currentTextLine == (textArray.Count - 1))
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        radiosWithDialogue.Remove(120.85f);
        radiosWithDialogue.Add(120.85f, 02f);

        while (true)
        {
            if (thisPlayerControlState == PlayerControlState.Radio && inConvo)
            {
                if (currentTextLine == (textArray.Count - 1))
                {
                    break;
                }
            }
            yield return new WaitForEndOfFrame();
        }

        print("done with the reminder");
    }
}

public enum PlayerControlState {
    Radio,Menu,Player, Null,EquipMenu,WeaponMenu
}
