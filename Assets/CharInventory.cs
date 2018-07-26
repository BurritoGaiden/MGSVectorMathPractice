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

    public float weaponCoolDown;

    public GameObject animRefObject;
    

    // Use this for initialization
    void Start () {
        equipments.Add(new Equipment("NONE", EquipType.melee));
        weapons.Add(new Weapon("CQC", 3, 15, AmmoType.Taze));
    }

    void Update() {

        Debug.DrawLine(transform.position, transform.position + transform.forward * weapons[weaponIndex].range);

        for (int e = 0; e < equipments.Count; e++) {
            //print(equipments[e].name);
        }

        for (int w = 0; w < weapons.Count; w++) {
            //print(weapons[w].name);
        }

        //UseWeapon();

        if (weaponCoolDown == weaponWaitTime)
        {
            if (Input.GetKeyUp(KeyCode.M))
            {
                weaponIndex++;

            }
            else if (Input.GetKeyUp(KeyCode.N))
            {
                weaponIndex--;
            }

            if (Input.GetKeyUp(KeyCode.J))
            {

                UseWeapon(weapons[weaponIndex].range);
            }
        }
        if (weaponCoolDown < weaponWaitTime) {
            weaponCoolDown += Time.deltaTime;
            if (weaponCoolDown > weaponWaitTime) {
                weaponCoolDown = weaponWaitTime;
            }
        }


        if (animRefObject != null)
        {
            animRefObject.GetComponent<Animator>().Play(1);
        }
    }

    public void UseEquipment() { }

    public void UseWeapon(float range) {
        print(gameObject.name + " used " + weapons[weaponIndex].name);
        
        weaponCoolDown = 0;

        RaycastHit hit;
        
        Physics.Linecast(transform.position, transform.position + transform.forward * range, out hit);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Character")
            {
                //print(hit.collider.name);
                hit.collider.GetComponent<Stats>().TakeDamage(weapons[weaponIndex].damage);
            }
        }
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