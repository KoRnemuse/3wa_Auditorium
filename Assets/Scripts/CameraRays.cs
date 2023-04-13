using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRays : MonoBehaviour
{
    private Camera _camera;
    private const string EFFECTOR_CENTER_TAG_NAME = "EffectorCenter";
    private const string EFFECTOR_EDGE_TAG_NAME = "EffectorEdge";

    private const string MOUSE_LEFT_CLICK = "Fire1";

    private Transform _currentColliderTransform;

    [SerializeField] private Texture2D _moveCursorTexture;
    [SerializeField] private Texture2D _resizeCursorTexture;
    [SerializeField] private LayerMask _interactableLayerMask;

    private bool _isMoving = false;
    private bool _isScaling = false;


    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D collider = CastRay();
            if(collider != null)
            {
                _currentColliderTransform = collider.transform;

                if(_currentColliderTransform.CompareTag(EFFECTOR_CENTER_TAG_NAME))
                {
                    _isMoving = true;
                }
                if(_currentColliderTransform.CompareTag(EFFECTOR_EDGE_TAG_NAME))
                {
                    _isScaling = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            _isMoving = false;
            _isScaling = false;
            _currentColliderTransform = null;
        }
        if(_isMoving)
        {
            DoMove();
        }

        if(_isScaling)
        {
            DoScale();
        }
    }
    private void FixedUpdate()
    {
        CastRay();
    }
    private Collider2D CastRay()
    {
        //Debug.Log(_camera.ScreenToWorldPoint(Input.mousePosition));
        //Debug.Log(Input.mousePosition);
        RaycastHit2D hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _interactableLayerMask);

        if(hit.collider != null)
        {
            if (hit.collider.CompareTag(EFFECTOR_CENTER_TAG_NAME))
            {
                //move                
                Cursor.SetCursor(_moveCursorTexture, new Vector2(_moveCursorTexture.width / 2, _moveCursorTexture.height / 2), CursorMode.ForceSoftware);

                /*if(Input.GetButton(MOUSE_LEFT_CLICK))
                {
                    //Debug.Log("Click appuyé");
                    //_currentColliderTransform = hit.collider.transform.parent;
                    //Debug.Log(element_clicked);
                    _currentColliderTransform.position = _camera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,-_camera.transform.position.z));

                }*/
               // Debug.Log("Effector Center");
            }
            else if(hit.collider.CompareTag(EFFECTOR_EDGE_TAG_NAME))
            {
                Cursor.SetCursor(_resizeCursorTexture, new Vector2(_resizeCursorTexture.width / 2, _resizeCursorTexture.height / 2), CursorMode.ForceSoftware);
                //resize
                //Debug.Log("Effector Edge");
            }
        }
        else
        {
            
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
          
        }

        return hit.collider;
    }
    private void DoMove() 
    {
        Vector3 screenToWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);// + new Vector3(0, 0, -_camera.transform.position.z));
        screenToWorldPosition.z = 0;
        _currentColliderTransform.parent.position = screenToWorldPosition;
        Debug.Log("moving");
    }
    private void DoScale()
    {
        Vector2 transformPos = _currentColliderTransform.position;
        Vector2 MousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(Vector2.Distance(transformPos, MousePos));
        //Debug.Log("resize");
        float modifiedRadius = 0f;
        CircleShape circleShape = _currentColliderTransform.GetComponent<CircleShape>();
        float circleRadius = circleShape.Radius;
        modifiedRadius = Vector2.Distance(transformPos, MousePos);
        circleRadius = modifiedRadius;
        circleShape.Radius = circleRadius;
        Debug.Log(circleRadius);
    }
}
