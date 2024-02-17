using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Section : MonoBehaviour
{
    [SerializeField] private ColorType _colorType;
    public ColorType ColorType => _colorType;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }
}