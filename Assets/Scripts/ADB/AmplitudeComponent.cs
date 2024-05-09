using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeComponent : MonoBehaviour
{
    private string AMPLITUDE_API_KEY = "6807148a8c6684cd93e54b5aa59c1cc8";

    private void Start()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(AMPLITUDE_API_KEY);
        Console.WriteLine("Amplitude::" + amplitude);
    }
}
