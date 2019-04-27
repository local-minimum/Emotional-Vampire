using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ProgressCompleteEvent();

public class ProgressBar : MonoBehaviour
{
    public event ProgressCompleteEvent OnProgressComplete;

    float duration = 1;
    float start = 0;
    bool running = false;
    bool allowAnyKeyAbort = false;

    [SerializeField]
    Image progressImage;

    private void OnEnable()
    {
        progressImage.fillAmount = 0;
    }

    public void StartProgress(float duration, bool allowAnyKeyAbort = false)
    {
        this.duration = duration;
        start = Time.timeSinceLevelLoad;
        running = true;
        this.allowAnyKeyAbort = allowAnyKeyAbort;
        progressImage.fillAmount = 0;
    }

    public void FreezeProgress()
    {
        running = false;
    }

    private void Update()
    {
        if (running)
        {
            float progress = Mathf.Clamp01((Time.timeSinceLevelLoad - start) / duration);
            progressImage.fillAmount = progress;
            if (allowAnyKeyAbort && Input.anyKey) progress = 1;
            if (progress == 1)
            {
                running = false;
                OnProgressComplete?.Invoke();                
            } 
        }
    }
}
