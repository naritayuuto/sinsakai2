using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;      // UI を操作するために追加している
using UnityEngine.SceneManagement;  // シーン遷移を行うために追加している
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    /// <summary>ロードするシーン名</summary>
    [SerializeField] string m_sceneNameToBeLoaded = "SceneNameToBeLoaded";
    /// <summary>フェードするためのマスクとなる Panel (Image) を指定する。アルファを 0 にしておき、必要ならば Raycast Target をオフにしておくこと。</summary>
    [SerializeField] Image m_fadePanel = null;
    /// <summary>フェードするスピード</summary>
    [SerializeField] float m_fadeSpeed = 1f;
    /// <summary>ロード開始フラグ</summary>
    bool m_isLoadStarted = false;
    float Speed = 0.2f;

    void Update()
    {
        if (m_isLoadStarted)
        {
            // m_panel が設定されていたら、徐々にアルファを上げて不透明にする
            if (m_fadePanel)
            {
                m_fadePanel.DOColor(Color.black, m_fadeSpeed).OnComplete(() => SceneManager.LoadScene(m_sceneNameToBeLoaded));
                m_isLoadStarted = false;
                Debug.Log("scene移動完了");
            }
            else
            {
                // m_panel が設定されていない時は、すぐにシーンをロードする
                SceneManager.LoadScene(m_sceneNameToBeLoaded);
                m_isLoadStarted = false;
            }
        }
    }

    /// <summary>
    /// シーンをロードする
    /// </summary>
    public void LoadScene()
    {
        m_isLoadStarted = true;
    }

    /// <summary>
    /// 名前を指定してシーンをロードする
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        m_isLoadStarted = true;
        m_sceneNameToBeLoaded = sceneName;
    }
}
