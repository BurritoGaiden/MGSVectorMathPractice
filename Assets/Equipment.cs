using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public string name;
    public EquipType type;

    public Equipment(string dName, EquipType dType)
    {
        name = dName;
        type = dType;
    }
}

public enum EquipType { melee, ranged };