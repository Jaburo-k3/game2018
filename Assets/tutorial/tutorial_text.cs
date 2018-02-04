using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tutorial_text : MonoBehaviour
{
    public string[] scenarios;
    [SerializeField]
    Text text;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;  // 1文字の表示にかかる時間

    private int currentLine = 0;
    public string currentText = string.Empty;  // 現在の文字列
    private float timeUntilDisplay = 0;     // 表示にかかる時間
    private float timeElapsed = 1;          // 文字列の表示を開始した時間
    private int lastUpdateCharacter = -1;       // 表示中の文字数


    public int displayCharacterCount;


    public string get_currentText() {
        return currentText;
    }

    void Start()
    {
        SetNextLine();
    }

    void Update()
    {
        /*
        if (currentLine < scenarios.Length)
        {
            SetNextLine();
        }
        */

        // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
        displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);

        // 表示文字数が前回の表示文字数と異なるならテキストを更新する
        if (displayCharacterCount != lastUpdateCharacter)
        {
            text.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }


    public void SetNextLine()
    {
        //Debug.Log("line");
        if (currentLine < scenarios.Length)
        {
            currentText = scenarios[currentLine];
            currentLine++;

            // 想定表示時間と現在の時刻をキャッシュ
            timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
            timeElapsed = Time.time;

            // 文字カウントを初期化
            lastUpdateCharacter = -1;
        }
    }
}