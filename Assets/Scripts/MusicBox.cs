using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] Renderer[] _volumeBarsRenderers;

    [SerializeField] private Material _volumeBarOffMaterial;
    [SerializeField] private Material _volumeBarOnMaterial;


    [SerializeField] float _increaseParticleVolume =0.04f;
    [SerializeField] float _delayBeforeDecreasingVolume;
    [SerializeField] float _volumeDecreaseStep;

    private float _lastParticleCollision = 0f;

    private float _volumeBarsQuantity;

    // Start is called before the first frame update
    void Start()
    {
        _volumeBarsQuantity = _volumeBarsRenderers.Length;
        _audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the time since the last particle which entered the Gameobject is elapsed        
        if( Time.time > _lastParticleCollision + _delayBeforeDecreasingVolume)
        {
            //Decreasing volume of audiosource by the step chosen *Time.deltaTime
            _audioSource.volume -= _volumeDecreaseStep * Time.deltaTime;
        }

        //percentage of each Volumebar based on volume [0;1]
        float percent = (1 / _volumeBarsQuantity);
        float volume = _audioSource.volume;

        //We check the table of renderers of each Volumebar
        for (int i=0; i<_volumeBarsQuantity; i++)
        {
            //We apply the minimum volume each should be called
            float minVolume = (i + 1) * percent;

            //we compare the volume of the audiosource to the minimum volume of each Volumebar
            if (volume >= minVolume)
            {
                //We apply a certain material to the Volume bar
                _volumeBarsRenderers[i].material.color=_volumeBarOnMaterial.color;
                //autre methode pour appliquer le materiau
                //_volumeBarsRenderers[i].sharedMaterial=_volumeBarOnMaterial;
            }
            else
            {
                //We apply a certain material to the Volume bar
                _volumeBarsRenderers[i].material.color= _volumeBarOffMaterial.color;
            }
        }
        if(_lastParticleCollision>_delayBeforeDecreasingVolume)
        {
           // _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, percent);
        }

    }

    //Detection de la collision d'une particle avec la music Box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if tag is "Particle"
        if (collision.CompareTag("Particle"))
        {
            if(_audioSource.volume<1)
            {
                //Increasing volume when a particle hits the music box 
                _audioSource.volume += _increaseParticleVolume;
            }
            //On keep the time when the particle enter the box
            _lastParticleCollision = Time.time;

            //Debug.Log("Particle collided");
        }
    }
}
