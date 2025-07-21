using System.Collections;
using TMPro;
using UnityEngine;

public class GameEventAlternative : MonoBehaviour
{
  public delegate void raceAction();

  public static event raceAction OnRaceStart;
  public static event raceAction OnRaceEnd;

  public static event raceAction OnRaceRetry;
  public static event raceAction OnRaceNextLevel;

  public delegate IEnumerator quitGameAction();
  public static event quitGameAction OnQuitGame;

  public static void RaceStart()
  {
    OnRaceStart?.Invoke();
  }
  public static void RaceEnd()
  {
    OnRaceEnd?.Invoke();
  }
  public static void RaceRetry()
  {
    OnRaceRetry?.Invoke();
  }
  public static void RaceNextLevel()
  {
    OnRaceNextLevel?.Invoke();
  }
  public void QuitGame()
  {
    if (OnQuitGame != null)
    {
      StartCoroutine(OnQuitGame());
    }
  }
}
