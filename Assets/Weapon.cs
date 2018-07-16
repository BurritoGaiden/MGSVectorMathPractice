using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string name;
    public WeaponType type;
    public WeaponKind kind;
    public float damage;
    public AmmoType ammo;

    public Weapon(string dName, WeaponType dType, WeaponKind dKind, float dDamage, AmmoType dAmmoType)
    {
        name = dName;
        type = dType;
        kind = dKind;
        damage = dDamage;
        ammo = dAmmoType;
    }
}

public enum AmmoType { Nine, FiveFiveSix, SevenSixTwo, Grenade, Taze}
public enum WeaponKind { USP, M16, Knife, CQC, Grenade, AK47, MP5}
public enum WeaponType { melee, ranged };