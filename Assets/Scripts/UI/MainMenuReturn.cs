using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuReturn : MonoBehaviour
{
    [SerializeField] private string _mainMenuScene;

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(_mainMenuScene);
    }
}
