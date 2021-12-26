using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private float cameraOrthoSize = 1f, cameraYPos = 0.63f;

    [SerializeField]
    private GameObject tapButton;

    private Camera mainCam;

    private bool changeCamPos;

    private Vector3 camTempPos;

    private float lerpSpeed = 5f;

    private bool canSelectCharacter;

	private void Awake()
	{
        mainCam = Camera.main;
	}

	private void Update()
	{
        ChangeCameraPosition();

        SelectCharacter();
	}

    private void ChangeCameraPosition()
	{
        if (changeCamPos)
		{
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, cameraOrthoSize, lerpSpeed * Time.deltaTime);

            Vector3 camTempPos = mainCam.transform.position;

            camTempPos.y = Mathf.Lerp(camTempPos.y, cameraYPos, lerpSpeed * Time.deltaTime);

            mainCam.transform.position = camTempPos;

            if (Mathf.Approximately(mainCam.transform.position.y, cameraYPos) && Mathf.Approximately(mainCam.orthographicSize, cameraOrthoSize))
			{
                changeCamPos = false;

                canSelectCharacter = true;
			}
		}
	}

    private void SelectCharacter()
	{
        if (canSelectCharacter)
		{
            if (Input.GetMouseButtonDown(0))
			{
                RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.transform.CompareTag(TagManager.MAIN_MENU_CHARACTER_TAG))
				{
                    hit.transform.GetComponent<CharacterSelectionPlayer>().enabled = true;

                    mainCam.GetComponent<MainMenuCamera>().SetPlayerTarget(hit.transform);
				}
			}
		}
	}

    public void TapToStartGame()
	{
        tapButton.SetActive(false);

        changeCamPos = true;
	}
}
