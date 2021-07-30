using UnityEngine;

public class ExplorationState : State
{
    [SerializeField] private float _velocity = 1f;

    [SerializeField] private float _rayDistance = 2f;
    [SerializeField] private float _rayHeight = 0.25f;

    [SerializeField] private Camera _camera;
    private CameraMovement _cameraMovement;
    private Transform _cameraTransform;

    private float _horizontal;
    private float _vertical;
    
    public override void Awake()
    {
        base.Awake();
        if (_camera == null) _camera = Camera.main;
         _cameraTransform = _camera.transform;
         _cameraMovement = _camera.GetComponent<CameraMovement>();
    }

    public override void Enter(State previousState)
    {
        _cameraMovement.CameraLock = false;
    }

    public override void StateUpdate()
    {
        _vertical = InputManager.Vertical;
        _horizontal = InputManager.Horizontal;

        if (InputManager.Action())
        {
            LookForInspectableObject();
        }

        if (InputManager.Pause())
        {
            StateManager.SetState(StateManager.PausedState);
        }
    }

    public override void StateFixedUpdate()
    {
        if (_vertical > 0.1f || _vertical < -0.1f || _horizontal > 0.1f || _horizontal < -0.1f)
        {
            var right = _cameraTransform.right;
            right.y = 0.0f;
            right = right.normalized;

            var forward = _cameraTransform.forward;
            forward.y = 0.0f;
            forward = forward.normalized;

            Vector3 direction = (forward * _vertical + right * _horizontal).normalized;
            Vector3 movement = new Vector3(direction.x, 0, direction.z);

            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * (_velocity * 0.01f), Space.World);
        }
    }

    void LookForInspectableObject()
    {
        RaycastHit hit, hitLeft, hitRight;

        if (Physics.Raycast(transform.position + new Vector3(0, _rayHeight, 0), transform.forward, out hit,
            _rayDistance)) // ray
        {
            if (hit.transform.gameObject.GetComponent<InteractionManager>() == null) return;
            
            GameManager.CurrentInteractionManager = hit.transform.gameObject.GetComponent<InteractionManager>();
            StateManager.SetState(StateManager.InspectionState);
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, _rayHeight + 1, 0), transform.forward, out hitLeft, _rayDistance)) // higher ray
        {
            if (hitLeft.transform.gameObject.GetComponent<InteractionManager>() == null) return;

            GameManager.CurrentInteractionManager = hitLeft.transform.gameObject.GetComponent<InteractionManager>();
            StateManager.SetState(StateManager.InspectionState);
        }
        else if (Physics.Raycast(transform.position + new Vector3(-0.2f, _rayHeight, 0), transform.forward, out hitLeft,_rayDistance)) //left ray
        {
            if (hitLeft.transform.gameObject.GetComponent<InteractionManager>() == null) return;
            
            GameManager.CurrentInteractionManager = hitLeft.transform.gameObject.GetComponent<InteractionManager>();
            StateManager.SetState(StateManager.InspectionState);
        }
        else if (Physics.Raycast(transform.position + new Vector3(0.2f, _rayHeight, 0), transform.forward, out hitRight, _rayDistance)) //right ray
        {
            if (hitRight.transform.gameObject.GetComponent<InteractionManager>() == null) return;
            
            GameManager.CurrentInteractionManager = hitRight.transform.gameObject.GetComponent<InteractionManager>();
            StateManager.SetState(StateManager.InspectionState);
        }
    }

    public override void Exit(State nextState)
    {
        return;
    }
}
