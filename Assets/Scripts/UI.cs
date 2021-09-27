using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public bool HitIndicatorActive;
    public GameObject HitIndicator;
    public TMP_Text HealthText;
    public static string Message;

    private void Update()
    {
        if (HitIndicatorActive)
        {
            HitIndicator.SetActive(true);
        }
        else
        {
            HitIndicator.SetActive(false);
        }
    }

    public void UpdateHealth(int health)
    {
        if (health < 0)
        {
            ReturnToMenu("You died!");
        }
        HealthText.SetText("Health: " + health);
    }

    public void EnableHitIndicator()
    {
        HitIndicatorActive = true;
    }

    public void DisableHitIndicator()
    {
        HitIndicatorActive = false;
    }

    public void ReturnToMenu(string message)
    {
        Message = message;
        SceneManager.LoadScene(0);
    }
}
