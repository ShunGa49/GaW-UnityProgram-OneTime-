using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("カメラ")]
    [SerializeField] private Camera cam;

    [Header("射程距離")]
    [SerializeField] private float distance = 100f;

    [Header("威力")]
    [SerializeField] private int damage = 25;

    [Header("銃口")]
    [SerializeField] private Transform firePoint;

    [Header("弾数")]
    [SerializeField] private int ammoMax = 30;

    [Header("UI")]
    [SerializeField] private TMP_Text ammoText;

    [Header("クロスヘア")]
    [SerializeField] private RectTransform crosshair;

    private int ammoCurrent;

    private Vector2 mousePosition;

    private void Start()
    {
        ammoCurrent = ammoMax;

        UpdateAmmoUI();
    }

    private void Update()
    {
        UpdateCrosshair();
    }

    #region 射撃

    /// <summary>
    /// 射撃入力
    /// </summary>
    public void OnShoot(InputAction.CallbackContext context)
    {
        // 押した瞬間だけ
        if (!context.performed) return;

        Shoot();
    }

    /// <summary>
    /// 射撃
    /// </summary>
    private void Shoot()
    {
        // 弾がない
        if (!CanShoot())
        {
            Debug.Log("弾切れ！");
            return;
        }
        // 弾を1発消費
        UseAmmo();

        // マウス位置からRayを飛ばす
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        // Rayが何かに当たった
        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log("Hit:" + hit.collider.name);

            // Enemyを取得
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                // ダメージ
                enemy.TakeDamage(damage);
                Debug.Log("Enemyに" + damage + "ダメージ！");
            }
        }
    }
    #endregion

    #region マウス

    /// <summary>
    /// マウス座標取得
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// クロスヘア更新
    /// </summary>
    private void UpdateCrosshair()
    {
        if (crosshair == null) return;

        crosshair.position = mousePosition;
    }
    #endregion

    #region 弾数

    /// <summary>
    /// 射撃可能か
    /// </summary>
    public bool CanShoot()
    {
        return ammoCurrent > 0;
    }

    /// <summary>
    /// 弾を1発使う
    /// </summary>
    public void UseAmmo()
    {
        ammoCurrent--;

        UpdateAmmoUI();
    }

    /// <summary>
    /// 弾数UI更新
    /// </summary>
    private void UpdateAmmoUI()
    {
        if (ammoText == null) return;

        ammoText.text = ammoCurrent + " / " + ammoMax;
    }
    #endregion
}
