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

    public string KeyName
    {
        get
        {
            switch (key)
            {
                case KeyCode.Alpha1:
                    return "1";
                case KeyCode.Alpha2:
                    return "2";
                case KeyCode.Alpha3:
                    return "3";
                default:
                    return key.ToString();
            }
        }
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        selectionFrame = transform.GetChild(0).gameObject;
    }

    public void Set(JokeResponse response)
    {
        selectionFrame.SetActive(false);
        text.text = $"({KeyName}) {response.text}";
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
