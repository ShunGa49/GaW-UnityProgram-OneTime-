using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target;

    [Header("カメラ位置")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 3f, -5f);

    [Header("ズーム")]
    [SerializeField] private Camera cam;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float aimFOV = 30f;
    [SerializeField] private float zoomSpeed = 10f;

    [Header("AIM")]
    [SerializeField] private float aimSensitivity = 0.05f;

    private bool isAiming = false;

    private Vector2 mousePosition;

    // 通常時のカメラ回転
    private Quaternion normalRotation;


    private void Start()
    {
        // カメラ位置
        transform.position = target.position + offset;

        // 現在のカメラ角度を保存
        normalRotation = transform.rotation;

        // 通常FOV
        cam.fieldOfView = normalFOV;
    }


    private void Update()
    {
        // 右クリック判定
        if (Mouse.current != null)
        {
            isAiming = Mouse.current.rightButton.isPressed;

            // マウス座標
            mousePosition = Mouse.current.position.ReadValue();
        }

        // ズーム
        UpdateZoom();
    }


    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // プレイヤーについていく
        transform.position = target.position + offset;

        if (isAiming)
        {
            // AIM中
            UpdateAim();
        }
        else
        {
            // 通常時
            transform.rotation = normalRotation;
        }
    }


    /// <summary>
    /// ズーム
    /// </summary>
    private void UpdateZoom()
    {
        float targetFOV;

        if (isAiming)
        {
            targetFOV = aimFOV;
        }
        else
        {
            targetFOV = normalFOV;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }


    /// <summary>
    /// AIM
    /// </summary>
    private void UpdateAim()
    {
        // 画面中央
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // マウスが中央からどれだけ離れているか
        Vector2 mouseOffset = mousePosition - screenCenter;

        // AIM角度
        float yaw = mouseOffset.x * aimSensitivity;

        float pitch = -mouseOffset.y * aimSensitivity;

        // 通常カメラを基準に回転
        Quaternion targetRotation = normalRotation * Quaternion.Euler(pitch, yaw, 0f);

        // カメラを回転
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
}
