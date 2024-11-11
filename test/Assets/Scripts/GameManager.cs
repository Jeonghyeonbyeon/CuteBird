using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject clear;
    [SerializeField] private GameObject obstacleSpawn;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotationSpeed = 45f;
    private int score = 0;
    private float originalGravityScale;
    private bool isRotating = false;
    private bool hasIncreasedJumpPower = false;
    public bool isGameClear = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();

        if (playerController != null)
        {
            originalGravityScale = playerController.GetComponent<Rigidbody2D>().gravityScale;
        }
    }

    private void Update()
    {
        if (isGameClear && playerController != null)
        {
            SmoothMovePlayerToGround();
        }
    }

    private void SmoothMovePlayerToGround()
    {
        Rigidbody2D playerRigidbody = playerController.GetComponent<Rigidbody2D>();

        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 0;
        }

        Vector3 targetPosition = playerController.transform.position;
        targetPosition.y = Mathf.MoveTowards(targetPosition.y, 0, Time.deltaTime * 5f);
        playerController.transform.position = targetPosition;

        if (Mathf.Abs(playerController.transform.position.y) < 0.01f)
        {
            playerController.transform.position = new Vector3(playerController.transform.position.x, 0, playerController.transform.position.z);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= 100)
        {
            clear.SetActive(true);
            obstacleSpawn.SetActive(false);
            scoreText.gameObject.SetActive(false);
            isGameClear = true;
            mainCamera.transform.rotation = Quaternion.identity;
        }
        else
        {
            if (score % 5 == 0)
            {
                RotateCameraRandomly();
            }

            if (score >= 30 && !isRotating)
            {
                StartCameraRotation();
            }

            if (score >= 50 && !hasIncreasedJumpPower)
            {
                IncreaseJumpPower(50f);
                hasIncreasedJumpPower = true;
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
    }

    private void RotateCameraRandomly()
    {
        int randomAngle = Random.Range(0, 4) * 90;
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, randomAngle);
    }

    private void StartCameraRotation()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCameraCoroutine());
        }
    }

    private IEnumerator RotateCameraCoroutine()
    {
        isRotating = true;
        float duration;
        float elapsedTime;
        int direction = 1;

        while (!isGameClear) 
        {
            duration = Random.Range(5f, 10f);
            elapsedTime = 0f;

            while (elapsedTime < duration && !isGameClear)
            {
                mainCamera.transform.Rotate(0, 0, direction * rotationSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            direction *= -1;
        }
        isRotating = false;
        mainCamera.transform.rotation = Quaternion.identity;
    }

    private void IncreaseJumpPower(float amount)
    {
        if (playerController != null)
        {
            playerController.jumpPower += amount;
        }
    }
}