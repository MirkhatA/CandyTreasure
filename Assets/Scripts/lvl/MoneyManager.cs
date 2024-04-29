using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private float money;

    public float Money
    {
        get { return money; }
        set
        {
            money = value; 
            PlayerPrefs.SetFloat("money", money); 
        }
    }

    private void Start()
    {
        Money = PlayerPrefs.GetFloat("money", 0f); 
    }

    public void IncreaseMoney()
    {
        if (Random.Range(0, 2) == 0)
        {
            Money += 1f;
        }
    }   
}
