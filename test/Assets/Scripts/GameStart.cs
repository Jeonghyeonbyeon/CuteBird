using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button gameStartButton;

    void Start()
    {
        gameStartButton.onClick.AddListener(OnClickGameStartButton);
    }

    private void OnClickGameStartButton()
    {
        SceneManager.LoadScene("InGame");
    }
}