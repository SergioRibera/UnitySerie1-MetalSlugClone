using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0f, 1000f)]
    [SerializeField] float velocity = 0.0f;
    [SerializeField] float timeDeath = 7.0f;
    [SerializeField] float raduisExplode = 10.0f;
    [SerializeField] int damage = 10;

    Rigidbody2D rb;

    bool initialize;
    ModelPlayer myPlayer;
    Vector2 pos;
    Vector2 direction;
    public void Init(bool right, ModelPlayer player)
    {
        direction = right ? Vector2.right : Vector2.left;
        myPlayer = player;
        rb = GetComponent<Rigidbody2D>();
        initialize = true;
        Explode();
    }
    private void FixedUpdate()
    {
        if (!initialize) return;
        pos = direction * velocity * Time.deltaTime;
        rb.MovePosition(rb.position + pos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ModelPlayer>())
            if (collision.GetComponent<ModelPlayer>().id == myPlayer.id)
                return;
        Explode();
    }
    void Explode()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, raduisExplode);
        foreach (var col in collisions)
            if (col.GetComponent<ModelPlayer>())
                if (col.GetComponent<ModelPlayer>().id == myPlayer.id)
                    col.GetComponent<ModelPlayer>().ReceivedDamage(damage);
        Destroy(gameObject, timeDeath);
    }
}