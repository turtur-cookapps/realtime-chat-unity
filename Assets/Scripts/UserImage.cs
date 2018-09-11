using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserImage : MonoBehaviour
{
    private string url;

	private void Start() {
			
	}

	public void LoadImage(string url) {
		this.url = url;
		if (this.url != "") StartCoroutine("LoadTexture");
	}
    private IEnumerator LoadTexture()
    {
		Texture2D tex = new Texture2D(0, 0);
        using (WWW www = new WWW(url)) {
            yield return www;
            www.LoadImageIntoTexture(tex);
            tex = www.texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0,0,tex.width, tex.height), new Vector2(0.5f,0.5f));
			GetComponent<Image>().sprite = sprite;
        }
    }
}