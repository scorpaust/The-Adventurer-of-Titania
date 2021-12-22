using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 2.5f;

    [SerializeField]
    private float damageAmount = 25f;

    [SerializeField]
    private float deactivateTimer = 3f;

    [SerializeField]
    private bool destroyObj;

    private Animator anim;

    private SpriteRenderer sr;

    private Sprite bulletSprite;

    private Color initialColor;

    private bool dealtDamage;

	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

		sr = GetComponent<SpriteRenderer>();

		bulletSprite = sr.sprite;

	}

	private void OnEnable()
	{
		anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, false);

		anim.enabled = false;

		sr.sprite = bulletSprite;

		dealtDamage = false;

		Invoke("DeactivateBullet", deactivateTimer);
	}

	private void OnDisable()
	{
		anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
	}

	private void DeactivateBullet()
	{
        if (destroyObj)
		{
            Destroy(gameObject);
		}
        else
		{
            gameObject.SetActive(false);
		}
	}

    public void MoveInDirection(Vector3 direction)
	{
        rb.velocity = direction * moveSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.ENEMY_TAG) || collision.CompareTag(TagManager.SHOOTER_ENEMY_TAG) || collision.CompareTag(TagManager.BOSS_TAG))
		{
			rb.velocity = Vector2.zero;

			CancelInvoke("DeactivateBullet");

			anim.enabled = true;

			anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);

			if (!dealtDamage)
			{
				dealtDamage = true;

				// Deal damage to enemy
			}
		}

        if (collision.CompareTag(TagManager.BLOCKING_TAG))
		{
			rb.velocity = Vector2.zero;

			CancelInvoke("DeactivateBullet");

			anim.enabled = true;

			anim.SetBool(TagManager.EXPLODE_ANIMATION_PARAMETER, true);
		}
	}

	

}
