using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dieUI;
    [SerializeField] private float minHeight = -5f;
    [SerializeField] private float maxHeight = 5f;
    private Rigidbody2D rb;
    public float jumpPower;
    public bool isDie = false;

    void Start()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.start);

        rb = GetComponent<Rigidbody2D>();

        Time.timeScale = 1f;
    }

    public void OnJump()
    {
        if (!isDie && !GameManager.Instance.isGameClear)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpPower);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.jump);
        }
    }

    private void Update()
    {
        if (!isDie && !GameManager.Instance.isGameClear && (transform.position.y < minHeight || transform.position.y > maxHeight))
        {
            ShowDieUI();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDie && !GameManager.Instance.isGameClear && collision.gameObject.CompareTag("Obstacle"))
        {
            ShowDieUI();
        }
    }

    private void ShowDieUI()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.death);
        isDie = true;
        dieUI.SetActive(true);
        Time.timeScale = 0f;
    }
}