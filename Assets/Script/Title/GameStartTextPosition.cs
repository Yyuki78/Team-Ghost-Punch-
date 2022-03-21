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
        // 対象Mobの場所にゲージを移動。World座標やLocal座標を変換するときはRectTransformUtilityを使う
        var screenPoint = _camera.WorldToScreenPoint(_transform.position);
        Vector2 localPoint;
        // 今回はCanvasのRender ModeがScreen Space - Overlayなので第3引数にnullを指定している。Screen Space - Camera の場合は、対象のカメラを渡す必要がある
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null,
            out localPoint);
        transform.localPosition = localPoint + new Vector2(0, 0);
    }
}
