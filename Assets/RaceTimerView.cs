using UnityEngine;
using TMPro;

public class RaceTimerView : MonoBehaviour
{
    [SerializeField] private CanvasGroup timer;

    private void OnEnable()
    {
        GameEventAlternative.OnRaceStart += DisplayTimer;
    }

    private void OnDisable()
    {
        GameEventAlternative.OnRaceStart -= DisplayTimer;
    }

    private void DisplayTimer()
    {
        timer.alpha = 1f;
    }
}
