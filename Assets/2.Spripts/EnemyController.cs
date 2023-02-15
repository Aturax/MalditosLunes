using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed = 0.0f;
    [SerializeField] private Transform _waypoint = null;
    private Vector3 _initial = Vector3.zero;
    private Vector3 _target = Vector3.zero;
    private bool _forward = true;

    private void Start()
    {
        _initial = transform.position;
        _target = _waypoint.position;
    }

    private void Update()
    {
        if (!HasReachedTarget())
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);        
    }

    private void SelectNextPoint()
    {
        if (_forward)
            _target = _initial;
        else
            _target = _waypoint.position;
        _forward = !_forward;
    }

    private bool HasReachedTarget()
    {
        Vector3 targetPosition = new();

        if (_forward)
            targetPosition = _target;
        else
            targetPosition = _initial;

        if (Vector3.Distance(targetPosition, transform.position) < _speed * Time.deltaTime)
        {
            SelectNextPoint();
            return true;
        }

        return false;
    }
}
