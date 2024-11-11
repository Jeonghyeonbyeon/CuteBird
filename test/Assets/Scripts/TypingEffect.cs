using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    private Text textComponent;
    private Coroutine typingCoroutine;
    private string fullText;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        fullText = textComponent.text;
        textComponent.text = "";
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypingCoroutine());
    }

    private IEnumerator TypingCoroutine()
    {
        textComponent.text = "";
        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}