using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YazirusiDirection : MonoBehaviour
{
    [SerializeField] private RectTransform _parentRectTransform;
    [SerializeField] private Camera _camera;
    private MobStatus _status;

    [SerializeField] GameObject Mirror;
    private Transform _Mtransform;

    [SerializeField] private Transform _playerTransform;

    // �O���̊�ƂȂ郍�[�J����ԃx�N�g��
    [SerializeField] private Vector3 _forward = Vector3.forward;

    private void Start()
    {
        //_parentRectTransform = GetComponent<RectTransform>();
        _Mtransform = Mirror.GetComponent<Transform>();
    }

    private void Update()
    {
        Refresh();
    }

    /// <summary>
    /// �Q�[�W�����������܂��B
    /// </summary>
    /// <param name="parentRectTransform"></param>
    /// <param name="camera"></param>
    /// <param name="status"></param>
    public void Initialize(MobStatus status)
    {
        _status = status;
        Refresh();
    }

    /// <summary>
    /// �Q�[�W���X�V���܂��B
    /// </summary>
    private void Refresh()
    {
        // �Ώ�Mob�̏ꏊ�ɃQ�[�W���ړ��BWorld���W��Local���W��ϊ�����Ƃ���RectTransformUtility���g��
        var screenPoint = _camera.WorldToScreenPoint((Vector2)_playerTransform.position-(Vector2)_Mtransform.position);
        Vector2 localPoint;
        // �����Canvas��Render Mode��Screen Space - Overlay�Ȃ̂ő�3������null���w�肵�Ă���BScreen Space - Camera �̏ꍇ�́A�Ώۂ̃J������n���K�v������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);

        // �^�[�Q�b�g�ւ̌����x�N�g���v�Z
        var dir = _Mtransform.position - transform.position;
        // �^�[�Q�b�g�̕����ւ̉�]
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // ��]�␳
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // ��]�␳���^�[�Q�b�g�����ւ̉�]�̏��ɁA���g�̌����𑀍삷��
        //transform.rotation = lookAtRotation * offsetRotation;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        transform.localPosition = localPoint + new Vector2(40, 20);
    }
}
