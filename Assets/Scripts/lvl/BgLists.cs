using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLists : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("hasBgWithId0", 1);
        PlayerPrefs.SetInt("hasBgWithId1", 1);
        PlayerPrefs.SetInt("hasBgWithId2", 1);
    }
}
