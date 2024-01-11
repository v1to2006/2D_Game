using UnityEngine;

public class LevelSelectorMenu : MonoBehaviour
{
	[SerializeField] private MainMenu _mainMenu;

	private void Awake()
	{
		Close();
	}

	public void ReturnToMainMenu()
	{
		Close();

		_mainMenu.Open();
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
