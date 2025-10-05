using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float playerAttack;
    [SerializeField] private float attackCooldown = .3f;

    private float timer;
    private int numAttackSwing = 0;

    private int attackAnims = 3;

    private PlayerController playerController;
    private Collider swordHB;
    private Animator animator;

    private float currentSpeed;

    public GameObject sword;
    public bool canAttack = true;
    public bool isAttacking = false;

    private void Awake()
    {
        animator = sword.GetComponent<Animator>();
        swordHB = sword.GetComponent<Collider>();
        playerController = transform.parent.GetComponent<PlayerController>();


        currentSpeed = playerController.PlayerSpeed;
        timer = attackCooldown;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void SwordSwing()
    {
        if (timer >= attackCooldown && canAttack == true)
        {
            SwingAttack();
            timer = 0;

            StartCoroutine(ResetAttackCooldown());
        }

    }

    private void SwingAttack()
    {
        numAttackSwing = (numAttackSwing % attackAnims) + 1;
        isAttacking = true;

        playerController.PlayerSpeed *= 0.5f;

        switch (numAttackSwing)
        {
            case 1:
                animator.SetTrigger("Attack");
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
            default:
                break;
        }
        swordHB.enabled = true;
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        playerController.PlayerSpeed = currentSpeed;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
