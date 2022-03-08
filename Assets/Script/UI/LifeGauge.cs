using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    Image _image;
    [SerializeField] private Image fillImage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;

    public bool IsSee = false;

    private void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine("Transparent");
    }

    private void Update()
    {
        if (IsSee == true)
        {
            IsSee = false;
            SeeGauge();
        }
        Refresh();
    }

    /// <summary>
    /// �Q�[�W�����������܂��B
    /// </summary>
    /// <param name="parentRectTransform"></param>
    /// <param name="camera"></param>
    /// <param name="status"></param>
    public void Initialize(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        // ���W�̌v�Z�Ɏg���p�����[�^���󂯎��A�ێ����Ă���
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();
    }

    /// <summary>
    /// �Q�[�W���X�V���܂��B
    /// </summary>
    private void Refresh()
    {
        // �c�胉�C�t��\��
        fillImage.fillAmount = _status.Life / _status.LifeMax;

        // �Ώ�Mob�̏ꏊ�ɃQ�[�W���ړ��BWorld���W��Local���W��ϊ�����Ƃ���RectTransformUtility���g��
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        // �����Canvas��Render Mode��Screen Space - Overlay�Ȃ̂ő�3������null���w�肵�Ă���BScreen Space - Camera �̏ꍇ�́A�Ώۂ̃J������n���K�v������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);
        transform.localPosition = localPoint + new Vector2(10, 40); // �Q�[�W���L�����ɏd�Ȃ�̂ŁA������ɂ��炵�Ă���
    }

    private void SeeGauge()
    {
        _image.color = new Color32(255, 255, 255, 255);
        fillImage.color = new Color32(255, 255, 255, 255);
        StopCoroutine("Transparent");
        StartCoroutine("Transparent");
    }

    private IEnumerator Transparent()
    {
        for (int i = 0; i < 255; i++)
        {
            if (_image.color.a <= 0) yield break;
            _image.color = _image.color - new Color32(0, 0, 0, 1);
            fillImage.color = _image.color - new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}