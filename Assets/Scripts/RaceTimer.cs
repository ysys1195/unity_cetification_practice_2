using UnityEngine;

public class RaceTimerAlternative : MonoBehaviour
{
  // startフラッグにぶつかったらタイマースタート
  // ゴールフラッグにぶつかったらタイマーを止めて、
  // タイムをconsoleで表示する
  // 違うフラッグにぶつかったら合計時間を増やす
  private float raceTime = 0f;
  private bool isRacing = false;

  private void Update() {
      if (isRacing)
      {
        raceTime += Time.deltaTime; // タイマーを更新
        Debug.Log("レースタイム: " + raceTime + "秒");
      }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag("Player")) return;

    string zoneName = gameObject.name.ToLower();

    if (zoneName.Contains("start"))
    {
      StartRace();
    }
  }

  private void StartRace()
  {
    isRacing = true;
    raceTime = 0f; // タイマーをリセット
    Debug.Log("レースがスタートしました。タイマーをリセットしました。");
  }

  private void StopRace()
  {
    isRacing = false;
    Debug.Log("レースタイム： " + raceTime + "秒");
  }

  public void AddTime(float timeToAdd)
  {
    if (isRacing)
    {
      raceTime += timeToAdd;
      Debug.Log("合計時間が " + timeToAdd + "秒 増えました。現在の合計時間: " + raceTime + "秒");
    }
  }
}