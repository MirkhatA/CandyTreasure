using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemComponent : MonoBehaviour
{
    public void SetItemBGPref(int index)
    {
        PlayerPrefs.SetInt("backgroundIndex", index);
        Debug.Log(PlayerPrefs.GetInt("backgroundIndex"));
    }
}
