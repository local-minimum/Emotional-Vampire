using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ResponseType { CORRECT, INCORRECT }
public delegate void ResponseSelectedEvent(ResponseType responseType);

public class Response : MonoBehaviour
{
    public event ResponseSelectedEvent OnResponse;

    TextMeshProUGUI text;

    bool correctAnswer;

    [SerializeField]
    KeyCode key;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Set(JokeResponse response)
    {
        text.text = $"({key.ToString()}) {response.text}";
        correctAnswer = response.isValid;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Emit();
        }
    }

    public void Emit()
    {
        OnResponse?.Invoke(correctAnswer ? ResponseType.CORRECT : ResponseType.INCORRECT);
    }
}
