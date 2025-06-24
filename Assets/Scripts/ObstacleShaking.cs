using System;
using System.Collections;
using UnityEngine;

public class ObstacleShaking : ObstacleAlternative
{
    public override void HitPlayer(GameObject player)
    {
        base.HitPlayer(player);
        // オブジェクトを揺らす
        
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        yield return StartCoroutine(RotateOverTime(Vector3.up, 45f, 0.1f));
        yield return StartCoroutine(RotateOverTime(Vector3.up, -45f, 0.1f));

        Destroy(this.gameObject);
    }

    IEnumerator RotateOverTime(Vector3 axis, float angle, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.AngleAxis(angle, axis);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Math.Clamp(elapsed / duration, 0f, 1f);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
