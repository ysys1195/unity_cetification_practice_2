using System.Collections;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public GameObject snowBall;
    public float throwDistance;
    public int throwSpeed;
    private bool justThown = false;
    private GameObject target;
    private Vector3 throwY = new Vector3(0, 0.33f, 0);
    private int frameInterval = 5;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % frameInterval == 0)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if (distanceToTarget < throwDistance && justThown == false)
            {
                ThrowSnowball();
            }

            // Vector3 offset = target.transform.position - transform.position;
            // float sqrLen = offset.sqrMagnitude;
            // if (sqrLen < throwDistance)
            // {
            //     ThrowSnowball();
            // }
        }

    }

    private void ThrowSnowball()
    {
        justThown = true;
        GameObject snowball = Instantiate(snowBall, transform.position, transform.rotation);
        Rigidbody rb = snowball.GetComponent<Rigidbody>();
        Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);
        targetDirection += throwY;
        rb.AddForce(targetDirection * throwSpeed);
        StartCoroutine(ThrowCooldown());
    }

    IEnumerator ThrowCooldown()
    {
        yield return new WaitForSeconds(1f);
        justThown = false;
    }
}
