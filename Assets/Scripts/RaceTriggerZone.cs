using UnityEngine;

/// <summary>
/// スキーレースのスタートストップゾーンを管理するクラス
/// </summary>
public class RaceTriggerZone : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag("Player")) return;

    var timer = RaceTimerAlternative.Instance;
    if (timer == null) return;

    if (gameObject.name.ToLower().Contains("start"))
    {
      timer.StartRace();
    }
    else if (gameObject.name.ToLower().Contains("finish"))
    {
      timer.StopRace();
    }
  }
}