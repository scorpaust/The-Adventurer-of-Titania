using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string levelName;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.MAIN_MENU_CHARACTER_TAG))
		{
			SceneManager.LoadScene(levelName);
		}
	}
}
