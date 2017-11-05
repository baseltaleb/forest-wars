using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;
    private AudioSource audioSource;

    private void OnEnable()
    {
        EventsManager.ItemPickedUp += PlayPickupSound;
    }
    private void OnDisable()
    {
        EventsManager.ItemPickedUp -= PlayPickupSound;
    }
    private void Awake()
    {
        if (!instance)
            instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayLooped(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void PlayPickupSound(Item item)
    {
        audioSource.PlayOneShot(PrefabsReference.instance.pickupAudio);
    }

    public void StopAllSounds()
    {
        audioSource.Stop();
    }
}
