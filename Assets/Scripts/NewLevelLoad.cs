using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelLoad : MonoBehaviour
{
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load the scene which has scene index +1 than the current scene
    }
}
