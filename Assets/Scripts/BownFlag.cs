using System.Collections;
using UnityEngine;

public class BownFlag : MonoBehaviour
{
    private readonly float boostTime = 1f; // ブースト時間
    private readonly float boostSpeed = 500f; // ブースト速度

    // プレイヤーが通ったらブーストする
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // ブーストの処理を実行
        BoostPlayer(other.gameObject);
    }

    // プレイヤーにブーストを与える
    private void BoostPlayer(GameObject player)
    {
        var playerController = player.GetComponent<AlternatePlayerInput>();
        if (playerController != null)
        {
            var originalSpeed = playerController.playerStats.speedMaximum;
            playerController.playerStats.speedMaximum += boostSpeed;

            StartCoroutine(ResetSpeedAfterDelay(playerController, originalSpeed));
        }
    }
    

    private IEnumerator ResetSpeedAfterDelay(AlternatePlayerInput playerController, float originalSpeed)
    {
        yield return new WaitForSeconds(boostTime);
        playerController.playerStats.speedMaximum = originalSpeed;
        Debug.Log("元のスピードに戻しました");
    }
}
