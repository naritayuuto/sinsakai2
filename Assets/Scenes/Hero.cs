﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Hero : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float m_walkSpeed = 1f;
    /// <summary>直前に移動した方向</summary>
    Vector2 m_lastMovedDirection;
    SpriteRenderer m_sprite;
    Animator m_anim;
    Rigidbody2D m_rb;
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
   }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = AdjustInputDirection(h, v);   // 入力方向を４方向に変換（制限）する
        // オブジェクトを動かす
        //transform.Translate(m_walkSpeed * dir * Time.deltaTime);    // このやり方でもできるが、コライダーにめり込む
        m_rb.velocity = dir * m_walkSpeed;

        // 入力方向によって左右の向きを変える
        if (dir.x != 0)
        {
            m_sprite.flipX = (dir.x < 0);
        }

        Animate(dir.x, dir.y);

        m_lastMovedDirection = dir;
    }

    /// <summary>
    /// 入力された方向を４方向に制限し、Vector2 にして返す
    /// （斜めに入力された場合でも、それ以前の入力状況に応じて４方向に制限する）
    /// </summary>
    /// <param name="inputX"></param>
    /// <param name="inputY"></param>
    Vector2 AdjustInputDirection(float inputX, float inputY)
    {
        Vector2 dir = new Vector2(inputX, inputY);

        if (m_lastMovedDirection == Vector2.zero)
        {
            if (dir.x != 0 && dir.y != 0)
            {
                dir.y = 0;
            }
        }
        else if (m_lastMovedDirection.x != 0)
        {
            dir.y = 0;
        }
        else if (m_lastMovedDirection.y != 0)
        {
            dir.x = 0f;
        }

        return dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "warp")
        {
            SceneManager.LoadScene("end");
        }
        if(collision.gameObject.tag == "shoes")
        {
            m_walkSpeed = m_walkSpeed * 1.1f;
        }
        if(collision.gameObject.tag == "Item")
        {
            m_walkSpeed = m_walkSpeed * 0.95f;
        }
    }
    /// <summary>
    /// 入力と直前に移動した方向に応じてアニメーションを制御する
    /// </summary>
    /// <param name="inputX"></param>
    /// <param name="inputY"></param>
    void Animate(float inputX, float inputY)
    {
        if (m_anim == null) return; // Animator Controller がない場合は何もしない

        if (inputX != 0)
        {
            m_anim.Play("WalkRight");
           
        }
        else if (inputY > 0)
        {
            m_anim.Play("WalkUp");
        }
        else if (inputY < 0)
        {
            m_anim.Play("WalkDown");
        }
        else
        {
            if (m_lastMovedDirection.x != 0)
            {
                m_anim.Play("IdleRight");
            }
            else if (m_lastMovedDirection.y > 0)
            {
                m_anim.Play("IdleBack");
            }
            else if (m_lastMovedDirection.y < 0)
            {
                m_anim.Play("IdleFront");
            }
        }
    }
}
