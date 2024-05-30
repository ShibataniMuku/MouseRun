using UnityEngine;
using UnityEngine.Networking;

public class ShareButtonModel : MonoBehaviour
{
    [SerializeField, Header("本文"), Multiline]
    private string mainMessage = "";
    [SerializeField, Header("ハッシュタグ")]
    private string hashTag = "";

    public void OnClickShareButton()
    {
        //urlの作成
        string esctext = UnityWebRequest.EscapeURL(mainMessage);
        string esctag = UnityWebRequest.EscapeURL(hashTag);
        string url = "https://twitter.com/intent/tweet?text=" + esctext + "&hashtags=" + esctag;

        //Twitter投稿画面の起動
        Application.OpenURL(url);
    }
}
