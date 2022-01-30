using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mob�i�����I�u�W�F�N�g�AMovingObject�̗��j�̏�ԊǗ��X�N���v�g
/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    /// <summary>
    /// ��Ԃ̒�`
    /// </summary>
    protected enum StateEnum
    {
        Normal, // �ʏ�
        Run,    //�ǂ������Ă���
        Attack, // �U����
        Die // ���S
    }
    /// <summary>
    /// �ړ��\���ǂ���
    /// </summary>
    public bool IsMovable => StateEnum.Normal == _state || StateEnum.Run == _state;

    /// <summary>
    /// �U���\���ǂ���
    /// </summary>
    public bool IsAttackable => StateEnum.Normal == _state || StateEnum.Run == _state;

    /// <summary>
    /// �ǂ���������ǂ���
    /// </summary>
    public bool IsRunnable => StateEnum.Normal == _state;

    /// <summary>
    /// �ǂ������Ă��邩�ǂ���
    /// </summary>
    public bool IsRunState => StateEnum.Run == _state;

    /// <summary>
    /// ���C�t�ő�l��Ԃ��܂�
    /// </summary>
    public float LifeMax => lifeMax;

    /// <summary>
    /// ���C�t�̒l��Ԃ��܂�
    /// </summary>
    public float Life => _life;

    [SerializeField] private float lifeMax = 10; // ���C�t�ő�l
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; // Mob���
    private float _life; // ���݂̃��C�t�l�i�q�b�g�|�C���g�j

    protected virtual void Start()
    {
        _life = lifeMax; // ������Ԃ̓��C�t���^��
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// �L�������|�ꂽ���̏������L�q���܂��B
    /// </summary>
    protected virtual void OnDie()
    {
    }

    /// <summary>
    /// �w��l�̃_���[�W���󂯂܂��B
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();
    }

    /// <summary>
    /// �\�ł���΍U�����̏�ԂɑJ�ڂ��܂��B
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// �\�ł���Βǂ��������ԂɑJ�ڂ��܂��B
    /// </summary>
    public void GoToRunStateIfPossible()
    {
        if (!IsMovable) return;

        _state = StateEnum.Run;
    }

    /// <summary>
    /// �\�ł����Normal�̏�ԂɑJ�ڂ��܂��B
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }
}