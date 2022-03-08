using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private Camera mainCamera; // ���C�t�Q�[�W�\���Ώۂ�Mob���f���Ă���J����
    [SerializeField] private LifeGauge lifeGaugePrefab; // ���C�t�Q�[�W��Prefab

    private RectTransform rectTransform;
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap = new Dictionary<MobStatus, LifeGauge>(); // �A�N�e�B�u�ȃ��C�t�Q�[�W��ێ�����R���e�i

    private void Awake()
    {
        // �V�[�����1�������݂����Ȃ��X�N���v�g�̂��߁A���̂悤�ȋ^���V���O���g�������藧��
        if (null != _instance) throw new Exception("LifeBarContainer instance already exists.");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ���C�t�Q�[�W��ǉ����܂��B
    /// </summary>
    /// <param name="status"></param>
    public void Add(MobStatus status)
    {
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    /// <summary>
    /// ���C�t�Q�[�W��j�����܂��B
    /// </summary>
    /// <param name="status"></param>
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }

    /// <summary>
    /// ���C�t�Q�[�W��\�����܂��B
    /// </summary>
    /// <param name="status"></param>
    public void Show(MobStatus status)
    {
        _statusLifeBarMap[status].IsSee = true;
    }
}