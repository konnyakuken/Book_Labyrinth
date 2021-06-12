using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    AudioSource audioSource;
    public bool IsFade;
    public double FadeOutSeconds = 1.0;
    public double FadeInSeconds = 0.01;
    public bool IsFadeOut = false;
    public bool IsFadeIn = false;
    double FadeDeltaTime = 0;
    public AudioClip first;
    public AudioClip create_book;

    [SerializeField]
    private AudioSource[] _audios;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = first;
        audioSource.Play();
    }

    void Update()
    {
        if (IsFadeOut)
        {
            /*
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime  <= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                IsFadeOut = false;
            }
            audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);*/
            audioSource.volume = 0;
            IsFadeOut = false;
            audioSource.clip = create_book;
            audioSource.volume = 0.01f;
            audioSource.Play();
        }

        if (IsFadeIn)
        {/*
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime >= FadeInSeconds)
            {
                FadeDeltaTime = FadeInSeconds;
                IsFadeIn = false;
            }
            audioSource.volume = (float)(FadeDeltaTime / FadeInSeconds);*/
            audioSource.volume = 0.01f;
            IsFadeIn = false;

        }
    }
}
