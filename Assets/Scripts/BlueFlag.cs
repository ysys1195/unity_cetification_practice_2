using System.Xml.Serialization;
using UnityEngine;

public class BlueFlag : MonoBehaviour
{
    [SerializeField] private Material correctMaterial;
    [SerializeField] private Material failMaterial;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // プレイヤーが通ったらフラッグの色を変える
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("player")) return;

        if (IsCorrectSide(other.transform.position.x))
        {
            // プレイヤーが正しい側からフラッグにぶつかった場合
            // フラッグの色を青に変える
            ChangeFlagColor(correctMaterial);
        }
        else
        {
            // プレイヤーが間違った側からフラッグにぶつかった場合
            ChangeFlagColor(failMaterial);
        }
    }

    // スタートのプレイヤーの位置からみて青いフラッグの位置が左にあったらtrueを返す
    private bool IsCorrectSide(float playerX)
    {
        var flagX = transform.position.x;
        return playerX >= flagX;
    }

    // フラッグの色を変える
    public void ChangeFlagColor(Material mat)
    {
        if (meshRenderer != null && mat != null)
        {
            meshRenderer.material = mat;
        }
    }
}
