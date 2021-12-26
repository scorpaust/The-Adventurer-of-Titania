using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnim;

    private AudioSource openGateSFX;

	private bool opened = false;

	private void Awake()
	{
		openGateSFX = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.MAIN_MENU_CHARACTER_TAG) && !opened)
		{
			openGateSFX.Play();

			doorAnim.SetBool(TagManager.OPEN_ANIMATION_PARAMETER, true);

			opened = true;
		}
	}
}
