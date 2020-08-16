using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer : MonoBehaviour
{
    #region Visible Vars in Inspector
    [SerializeField] float velocity;
    [SerializeField] float jumpForce;
    #endregion

    #region External Components
    [Header("Componentes Internos (Extras)")]
    [SerializeField] CollisionDetector floorDetector;
    #endregion

    #region Internal Vars
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Vector2 pos = Vector2.zero;
    #endregion

    #region Auxiliar Vars
    bool doubleJump = false;
    bool habiliteDoubleJump = true;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (floorDetector != null)
        {
            floorDetector.onEnter += OnEnterFloor;
            floorDetector.onExit += OnExitFloor;
        }
        else
        {
            throw new System.Exception("Floor Detector No Asignado");
        }
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void OnEnterFloor(GameObject colObj)
    {
        if (!colObj.CompareTag("Player"))
        {
            anim.SetBool("Jump", false);
            habiliteDoubleJump = true;
            doubleJump = false;
        }
    }
    private void OnExitFloor(GameObject colObj)
    {
        if (!colObj.CompareTag("Player"))
            habiliteDoubleJump = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && habiliteDoubleJump && !doubleJump)
        {
            anim.SetBool("Jump", true);
            rb.AddForce(new Vector2(0, jumpForce));
        }
        if (Input.GetKeyDown(KeyCode.Space) && !habiliteDoubleJump && !doubleJump)
        {
            anim.SetBool("Jump", true);
            doubleJump = true;
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        anim.SetBool("Walk", x > 0 || x < 0);
        if(x < 0)
        {
            spriteRenderer.flipX = true;
            spriteRenderer.transform.localPosition = new Vector3(-1.73f, 0, 0);
        }
        else if(x > 0)
        {
            spriteRenderer.flipX = false;
            spriteRenderer.transform.localPosition = new Vector3(1.73f, 0, 0);
        }
        pos.Set(x, 0);
        pos = pos.normalized * velocity * Time.deltaTime;
        rb.MovePosition(rb.position + pos);
    }
}