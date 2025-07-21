using System.Collections;
using TMPro;
using UnityEngine;

public class GameEventAlternative : MonoBehaviour
{
  public delegate void raceAction();

  public static event raceAction OnRaceEnd;

  public static void RaceEnd()
  {
    OnRaceEnd?.Invoke();
  }
}
