using UnityEngine;

/// <summary>
/// 衝突時サウンドを再生させる
/// </summary>
public class CollisionSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private ObstacleEventManager obstacleEventManager;

    private void Start() {
        obstacleEventManager = GetComponent<ObstacleEventManager>();
        obstacleEventManager.onCollideWithObstacle += SoundCollision;
    }

    private void SoundCollision(GameObject obstacle) {
        Debug.Log("sound!!!");
        audioSource.PlayOneShot(audioClip);
    }
}
