using UnityEngine;

public class VelocityMovement : MonoBehaviour, IMove
{
    [SerializeField] protected float _speed = 1f;
    [SerializeField] protected Rigidbody2D _rigidbody2D;

    public Vector2 MoveVector { get; set; }

    public virtual void Move()
    {
        //Debug.Log(gameObject.name + " move to " + MoveVector);
        _rigidbody2D.velocity = MoveVector * Time.fixedDeltaTime * _speed;
    }

    private void FixedUpdate()
    {
        Move();
    }
}
