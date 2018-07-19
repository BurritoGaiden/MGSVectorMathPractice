using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInventory : MonoBehaviour {

    public List<Equipment> equipments = new List<Equipment>();
    public int equipIndex;

    public List<Weapon> weapons = new List<Weapon>();
    public int weaponIndex;

    public Ammo ammo = new Ammo(0, 0, 0, 0, 0);
    public float weaponWaitTime;

    public bool weaponUse;
    

    // Use this for initialization
    void Start () {
        equipments.Add(new Equipment("NONE", EquipType.melee));
        weapons.Add(new Weapon("CQC", WeaponType.melee, WeaponKind.CQC, 5, AmmoType.Taze));
    }

    void Update() {
        for (int e = 0; e < equipments.Count; e++) {
            print(equipments[e].name);
        }

        for (int w = 0; w < weapons.Count; w++) {
            print(weapons[w].name);
        }

        UseWeapon();
    }

    public void UseEquipment() { }

    public void UseWeapon() {
        print("Weapon Used");
    }

    public void AddWeapon(GameObject item)
    {
        //determine which ammo type the item is
        //string itemAmmoType = item.GetComponent<AmmoObject>().itemAmmoType;
        //add that ammo to this ammo class
        //ammo.itemAmmoType = item.GetComponent<AmmoObject>().amount;
    }

    public void AddEquipment(GameObject item)
    {
        //determine which ammo type the item is
        //string itemAmmoType = item.GetComponent<AmmoObject>().itemAmmoType;
        //add that ammo to this ammo class
        //ammo.itemAmmoType = item.GetComponent<AmmoObject>().amount;
    }
}