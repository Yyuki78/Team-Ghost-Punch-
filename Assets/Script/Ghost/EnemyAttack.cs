using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �U������N���X
/// </summary>
[RequireComponent(typeof(MobStatus))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f; // �U����̃N�[���_�E���i�b�j
    [SerializeField] private Collider attackCollider;
    public bool attckone = true;
    private bool attackcor = true;
    private MobStatus _status;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// �U���\�ȏ�Ԃł���΍U�����s���܂��B
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return; // �X�e�[�^�X�ƏՓ˂����I�u�W�F�N�g�ōU���ۂ𔻒f

        _status.GoToAttackStateIfPossible();
        //�ǋL
        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// �U���Ώۂ��U���͈͂ɓ��������ɌĂ΂�܂��B
    /// </summary>
    /// <param name="collider"></param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    /*
    /// <summary>
    /// �U���̊J�n���ɌĂ΂�܂��B
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }*/

    /// <summary>
    /// attackCollider���U���Ώۂ�Hit�������ɌĂ΂�܂��B
    /// </summary>
    /// <param name="collider"></param>
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<Player>();
        if (null == targetMob) return;

        // �v���C���[�Ƀ_���[�W��^����
        //targetMob.Damage(1);
    }

    /*
    /// <summary>
    /// �U���̏I�����ɌĂ΂�܂��B
    /// </summary>
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }*/

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
        attckone = true;
    }

    private IEnumerator AttackCoroutine()
    {
        if (attckone == false) yield break;
        attckone = false;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
        yield break;
    }
}