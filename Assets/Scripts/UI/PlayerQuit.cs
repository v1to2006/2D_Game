using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuit : MonoBehaviour
{
	[SerializeField] private KeyCode _quitKey;
	[SerializeField] private string _mainMenuScene;

	private void Update()
	{
		if (Input.GetKeyDown(_quitKey))
		{
			SceneManager.LoadScene(_mainMenuScene);
		}
	}
}
