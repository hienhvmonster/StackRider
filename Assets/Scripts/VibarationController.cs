using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibarationController : MonoBehaviour
{
    public static void SoftVibrate()
    {
        Handheld.Vibrate();
    }

    public static void HardVibrate()
    {
        Handheld.Vibrate();
        Handheld.Vibrate();
    }
}
