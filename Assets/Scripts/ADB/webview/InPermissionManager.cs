using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class InPermissionManager : MonoBehaviour
{
    private object callbacks;

    public void InPermissionManager_CheckPermission(string permissionName)
    {
        bool result = Permission.HasUserAuthorizedPermission("android.permission." + permissionName);

        if (!result)
        {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;

            Permission.RequestUserPermission("android.permission." + permissionName, callbacks);
            result = Permission.HasUserAuthorizedPermission("android.permission." + permissionName);
        }
        else GetInAppBrowserHelper().CallStatic("gggggg", true);
    }
    private static AndroidJavaObject GetInAppBrowserHelper()
    {
        var helper = new AndroidJavaClass("inappbrowser.kokosoft.com.InAppBrowserDialogFragment");
        return helper;
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        System.Console.WriteLine("gfdgf #0");
        GetInAppBrowserHelper().CallStatic("gggggg", false);
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        System.Console.WriteLine("gfdgf #1");
        GetInAppBrowserHelper().CallStatic("gggggg", true);
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        System.Console.WriteLine("gfdgf #2");
        GetInAppBrowserHelper().CallStatic("gggggg", false);
    }
}