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

    GameObject selectionFrame;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        selectionFrame = transform.GetChild(0).gameObject;
    }

    public void Set(JokeResponse response)
    {
        selectionFrame.SetActive(false);
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
        selectionFrame.SetActive(true);
        OnResponse?.Invoke(correctAnswer ? ResponseType.CORRECT : ResponseType.INCORRECT);
    }
}
