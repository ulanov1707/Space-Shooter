using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _ExplosionAudioSource;
    [SerializeField]
    private AudioClip _ExplosionClip;
    void Start()
    {
        _ExplosionAudioSource = GetComponent<AudioSource>();
        if (_ExplosionAudioSource == null) { Debug.Log("AudioSource not Set"); }
        else { 
            _ExplosionAudioSource.clip = _ExplosionClip;
            _ExplosionAudioSource.Play();
        }
        Destroy(this.gameObject, 3f);// finishes his own animation and destoys himself
    }

   
}
