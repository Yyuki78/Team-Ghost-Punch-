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
    /// ゲージを初期化します。
    /// </summary>
    /// <param name="parentRectTransform"></param>
    /// <param name="camera"></param>
    /// <param name="status"></param>
    public void Initialize(RectTransform parentRectTransform, Camera camera, MobStatus status)
    {
        // 座標の計算に使うパラメータを受け取り、保持しておく
        _parentRectTransform = parentRectTransform;
        _camera = camera;
        _status = status;
        Refresh();
    }

    /// <summary>
    /// ゲージを更新します。
    /// </summary>
    private void Refresh()
    {
        // 残りライフを表示
        fillImage.fillAmount = _status.Life / _status.LifeMax;

        // 対象Mobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        // 今回はCanvasのRender ModeがScreen Space - Overlayなので第3引数にnullを指定している。Screen Space - Camera の場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);
        transform.localPosition = localPoint + new Vector2(10, 40); // ゲージがキャラに重なるので、少し上にずらしている
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