using UnityEngine;

public class AstronautAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private VelocityMovement velocityMovement;

    private int walk_up, walk_down, walk_side;
    private void SetAnimation()
    {
        //Debug.Log(Vector2Int.RoundToInt(velocityMovement.MoveVector));
        switch (Vector2Int.RoundToInt(velocityMovement.MoveVector))
        {
            case Vector2Int v when v == Vector2Int.up || v == new Vector2Int(-1, 1):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_up, 0f);
                break;

            case Vector2Int v when v == Vector2Int.down || v == new Vector2Int(1, -1):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_down, 0f);
                break;

            case Vector2Int v when v == Vector2Int.left || v == new Vector2Int(-1, -1):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_side, 0f);
                break;

            case Vector2Int v when v == Vector2Int.right || v == new Vector2Int(1, 1):
                _spriteRenderer.flipX = true;
                _animator.CrossFade(walk_side, 0f);
                break;

            case Vector2Int v when v == Vector2Int.zero:
                _animator.SetTrigger("Stop");
                break;
        }
    }

    private void SerializeAnimationStrings()
    {
        walk_up = Animator.StringToHash("walk_up");
        walk_down = Animator.StringToHash("walk_down");
        walk_side = Animator.StringToHash("walk_side");
    }

    private void Awake()
    {
        SerializeAnimationStrings();
    }

    private void Update()
    {
        SetAnimation();
    }
}
