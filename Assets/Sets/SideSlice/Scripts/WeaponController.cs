using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float playerAttack;
    [SerializeField] private float comboDelay = .5f;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sword;

    private Collider swordHB;
    private PlayerController playerController;

    private int numAttackSwing = 0;
    private float lastSwingTime = 0f;
    private bool bufferedInput = false;

    public bool canAttack = true;
    public bool IsAttacking { get; set; }

    private void Awake()
    {
        swordHB = sword.GetComponent<Collider>();
        playerController = transform.root.GetComponent<PlayerController>();
        IsAttacking = false;
    }

    private void Update()
    {
        if (IsAttacking && Time.time - lastSwingTime > comboDelay)
        {
            ResetCombo();
        }
        if (bufferedInput && !IsAttacking)
        {
            Debug.Log("Combo buffer");
            StartComboFromBuffer();
        }
    }

    public void SwordSwing()
    {
        if (!canAttack)
        {
            return;
        }

        lastSwingTime = Time.time;

        if (!IsAttacking)
        {
            numAttackSwing = 1;
            SwingAttack(numAttackSwing);
        }
        else
        {
            bufferedInput = true;
        }
    }

    private void SwingAttack(int attackNumber)
    {
        IsAttacking = true;
        swordHB.enabled = true;

        switch (attackNumber)
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
        }
    }

    private void StartComboFromBuffer()
    {
        if (numAttackSwing == 0)
        {
            numAttackSwing = 1;
        }
        else
        {
            numAttackSwing = Mathf.Clamp(numAttackSwing + 1, 1, 3);
        }

        bufferedInput = false;

        Debug.Log(numAttackSwing);

        SwingAttack(numAttackSwing);
    }

    public void OnAttackAnimationEnd()
    {
        swordHB.enabled = false;

        if (bufferedInput && numAttackSwing < 3)
        {
            bufferedInput = false;
            numAttackSwing++;
            lastSwingTime = Time.time;
            SwingAttack(numAttackSwing);
        }
        else
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        Debug.Log("Combo Reset");
        numAttackSwing = 0;
        bufferedInput = false;
        IsAttacking = false;
        RestoreSpeed();
    }

    public void SlowPlayer()
    {
        animator.SetBool("isMoving", false);
        playerController.PlayerSpeed = 0f;
        playerController.PlayerTurnSpeed = 0f;
    }

    public void RestoreSpeed()
    {
        playerController.PlayerSpeed = 6f;
        playerController.PlayerTurnSpeed = 1000f;
        EndAttack();
    }

    public void EndAttack()
    {
        IsAttacking = false;
        swordHB.enabled = false;
    }
}
