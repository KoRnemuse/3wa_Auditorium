using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    [SerializeField] float _velocityLimit;

    private Rigidbody2D _rigibody;

    private MeshRenderer _meshRenderer;

    [SerializeField] float _timeToFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(_rigibody.velocity);

        if(_rigibody != null)
        {
            if(_rigibody.velocity.magnitude<= _velocityLimit)
            {
                StartCoroutine(IE_FadeOut(_timeToFadeOut));
            }
        }

        /*if (_rigibody.velocity.x <= _velocityLimit.x && _rigibody.velocity.y <= _velocityLimit.y) 
        {
            Destroy(gameObject);
        }*/
    }

    private IEnumerator IE_FadeOut(float timeToFade)
    {
        // on definit un timer pour définir quand il faut arrêter le fade
        float timer = 0f;
        // on cree une variable material qui recupere le material de la particule
        Material material = _meshRenderer.material;

        // on lance une boucle while tant que le fade n'est pas termine
        while(timer < timeToFade)
        {
            // on calcule le pourcentage de temps passe entre le temps d'entree et le temps total pour fade
            float percent = timer / timeToFade;

            //on recupere la couleur la color du materiau
            Color matColor = material.color;

            //On diminue l'alpha (opacite) du color du materiau
            matColor.a = Mathf.Lerp(1f, 0f, percent);

            //On applique l'alpha a la propriete color
            material.color = matColor;

            //on incremente le timer
            timer += Time.deltaTime;
           
            //on retourne null pour pauser et attendre la frame suivante
            yield return null;
        }

        Color finalColor = material.color;
        finalColor.a = 0f;
        material.color = finalColor;
           
        Destroy(gameObject);
    }
}
