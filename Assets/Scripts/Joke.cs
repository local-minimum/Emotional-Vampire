using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum JokePhase { NONE, CALL, RESPONSE };

[System.Serializable]
public struct JokeCall
{
    public string text;
    public float readTime;
}

[System.Serializable]
public struct JokeResponse
{
    public string text;
    public bool isValid;    
}

[System.Serializable]
public struct JokeData
{
    public JokeCall call;
    public JokeResponse response1;
    public JokeResponse response2;
    public JokeResponse response3;
    public float responseTime;
    public int power;
}

public delegate void JokedEvent(int score);

public class Joke : MonoBehaviour
{
    public event JokedEvent OnJoked;

    [SerializeField]
    List<JokeData> jokes = new List<JokeData>();

    [SerializeField]
    ProgressBar progress;

    JokePhase jokePhase = JokePhase.CALL;

    [SerializeField]
    GameObject callGO;

    [SerializeField]
    Response[] responses;
    [SerializeField]
    GameObject responseGO;

    JokeData currentJoke;

    private void OnEnable()
    {
        progress.OnProgressComplete += HandleRunningOutOfTime;
        for (int i=0; i< responses.Length;i++)
        {
            responses[i].OnResponse += HandleResponse;
        }
        currentJoke = jokes[Random.Range(0, jokes.Count)];
        ShowCall();
    }

    private void OnDisable()
    {
        progress.OnProgressComplete -= HandleRunningOutOfTime;
        for (int i = 0; i < responses.Length; i++)
        {
            responses[i].OnResponse -= HandleResponse;
        }
    }

    private void HandleResponse(ResponseType responseType)
    {
        switch (responseType)
        {
            case ResponseType.CORRECT:
                OnJoked?.Invoke(currentJoke.power);
                break;
            case ResponseType.INCORRECT:
                OnJoked?.Invoke(-currentJoke.power);
                break;
        }
        progress.FreezeProgress();

    }

    private void HandleRunningOutOfTime()
    {
        switch (jokePhase)
        {
            case JokePhase.CALL:
                jokePhase = JokePhase.RESPONSE;
                ShowResponses();
                break;
            case JokePhase.RESPONSE:
                jokePhase = JokePhase.NONE;
                PanicSelectRandom();
                break;
        }        
    }

    private void ShowCall()
    {
        callGO.SetActive(true);
        callGO.GetComponent<TextMeshProUGUI>().text = currentJoke.call.text;
        progress.StartProgress(currentJoke.call.readTime, true);
        responseGO.SetActive(false);
        jokePhase = JokePhase.CALL;
    }

    private void ShowResponses()
    {
        callGO.SetActive(false);
        JokeResponse[] responses = GetJokeResponses();

        //TODO: Shuffle?
        for (int i=0; i< this.responses.Length; i++)
        {
            this.responses[i].Set(responses[i]);
        }
        responseGO.SetActive(true);
        progress.StartProgress(currentJoke.responseTime);
    }

    private JokeResponse[] GetJokeResponses()
    {
        JokeResponse[] responses = new JokeResponse[]
        {
            currentJoke.response1, currentJoke.response2, currentJoke.response3,
        };

        //TODO: Shuffle?
        return responses;
    }

    private void PanicSelectRandom()
    {
        responses[Random.Range(0, responses.Length)].Emit();
    }

    private void JokeOut()
    {
        jokePhase = JokePhase.NONE;       
        gameObject.SetActive(false);
    }
}
