using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_Text MessageBox;

    private void Start()
    {
        // unlock mouse
        Cursor.lockState = CursorLockMode.None;
        // update the message box with win/loss message
        MessageBox.SetText(UI.Message);
    }

    public void StartGame()
    {
        // load the game
        SceneManager.LoadScene(1);
    }
}
