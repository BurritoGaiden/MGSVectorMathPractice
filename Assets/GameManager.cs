using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    [SerializeField]
    PlayerControlState thisPlayerControlState;

    public GameObject playerCharacter;

    public Text menuText;
    public float radioValue;
    public bool inConvo;
    //public static List<KeyValuePair<float, float>> radiosWithDialogue = new List<KeyValuePair<float, float>>();
    public Dictionary<float, float> radiosWithDialogue = new Dictionary<float, float>();

    public TextAsset textFile;
    public static List<string> textArray;
    public int currentTextLine;

    public int gameMenuIndex;

    public int equipMenuIndex;
    public int weaponMenuIndex;
    public static List<PlayerWeapon> playerWeapons = new List<PlayerWeapon>();
    public static List<PlayerEquipment> playerEquipments = new List<PlayerEquipment>();

    public PlayerAmmo thisPlayerAmmo = new PlayerAmmo(10, 30, 0, 3, 2);
    
    public bool radioConversationAvailable;

    public int currentLevel;

    public GameObject cam;

    public float weaponWaitTime;

    public int[] roomArray = new int[30];
    public int currentRoomIndex;

    //public Event 

	// Use this for initialization
	void Start () {
        //radiosWithDialogue = new Dictionary<float, float>();
        CharController.itemPickup += CheckItem;
        CharController.OnTrigger += LevelTransition;
        CamController.alertStatus += alert;

        playerEquipments.Add(new PlayerEquipment("NONE"));
        playerWeapons.Add(new PlayerWeapon("NONE", WeaponType.melee, 0));
        playerWeapons.Add(new PlayerWeapon("CQC", WeaponType.melee, 5));

        weaponWaitTime = 3;

        StartCoroutine(GameScript());
	}

    public void alert() {
        //Enemies in this room will attack the Player
        //This room will spawn enemies at all of its entrances while alert
        roomArray[currentRoomIndex] = 1;
    }

    public void roomManager() {

    }

    public void CheckItem(GameObject item) {
        thisPlayerControlState = PlayerControlState.Null;

        if (item.GetComponent<Ammo>())
        {
            menuText.text = "You picked up: " + item.GetComponent<Ammo>().amount + " " + item.GetComponent<Ammo>().type;
            if (item.GetComponent<Ammo>().type == "Nine")
                thisPlayerAmmo.Nine += item.GetComponent<Ammo>().amount;
            else if (item.GetComponent<Ammo>().type == "Tazes")
                thisPlayerAmmo.Tazes += item.GetComponent<Ammo>().amount;
            else if (item.GetComponent<Ammo>().type == "FiveFiveSix")
                thisPlayerAmmo.FiveFiveSix += item.GetComponent<Ammo>().amount;
            else if (item.GetComponent<Ammo>().type == "SevenSixTwo")
                thisPlayerAmmo.SevenSixTwo += item.GetComponent<Ammo>().amount;
        }
        else if (item.GetComponent<Weapon>())
        {
            playerWeapons.Add(new PlayerWeapon(item.GetComponent<Weapon>().type, WeaponType.ranged,12));
            menuText.text = "You picked up: " + item.GetComponent<Weapon>().type;
            print(playerWeapons[playerWeapons.Count - 1].name);
        }
        else if (item.GetComponent<Equipment>()) {
            playerEquipments.Add(new PlayerEquipment("Vest"));
            menuText.text = "You picked up: " + "Vest";
            print(playerEquipments[playerEquipments.Count - 1].name);
        }
        DisableItemAfterPickup(item);
        StartCoroutine(PlayPickupNotification());
    }

    public IEnumerator PlayPickupNotification() {
        playerCharacter.GetComponent<CharController>().left = false;
        playerCharacter.GetComponent<CharController>().right = false;
        playerCharacter.GetComponent<CharController>().up = false;
        playerCharacter.GetComponent<CharController>().down = false;
        yield return new WaitForSeconds(1.5f);
        thisPlayerControlState = PlayerControlState.Player;
        menuText.text = "";
    }

    void DisableItemAfterPickup(GameObject item)
    {
        item.GetComponent<Renderer>().enabled = false;
        item.GetComponent<Collider>().enabled = false;
    }

    public void AddWeapon(GameObject item)
    {
        if (item.GetComponent<Ammo>().type == "Nine")
        {
            print(thisPlayerAmmo.Nine);
            thisPlayerAmmo.Nine += item.GetComponent<Ammo>().amount;
            print(thisPlayerAmmo.Nine);
        }
    }

    public void LevelTransition(Vector3 camDest, Vector3 playerDest) {
        //print("level transition now");
        //print(transitionValue);

        cam.transform.position = camDest;
        playerCharacter.transform.position = playerDest;
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
        else if (thisPlayerControlState == PlayerControlState.Menu)
        {
            GameMenuLogic();
        }
        else if (thisPlayerControlState == PlayerControlState.WeaponMenu)
        {
            WeaponMenuLogic();
        }
        else if (thisPlayerControlState == PlayerControlState.EquipMenu) {
            EquipmentMenuLogic();
        }
	}

    public void WeaponMenuLogic() {
        //Changing Choice
        if (Input.GetKeyDown(KeyCode.W))
        {
            print(playerWeapons.Count);
            if (weaponMenuIndex > 0) weaponMenuIndex--;
            else if (weaponMenuIndex == 0) weaponMenuIndex = playerWeapons.Count - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            print(playerWeapons.Count);
            if (weaponMenuIndex < playerWeapons.Count - 1) weaponMenuIndex++;
            else if (weaponMenuIndex == playerWeapons.Count - 1) weaponMenuIndex = 0;
        }

        //Backing out
        else if (Input.GetKeyDown(KeyCode.U))
        {
            thisPlayerControlState = PlayerControlState.Menu;
        }

        menuText.text = "";
        for (int i = 0; i < playerWeapons.Count; i++) {
            menuText.text += playerWeapons[i].name;
            if (i == weaponMenuIndex)
                menuText.text += " X";
            menuText.text += "\n";
        }
    }

    public void EquipmentMenuLogic()
    {
        //Changing Choice
        if (Input.GetKeyDown(KeyCode.W))
        {
            print(playerEquipments.Count);
            if (equipMenuIndex > 0) equipMenuIndex--;
            else if (equipMenuIndex == 0) equipMenuIndex = playerEquipments.Count - 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            print(playerEquipments.Count);
            if (equipMenuIndex < playerEquipments.Count - 1) equipMenuIndex++;
            else if (equipMenuIndex == playerEquipments.Count - 1) equipMenuIndex = 0;
        }

        //Backing out
        else if (Input.GetKeyDown(KeyCode.U))
        {
            thisPlayerControlState = PlayerControlState.Menu;
        }

        menuText.text = "";
        for (int i = 0; i < playerEquipments.Count; i++)
        {
            menuText.text += playerEquipments[i].name;
            if (i == equipMenuIndex)
                menuText.text += " X";
            menuText.text += "\n";
        }
    }

    public void GameMenuLogic() {
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
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (gameMenuIndex == 0)
            {
                thisPlayerControlState = PlayerControlState.WeaponMenu;
            }
            else if (gameMenuIndex == 1) {
                thisPlayerControlState = PlayerControlState.EquipMenu;
            }
            else if (gameMenuIndex == 2)
            {
                thisPlayerControlState = PlayerControlState.Radio;
            }
        }
        string W, E, T;
        W = gameMenuIndex == 0 ? "X" : "";
        E = gameMenuIndex == 1 ? "X" : "";
        T = gameMenuIndex == 2 ? "X" : "";

        menuText.text = "Weapons " + W + "\n" + "Equip. " + E + "\n" + "Transc. " + T;
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
            else if (Input.GetKey(KeyCode.I))
            {
                if (weaponWaitTime == 3)
                {
                    WeaponAbility();
                    weaponWaitTime = 0;
                }
            }
            else if (Input.GetKey(KeyCode.O))
            {

            }
            if (weaponWaitTime < 3) {
                weaponWaitTime += Time.deltaTime;
                if (weaponWaitTime > 3) {
                    weaponWaitTime = 3;
                }
            }
        }

        menuText.text = "Current Weapon: " + playerWeapons[weaponMenuIndex].name + "\n" + "Current Equipment: " + playerEquipments[equipMenuIndex].name;
    }

    void WeaponAbility() {
        print("I did it");
    }

    void EquipmentEffect() {

    }

    public void RadioMenuLogic() {
        if (menuText != null)
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
                        
                    }
                    else if (currentTextLine == (textArray.Count - 1)) {
                        inConvo = false;
                        currentTextLine = 0;
                        textArray = null;
                    }
                }

            }
            string addString = "";
            if (inConvo)
                addString = textArray[currentTextLine];
            menuText.text = radioValue + "\n" + addString;
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

public enum AlertState {
    alert, guard
}

public enum PlayerControlState {
    Radio,Menu,Player, Null,EquipMenu,WeaponMenu, Notification
}

public class PlayerAmmo
{
    public float Nine;
    public float FiveFiveSix;
    public float SevenSixTwo;
    public float Grenade;
    public float Tazes;

    public PlayerAmmo(float x, float y, float z, float a, float b)
    {
        Nine = x;
        FiveFiveSix = y;
        SevenSixTwo = z;
        Grenade = a;
        Tazes = b;
    }
}

public enum WeaponType { melee, ranged };

public class PlayerWeapon {
    public string name;
    public WeaponType weaponType;
    public int damage;

    public PlayerWeapon(string dName, WeaponType dType, int dDamage) {
        name = dName;
        weaponType = dType;
        damage = dDamage;
    }
}

public class PlayerEquipment {
    public string name;

    public PlayerEquipment(string dName) {
        name = dName;
    }
}
