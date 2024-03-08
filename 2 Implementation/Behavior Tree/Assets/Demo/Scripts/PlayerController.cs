using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Vector3 _velocity;
    private Vector3 _smoothDeltaPosition;

    [Header("References")] [SerializeField]
    private NavMeshAgent _agent = null;

    [SerializeField] private Animator _animator = null;

    private Vector3 _facingDirection;

    public bool isMoving => _agent.velocity.magnitude > 0.5 && _agent.remainingDistance > _agent.stoppingDistance;

    [UsedImplicitly]
    private void Awake()
    {
        // Customize root motion handling
        _animator.applyRootMotion = true;
        _agent.updatePosition = false;
        _agent.updateRotation = true;
    }

    [UsedImplicitly]
    private void Update()
    {
        SynchronizeAnimatorAndAgent();
    }

    public void MoveToTable(Table table)
    {
        if (table == null)
        {
            Debug.LogWarning("Table is not assigned.");
            return;
        }

        float distance = 1.25f;
        Vector3 target = table.transform.position;
        Vector3 destination = target + table.transform.forward * distance;
        _facingDirection = -table.transform.forward;
        _agent.SetDestination(destination);
    }

    // details: this function has to be called in Update() because OnAnimatorMove() is before LateUpdate()
    public void SynchronizeAnimatorAndAgent()
    {
        Vector3 movingDirection = (_agent.nextPosition - transform.position).normalized;
        movingDirection.y = 0;

        float dx = Vector3.Dot(transform.right, movingDirection);
        float dz = Vector3.Dot(transform.forward, movingDirection);
        var localDeltaPosition = new Vector3(dx, 0, dz);

        float smoothingFactor = Mathf.Min(1, Time.deltaTime / 0.1f);
        _smoothDeltaPosition = Vector3.Lerp(_smoothDeltaPosition, localDeltaPosition, smoothingFactor);

        _velocity = _smoothDeltaPosition / Time.deltaTime;

        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _velocity = Vector3.Lerp(Vector3.zero, _velocity,
                _agent.remainingDistance / _agent.stoppingDistance) * Time.deltaTime;
        }

        float turnThreshold = CalculateTurnThreshold();

        _animator.SetBool("isMoving", isMoving);
        _animator.SetFloat("inputX", _velocity.x);
        _animator.SetFloat("inputZ", _velocity.z);
        _animator.SetFloat("turnThreshold", turnThreshold);
    }

    private float CalculateTurnThreshold(float min = -2, float max = 2)
    {
        Vector3 currentDirection = _agent.transform.forward;
        Vector3 targetDirection = _facingDirection.normalized;
        Vector3 crossProduct = Vector3.Cross(currentDirection, targetDirection);
        float dotProduct = Vector3.Dot(crossProduct, _agent.transform.up);
        float angle = Vector3.Angle(currentDirection, targetDirection);

        float turnDirection = dotProduct > 0 ? angle : -angle;

        return Mathf.Clamp(turnDirection / 90, min, max);
    }

    [UsedImplicitly]
    private void OnAnimatorMove()
    {
        Vector3 rootPosition = _animator.rootPosition;
        rootPosition.y = _agent.nextPosition.y;

        transform.SetPositionAndRotation(rootPosition, _animator.rootRotation);
        _agent.nextPosition = rootPosition;
    }
}