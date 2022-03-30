using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �U������N���X
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    Player _player;

    [SerializeField] private float attackCooldown = 0.5f; // �U����̃N�[���_�E���i�b�j
    [SerializeField] private Collider attackColliderRight;
    [SerializeField] private Collider attackColliderLeft;
    public bool attackone = true;
    public bool attackcol = true;

    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
        attackColliderRight.enabled = false;
        attackColliderLeft.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// �U���\�ȏ�Ԃł���΍U�����s���܂��B
    /// </summary>
    public void AttackIfPossible()
    {
        //if (!_status.IsAttackable) return; // �X�e�[�^�X�ƏՓ˂����I�u�W�F�N�g�ōU���ۂ𔻒f

        //_status.GoToAttackStateIfPossible(); //Attack�X�e�[�^�X�Ɉڍs
        //�ǋL
        if (attackcol == true)
        {
            attackcol = false;
            StartCoroutine(AttackCoroutine());
        }
    }
    /// <summary>
    /// �U���Ώۂ��U���͈͂ɓ��������ɌĂ΂�܂��B
    /// </summary>
    /// <param name="collider"></param>
    public void OnAttackRangeEnter(Collider collider)
    {
        //AttackIfPossible();
    }
    /// <summary>
     /// attackCollider���U���Ώۂ�Hit�������ɌĂ΂�܂��B
     /// </summary>
     /// <param name="collider"></param>
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<EnemyStatus>();
        if (null == targetMob) return;

        // �G�l�~�[�Ƀ_���[�W��^����
        Debug.Log("Enemy�ɍU�����܂�");
        targetMob.Damage(1);
    }
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        //_status.GoToNormalStateIfPossible();
        attackone = true;
        attackcol = true;
    }

    private IEnumerator AttackCoroutine()
    {
        if (attackone == false) yield break;
        attackone = false;
        yield return new WaitForSeconds(0.01f);
        if (_player.IsDirection == true)
        {
            attackColliderRight.enabled = true;
        }
        else
        {
            attackColliderLeft.enabled = true;
        }
        audioSource.PlayOneShot(sound1);
        yield return new WaitForSeconds(0.5f);
        attackColliderRight.enabled = false;
        attackColliderLeft.enabled = false;
        StartCoroutine(CooldownCoroutine());
        yield break;
    }
}
