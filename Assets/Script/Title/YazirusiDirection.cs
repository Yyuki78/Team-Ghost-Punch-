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

    // 前方の基準となるローカル空間ベクトル
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
    /// ゲージを初期化します。
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
    /// ゲージを更新します。
    /// </summary>
    private void Refresh()
    {
        // 対象Mobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        var screenPoint = _camera.WorldToScreenPoint((Vector2)_playerTransform.position-(Vector2)_Mtransform.position);
        Vector2 localPoint;
        // 今回はCanvasのRender ModeがScreen Space - Overlayなので第3引数にnullを指定している。Screen Space - Camera の場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);

        // ターゲットへの向きベクトル計算
        var dir = _Mtransform.position - transform.position;
        // ターゲットの方向への回転
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // 回転補正
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // 回転補正→ターゲット方向への回転の順に、自身の向きを操作する
        //transform.rotation = lookAtRotation * offsetRotation;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        transform.localPosition = localPoint + new Vector2(40, 20);
    }
}
