using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public delegate void ItemTrigger(GameObject Item);
    public static event ItemTrigger itemTriggered;

    void Start()
    {
        ItemPickup.itemTriggered += CheckItem;
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<Ammo>() || other.GetComponent<Weapon>() || other.GetComponent<Equipment>())
        //{
            itemTriggered(other.gameObject);
        //}
    }

    public void CheckItem(GameObject item)
    {
        //thisPlayerControlState = PlayerControlState.Null;
        /*
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
            playerWeapons.Add(new PlayerWeapon(item.GetComponent<Weapon>().type, WeaponType.ranged, 12));
            menuText.text = "You picked up: " + item.GetComponent<Weapon>().type;
            print(playerWeapons[playerWeapons.Count - 1].name);
        }
        else if (item.GetComponent<Equipment>())
        {
            playerEquipments.Add(new PlayerEquipment("Vest"));
            menuText.text = "You picked up: " + "Vest";
            print(playerEquipments[playerEquipments.Count - 1].name);
        }
        DisableItemAfterPickup(item);
        StartCoroutine(PlayPickupNotification());
        */
    }
}
