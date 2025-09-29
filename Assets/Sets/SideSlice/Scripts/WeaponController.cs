using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float playerAttack;
    [SerializeField] private float attackCooldown = 1f;

    public GameObject sword;
    public bool canAttack = true;

    public void SwordSwing()
    {
        if (canAttack)
        {
            canAttack = false;
            Animator animator = sword.GetComponent<Animator>();

            animator.SetTrigger("Attack");
            StartCoroutine(ResetAttackCooldown());
        }
    }

    IEnumerator ResetAttackCooldown()
    {
        canAttack = true;
        yield return new WaitForSeconds(attackCooldown);
    }
}
