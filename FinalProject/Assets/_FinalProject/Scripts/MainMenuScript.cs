using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlaySingleplayer()
    {
        SceneManager.LoadScene("MainScene"); // First load the MainScene (maze scene)
        SceneManager.LoadScene("SingleplayerScene", LoadSceneMode.Additive); // Then load the Singleplayer scene additively
    }

    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene("MultiplayerScene", LoadSceneMode.Additive); // Load Multiplayer scene additively
    }

    public void PlayCoop()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.LoadScene("CoopScene", LoadSceneMode.Additive); // Load Coop scene additively
    }
}
