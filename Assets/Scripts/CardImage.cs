using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardImage : MonoBehaviour
{
    private const string URL = "https://picsum.photos/200/300";
    

    private void Awake()
    {
        var image = GetComponent<Image>();
        StartCoroutine(LoadTextureFromServer(image));
    }

    IEnumerator LoadTextureFromServer(Image image)
    {
        var request = UnityWebRequestTexture.GetTexture(URL);

    yield return request.SendWebRequest();

    if (!request.isHttpError && !request.isNetworkError)
    {
        var response = DownloadHandlerTexture.GetContent(request);
        image.sprite = Sprite.Create(response, new Rect(0.0f, 0.0f, response.width, response.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
    else
    {
    	Debug.LogErrorFormat("error request [{0}, {1}]", URL, request.error);
    }
    request.Dispose();
}
}