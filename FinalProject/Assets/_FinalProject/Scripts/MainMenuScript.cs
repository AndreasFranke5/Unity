using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlaySingleplayer()
    {
        Debug.Log("PlaySingleplayer() called");
        StartCoroutine(LoadAndStartGame("SingleplayerScene"));
    }

    public void PlayMultiplayer()
    {
        Debug.Log("PlayMultiplayer() called");
        StartCoroutine(LoadAndStartGame("MultiplayerScene"));
    }

    public void PlayCoop()
    {
        Debug.Log("PlayCoop() called");
        StartCoroutine(LoadAndStartGame("CoopScene"));
    }

    IEnumerator LoadAndStartGame(string sceneName)
    {
        // Ensure that all existing audio listeners are disabled before loading the new scene
        DisableAudioListeners();

        // Load the scene asynchronously
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Wait for one frame to ensure scene is fully initialized
        yield return null;

        // Find the GameManager by name
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            Debug.Log("GameManager GameObject found: " + gameManagerObject.name);

            // Get the component that implements IGameManager
            IGameManager gameManagerScript = gameManagerObject.GetComponent<IGameManager>();

            if (gameManagerScript != null)
            {
                Debug.Log("GameManager script found: " + gameManagerScript.GetType().Name);

                // Call StartGame() directly
                gameManagerScript.StartGame();
                Debug.Log("StartGame() method invoked.");
            }
            else
            {
                Debug.LogError("No script implementing IGameManager found on GameManager GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameManager GameObject not found in the scene!");
        }
    }

    // This function ensures that only one audio listener is active
    void DisableAudioListeners()
    {
        AudioListener[] listeners = GameObject.FindObjectsOfType<AudioListener>();
        foreach (AudioListener listener in listeners)
        {
            listener.enabled = false; // Disable all audio listeners before loading the new scene
        }
    }
}
