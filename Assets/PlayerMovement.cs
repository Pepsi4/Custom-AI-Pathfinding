using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed = 1f;

    public Vector2 MoveVector { get; private set; }

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
    }

    private void Update()
    {
        this.transform.Translate(MoveVector * Time.deltaTime * _speed);
    }
}
