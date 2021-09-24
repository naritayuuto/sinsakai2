using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItenCount : MonoBehaviour
{
    public Text ScoreText;
    private int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetScore();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            Score += 1;
        }
        SetScore();
    }
    void SetScore()
    {
        ScoreText.text = string.Format("財宝数{0}", Score);
    }
}
