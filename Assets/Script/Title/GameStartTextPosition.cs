using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTextPosition : MonoBehaviour
{
    [SerializeField] private RectTransform _parentRectTransform;
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �Ώ�Mob�̏ꏊ�ɃQ�[�W���ړ��BWorld���W��Local���W��ϊ�����Ƃ���RectTransformUtility���g��
        var screenPoint = _camera.WorldToScreenPoint(_transform.position);
        Vector2 localPoint;
        // �����Canvas��Render Mode��Screen Space - Overlay�Ȃ̂ő�3������null���w�肵�Ă���BScreen Space - Camera �̏ꍇ�́A�Ώۂ̃J������n���K�v������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 0);
    }
}
