using UnityEngine;

public class BlueFlag : MonoBehaviour
{
    [SerializeField] private Material correctMaterial;
    [SerializeField] private Material failMaterial;
    private MeshRenderer parentRenderer;

    private void Start()
    {
        parentRenderer = transform.parent.GetComponent<MeshRenderer>();
    }

    // プレイヤーが通ったらフラッグの色を変える
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (IsCorrectSide(other.transform.position.x))
        {
            // プレイヤーが正しい側からフラッグにぶつかった場合フラッグの色を緑に変える
            ChangeFlagColor(correctMaterial);
        }
        else
        {
            // プレイヤーが間違った側からフラッグにぶつかった場合フラッグの色を黒に変える
            ChangeFlagColor(failMaterial);
            // 1秒のペナルティを追加
            RaceTimerAlternative.Instance.AddTime(1f);
        }
    }

    // プレイヤーの位置からみて青いフラッグの位置が左にあったらtrueを返す
    private bool IsCorrectSide(float playerX)
    {
        var flagX = transform.position.x;
        return playerX >= flagX;
    }

    // フラッグの色を変える
    public void ChangeFlagColor(Material mat)
    {
        if (parentRenderer != null && mat != null)
        {
            parentRenderer.material = mat;
        }
    }
}
