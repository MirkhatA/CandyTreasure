using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeComponent : MonoBehaviour
{
    public string AMPLITUDE_API_KEY;

    private void Start()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init(AMPLITUDE_API_KEY);
    }
}

