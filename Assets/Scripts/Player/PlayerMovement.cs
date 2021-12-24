using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;

    private Camera mainCam;

    private Vector2 mousePos;
    private Vector2 direction;
    private Vector2 tempScale;

    private Animator anim;

    private PlayerWeaponManager playerWeaponManager;

    private CharacterHealth playerHealth;

	protected override void Awake()
	{
		base.Awake();

        mainCam = Camera.main;

        anim = GetComponent<Animator>();

        playerWeaponManager = GetComponent<PlayerWeaponManager>();
	}

	// Start is called before the first frame update
	void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
    private void HandlePlayerTurning()
	{
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized;

        HandlePlayerAnimation(direction.x, direction.y);

	}

    private void HandlePlayerAnimation(float x, float y)
	{
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;

        if (x > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (x < 0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;

        x = Mathf.Abs(x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);

        ActivateWeaponForSide(x, y);
	}

    private void ActivateWeaponForSide(float x, float y)
	{
        // Side
        if (x == 1f && y == 0f)
            playerWeaponManager.ActivateGun(0);

        // Up
        if (x == 0f && y == 1f)
            playerWeaponManager.ActivateGun(1);

        // Down
        if (x == 0f && y == -1f)
            playerWeaponManager.ActivateGun(2);

        // Diagonal Up
        if (x == 1f && y == 1f)
            playerWeaponManager.ActivateGun(3);

        // Diagonal Down
        if (x == 1f && y == -1f)
            playerWeaponManager.ActivateGun(4);
	}

	private void FixedUpdate()
	{
        if (!playerHealth.IsAlive()) return;

        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);

        moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        HandlePlayerTurning();

        HandleMovement(moveX, moveY);
	}

}
