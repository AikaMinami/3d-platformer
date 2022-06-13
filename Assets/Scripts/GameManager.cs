using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentGold, currentHealth, maxGold;
    public Text textGold, textHealth;

    public GameObject panelLose, panelWin;
    public AudioSource loseSfx, winSfx;
    
    void Update()
    {
        if(currentGold >= maxGold)
        {
            winSfx.Play();
            panelWin.SetActive(true);
        }

        if(currentHealth <= 0)
        {
            loseSfx.Play();
            panelLose.SetActive(true);
        }
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        textGold.text = "Gold: " + currentGold;
    }

    public void GetHurt(int hurtDamage)
    {
        currentHealth -= hurtDamage;
        textHealth.text = "Health: " + currentHealth;
    }
}
