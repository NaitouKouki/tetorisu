using UnityEngine;

public class BGMController : MonoBehaviour
{
    // スピーカーの役割
    public AudioSource audioSource;
    // 流したい曲（アセットからアサインする）
    public AudioClip musicClip;

    void Start()
    {
        // ゲーム開始時に再生したい場合はこれ
        PlayMusic();
    }

    // 音楽を再生するメソッド
    public void PlayMusic()
    {
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    // 音楽を止めるメソッド
    public void StopMusic()
    {
        audioSource.Stop();
    }
}