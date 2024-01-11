using UnityEngine;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private LevelSelectorMenu _levelSelectorMenu;

	private void Awake()
	{
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
