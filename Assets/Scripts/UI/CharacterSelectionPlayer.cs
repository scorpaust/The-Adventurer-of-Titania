using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionPlayer : CharacterMovement
{
    private float moveX, moveY;

    private Camera mainCam;

    private Vector2 mousePos;
    private Vector2 direction;
    private Vector2 tempScale;

    private Animator anim;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;

        anim = GetComponent<Animator>();
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

    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);

        moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        HandlePlayerTurning();

        HandleMovement(moveX, moveY);
    }
}
