using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Camera _camera;

    public float smoothTime = .5f;
    public float zoomVelocity;
    public float startTimeBeforeZoomOut;

    private Vector3 _velocity = Vector3.zero;
    private float _timeBeforeZoomOut;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // camera follow
        transform.position = Vector3.SmoothDamp(transform.position, _target.position, ref _velocity, smoothTime);

        // TODO: check this code:
        // perhaps we should create a ZoomIn and ZoomOut method
        // camera zooming out (if player is editing, or is moving)
        if (false)
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, 7f, ref zoomVelocity, 1f);
        }
        else if (_camera.orthographicSize >= 5.01f)
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, 5f, ref zoomVelocity, 0.4f);
            _timeBeforeZoomOut = startTimeBeforeZoomOut;
        }
        else if (_timeBeforeZoomOut > 0f)
        {
            _timeBeforeZoomOut -= Time.deltaTime;
        }
    }
}
