using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] PlayerMovement _playerMovement;

    private int walk_up, walk_down, walk_side;
    private void SetAnimation()
    {
        switch (_playerMovement.MoveVector)
        {
            case Vector2 v when v.Equals(Vector2.up):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_up, 0f);
                break;

            case Vector2 v when v.Equals(Vector2.down):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_down, 0f);
                break;

            case Vector2 v when v.Equals(Vector2.left):
                _spriteRenderer.flipX = false;
                _animator.CrossFade(walk_side, 0f);
                break;

            case Vector2 v when v.Equals(Vector2.right):
                _spriteRenderer.flipX = true;
                _animator.CrossFade(walk_side, 0f);
                break;

            case Vector2 v when v.Equals(Vector2.zero):
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

    void Awake()
    {
        SerializeAnimationStrings();
    }

    void Update()
    {
        SetAnimation();
    }
}
