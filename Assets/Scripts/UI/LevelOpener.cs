using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelOpener : MonoBehaviour
{
	[SerializeField] private string SceneToLoad;

	public void OpenLevel()
	{
		SceneManager.LoadScene(SceneToLoad);
	}
}
