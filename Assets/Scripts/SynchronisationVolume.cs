using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class SynchronisationVolume : MonoBehaviour
{
    [SerializeField] Material _soundbarOn;
    [SerializeField] Material _soundbarOff;

    Renderer[] _renderers;
    //Declaration d'un tableau a 2 dimensions
    //Renderer[,] _renderers;

    private MeshRenderer[] _meshRendererSoundbar;

    List<Transform> _children = new();
    List<GameObject> _childrenGO = new();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            _children.Add(child);
                
        }
        Debug.Log(_children.Count);

        for (int i=0; i< _children.Count; i++)
        {
            Debug.Log(_children[i]);
            _childrenGO.Add(transform.GetChild(i).gameObject);
            Debug.Log(_childrenGO[i]);
            _meshRendererSoundbar[i] = _childrenGO[i].GetComponent<MeshRenderer>();
            _meshRendererSoundbar[i].material.color = _soundbarOff.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
