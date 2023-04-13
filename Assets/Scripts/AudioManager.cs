using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private int _audioSourceCount;
    private AudioSource[] _aSource;

    private bool _isComplete;
    [SerializeField] private float _durationBeforeWinning = 3.0f;
    private float _timeWhenComplete = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _aSource = GetComponentsInChildren<AudioSource>();
        //_aSourceCounter = FindObjectsOfType<MusicBox>();
        _audioSourceCount = _aSource.Length;
        Debug.Log(_audioSourceCount);
        
        /*foreach(AudioSource source in _aSource)
        {
            source.Play();
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _audioSourceCount; i++)
        {
            if (_aSource[i].volume == 1)
            {
                _isComplete = true;
                _timeWhenComplete += Time.deltaTime;
            }
            else if (_aSource[i].volume < 1)
            {
                _isComplete = false;
                _timeWhenComplete = 0.0f;
            }
        }
        if(_isComplete && _timeWhenComplete<_durationBeforeWinning)
        {
            Win();
        }
    }

    void Win()
    {
        Debug.Log("WIIIIN");
    }

}
