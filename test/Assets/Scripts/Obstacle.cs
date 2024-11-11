using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hasScored = false;

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (!hasScored && transform.position.x < -1.5f)
        {
            GameManager.Instance.AddScore(1);
            hasScored = true;
            StartCoroutine(DestroyDelay());
        }
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}