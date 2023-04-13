using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] GameObject _particlePrefab;
    //[SerializeField] Vector2 _particleDirection;
    [Range(0.0f, 1f)]
    [SerializeField] float _circleRadius;
    [Range(0.0f, 3f)]
    [SerializeField] float _speed =0.04f;

    //[SerializeField] Rigidbody2D _rb;

    [SerializeField] float _delayBetweenParticles;
    private float _nextParticleTime;

    private void Awake()
    {
        //_rb.AddForce(new Vector2(1, 0.5f)* _speed, ForceMode2D.Impulse);

        _nextParticleTime = Time.time;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Time.time > _nextParticleTime)
        {
            _nextParticleTime = Time.time + _delayBetweenParticles;
            GenerateParticle();
        }
    }

    private void GenerateParticle()
    {
        Vector2 randomPosition = (Random.insideUnitCircle * _circleRadius) + (Vector2)transform.position;
        GameObject particleGO;
        //Instantiate le prefab choisi à la position randomPosition et avec une rotation nulle
        particleGO = Instantiate(_particlePrefab, randomPosition, Quaternion.identity);
        //Destroy(particleGO, 2.0f);

        // On récupère le component Rigidbody2D de la particule 
        Rigidbody2D particleRb2D = particleGO.GetComponent<Rigidbody2D>();
        //On lui rajoute une force impulse à une vitesse choisie
        particleRb2D.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _circleRadius);
    }
}
