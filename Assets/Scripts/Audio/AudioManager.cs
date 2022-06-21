using UnityEngine;

public enum AudioKey { UIClick }
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource UIClick;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioKey key)
    {
        switch (key)
        {
            case AudioKey.UIClick:
                UIClick.Play();
                break;
        }
    }
}
