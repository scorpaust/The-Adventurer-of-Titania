using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float health;

    private Animator anim;

	private void Awake()
	{
        anim = GetComponent<Animator>();
	}

	private void Start()
	{
		health = maxHealth;
	}

	private void DestroyCharacter()
	{
		Destroy(gameObject);
	}

	public void TakeDamage(float amount)
	{
		health -= amount;

		if (health <= 0f)
		{
			anim.SetTrigger(TagManager.DEATH_ANIMATION_PARAMETER);
		}
	}

	public bool IsAlive()
	{
		return health > 0f;
	}
}
