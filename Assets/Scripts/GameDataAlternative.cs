using UnityEngine;

public class GameDataAlternative : MonoBehaviour
{
  public int completedRaces;
  private static GameDataAlternative instance;

  private void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;
      DontDestroyOnLoad(this);
    }
  }

  public static GameDataAlternative Instance
  {
    get { return instance; }
  }
}