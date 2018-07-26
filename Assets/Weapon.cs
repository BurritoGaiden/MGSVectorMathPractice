using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string name;
    public float range;
    public float damage;
    public AmmoType ammo;

    public Weapon(string dName, float dRange, float dDamage, AmmoType dAmmoType)
    {
        name = dName;
        range = dRange;
        damage = dDamage;
        ammo = dAmmoType;
    }
}

public enum AmmoType { Nine, FiveFiveSix, SevenSixTwo, Grenade, Taze}