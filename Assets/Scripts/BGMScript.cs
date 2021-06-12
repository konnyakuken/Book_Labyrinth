using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMScript : MonoBehaviour
{
    AudioSource audioSource;
    public bool IsFade;
    public double FadeOutSeconds = 1.0;
    public double FadeInSeconds = 0.01;
    public bool chenge_BGM = false;
    bool BGM_check = false;

    double FadeDeltaTime = 0;
    public AudioClip first;
    public AudioClip create_book;
    public AudioClip winer_bgm;

    public bool vIctory_anime = false;

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
        if (chenge_BGM==true&&BGM_check==false)
        {
            /*
            FadeDeltaTime += Time.deltaTime;
            if (FadeDeltaTime  <= FadeOutSeconds)
            {
                FadeDeltaTime = FadeOutSeconds;
                chenge_BGM = false;
            }
            audioSource.volume = (float)(1.0 - FadeDeltaTime / FadeOutSeconds);*/
            audioSource.volume = 0;
            chenge_BGM = false;
            BGM_check = true;
            audioSource.clip = create_book;
            audioSource.volume = 0.01f;
            audioSource.Play();
        }
    }

    public void Victory()
    {
        Debug.Log("on!");
        audioSource.volume = 0; audioSource.clip = winer_bgm;
        audioSource.volume = 0.01f;
        vIctory_anime = true;
        audioSource.Play();
        DOVirtual.DelayedCall(2f, () => {
            SceneManager.LoadScene("result");
        });
        
    }
}
