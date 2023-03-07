using UnityEngine;

public class PlayerMovement : VelocityMovement
{
    [SerializeField] private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void FixedUpdate()
    {
        MoveVector = _playerInput.Player.Move.ReadValue<Vector2>();
        Move();
    }
}
