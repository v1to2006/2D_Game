using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private LevelSelectorMenu _levelSelectorMenu;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		Open();
	}

	public void OpenLevelSelectorMenu()
	{
		Close();

		_levelSelectorMenu.Open();
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
