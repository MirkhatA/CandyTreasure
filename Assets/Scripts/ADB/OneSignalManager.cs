using OneSignalSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalManager : MonoBehaviour
{
    private string ONE_SIGNAL_APP_ID = "1b35a4a7-ea78-487e-857d-9b81b22ec14b";

    private void Start()
    {
        OneSignal.Initialize(ONE_SIGNAL_APP_ID);
    }
}
