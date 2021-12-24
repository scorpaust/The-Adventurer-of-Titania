using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private bool isSlow, canRotate;

    [SerializeField]
    private bool poolBullet;

    [SerializeField]
    private float deactivateTimer = 5f;

    [SerializeField]
    private float damageAmount = 10f;

    private Rigidbody2D rb;

    private Animator anim;

    private bool dealtDamage;

	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
	}

	private void Start()
	{
        Invoke("DeactivateBullet", deactivateTimer);
	}

	private void FixedUpdate()
	{
		if (isSlow)
		{
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Random.value * Time.deltaTime);
		}

        if (canRotate)
		{
            transform.Rotate(Vector3.forward * 60f);
		}
	}

	private void DeactivateBullet()
	{
        if (poolBullet)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
	}

	private void OnDisable()
	{
        transform.rotation = Quaternion.identity;

        isSlow = false;
	}

    public void SetIsSlow (bool slowParam)
	{
        isSlow = slowParam;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TagManager.BLOCKING_TAG))
		{
			rb.velocity = Vector2.zero;

			CancelInvoke("DeactivateBullet");

			anim.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);
		}

		if (collision.CompareTag(TagManager.PLAYER_TAG))
		{
			rb.velocity = Vector2.zero;

			CancelInvoke("DeactivateBullet");

			anim.SetTrigger(TagManager.EXPLODE_ANIMATION_PARAMETER);

			if (!dealtDamage)
			{
				dealtDamage = true;

				collision.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
			}
		}
	}
}
