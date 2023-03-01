using UnityEngine;

public interface IMove
{
    Vector2 MoveVector { get; set; }
    void Move();
}
