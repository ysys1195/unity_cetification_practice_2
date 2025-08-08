using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public GameObject snowBall;
    public float throwDistance;
    public int throwSpeed;
    private bool justThown = false;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (justThown) return;

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget < throwDistance)
        {
            justThown = true;
            GameObject tempSnowBall = Instantiate(snowBall, transform.position, transform.rotation);
            Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
            Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

            //Add a small throw angle
            targetDirection += new Vector3(0, 0.33f, 0);
            tempRb.AddForce(targetDirection * throwSpeed);
            StartCoroutine(ThrowOver());
        }

    }

    IEnumerator ThrowOver()
    {
        yield return new WaitForSeconds(0.1f);
        justThown = false;
    }
}
