using UnityEngine;
using Delegate;

public class CollisionDetector : MonoBehaviour
{
    public event CollisionDelegate onEnter;
    public event CollisionDelegate onStay;
    public event CollisionDelegate onExit;

    private void OnTriggerEnter2D(Collider2D collision) => onEnter?.Invoke(collision.gameObject);
    private void OnTriggerStay2D(Collider2D collision) => onStay?.Invoke(collision.gameObject);
    private void OnTriggerExit2D(Collider2D collision) => onExit?.Invoke(collision.gameObject);
}