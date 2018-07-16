using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public float Nine;
    public float FiveFiveSix;
    public float SevenSixTwo;
    public float Grenade;
    public float Tazes;

    public Ammo(float x, float y, float z, float a, float b)
    {
        Nine = x;
        FiveFiveSix = y;
        SevenSixTwo = z;
        Grenade = a;
        Tazes = b;
    }
}
