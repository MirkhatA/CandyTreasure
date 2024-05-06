using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OneSignalSDK;

public class OneSignalComponent : MonoBehaviour
{

    public string ONESIGNAL_APP_ID;

    private void Start()
    {
        OneSignal.Initialize(ONESIGNAL_APP_ID);
    }
}
