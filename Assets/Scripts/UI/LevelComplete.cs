using UnityEngine;

public class LevelComplete : MonoBehaviour
{
	private void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
