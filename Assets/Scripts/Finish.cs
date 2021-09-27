using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public Enemy[] Remaining;
    public MeshRenderer Renderer;
    public Material Red, Yellow;

    private void Update()
    {
        // check how many enemies are left, and change the goal color based on count
        Remaining = FindObjectsOfType<Enemy>();
        if (Remaining.Length == 0)
        {
            Renderer.material = Yellow;
        }
        else
        {
            Renderer.material = Red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Remaining.Length == 0)
        {
            // back to menu
            UI ui = FindObjectOfType<UI>();
            if (ui != null)
            {
                ui.ReturnToMenu("You Win!");
            }
        }
    }
}
