using UnityEngine;

public class PinkFlag : MonoBehaviour
{
    [SerializeField] private Material correctMaterial;
    [SerializeField] private Material failMaterial;
    private MeshRenderer parentRenderer;
    private RaceTimerAlternative raceTimer;

    private void Start()
    {
        parentRenderer = transform.parent.GetComponent<MeshRenderer>();
        raceTimer = FindFirstObjectByType<RaceTimerAlternative>();
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
            raceTimer.AddTime(1f);
        }
    }

    // プレイヤーの位置からみてピンクのフラッグの位置が右にあったらtrueを返す
    private bool IsCorrectSide(float playerX)
    {
        Debug.Log("Player X: " + playerX + ", Flag X: " + transform.position.x + ", Is Correct Side: " + (transform.position.x > playerX));
        var flagX = transform.position.x;
        return flagX > playerX;
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
