using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 2D Platformer を動かすためのコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    /// <summary>横移動する力</summary>
    [SerializeField] float m_runPower = 5f;
    /// <summary>ジャンプする力</summary>
    [SerializeField] float m_jumpPower = 250f;
    /// <summary>（横移動の）最大速度</summary>
    [SerializeField] float m_maxSpeed = 1f;
    /// <summary>横移動が入力されていない時の減速係数</summary>
    [SerializeField] float m_breakCoeff = 0.9f;
    /// <summary>左右の入力値</summary>
    float h;
    /// <summary>上下の入力値</summary>
    float v;
    Rigidbody2D m_rb2d;
    bool Jump = false;
    private float interval = 1.0f;
    float timer;
    Animation anim;
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 上下左右の入力を受け付ける
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        m_rb2d.velocity = dir * m_runPower;
        if (Player.GetComponent<Renderer>().material.color == Color.white)
        {
            m_runPower = 5.0f;
            m_jumpPower = 250f;
        }
        if (Player.GetComponent<Renderer>().material.color == Color.red)
        {
            m_runPower = 10.0f;
            m_jumpPower = 250f;
        }
        if (Player.GetComponent<Renderer>().material.color == Color.blue)
        {
            m_runPower = 5.0f;
            m_jumpPower = 500f;
        }
        
        // 力学的に上昇している時は接地とみなさない
        //if ()
        //{
        //    return false;
        //}
        //ジャンプ処理
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (Jump == true)
            {
                if (timer > interval)
                {
                    timer = 0;
                    m_rb2d.AddForce(Vector2.up * m_jumpPower);
                }
            }
        }
    }

    void FixedUpdate()
    {
        // 入力に応じて物理挙動を制御する
        if (h == 0)
        {
            // 左右の入力がない時は減速する
            if (m_rb2d.velocity.x != 0)
            {
                Vector2 v = m_rb2d.velocity;
                v.x = v.x * m_breakCoeff;
                m_rb2d.velocity = v;
            }
        }
        else
        {
            // 入力がある時は、最大速度になるまで加速する
            if (h > 0 ? m_rb2d.velocity.x < m_maxSpeed : -1 * m_rb2d.velocity.x < m_maxSpeed)
            {
                m_rb2d.AddForce(Vector2.right * m_runPower * h, ForceMode2D.Force);
            }
        }
        if(Input.GetKeyDown("b"))
        {
                GetComponent<Renderer>().material.color = Color.blue;
        }
        if(Input.GetKeyDown("m"))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (Input.GetKeyDown("v"))
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Jump = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        Jump = false;   
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.GetComponent<Renderer>().material.color == Color.red)
        {
            SceneManager.LoadScene("stage2");
        }
        if(Player.GetComponent<Renderer>().material.color == Color.blue)
        {
            SceneManager.LoadScene("stage3");
        }
        if (Player.GetComponent<Renderer>().material.color == Color.white)
        {
            SceneManager.LoadScene("stage4");
        }
    }
}
