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
            var cb = new PermissionCallbacks();
            cb.PermissionDenied += Callback_denied;
            cb.PermissionGranted += Callback_granted;
            cb.PermissionDeniedAndDontAskAgain += Callback_denied_dontAskMore;

            Permission.RequestUserPermission("android.permission." + permissionName, cb);
            result = Permission.HasUserAuthorizedPermission("android.permission." + permissionName);
        }
        else AppBrowseHelp().CallStatic("gggggg", true);
    }
    private static AndroidJavaObject AppBrowseHelp()
    {
        var hp = new AndroidJavaClass("treasuresco.candy.page.InAppBrowserDialogFragment");
        return hp;
    }

    internal void Callback_denied_dontAskMore(string permissionName)
    {
        AppBrowseHelp().CallStatic("gggggg", false);
    }

    internal void Callback_granted(string permissionName)
    {
        AppBrowseHelp().CallStatic("gggggg", true);
    }

    internal void Callback_denied(string permissionName)
    {
        AppBrowseHelp().CallStatic("gggggg", false);
    }
}