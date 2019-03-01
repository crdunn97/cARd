using UnityEngine;
using System.Collections;

public class TextureURL : MonoBehaviour
{
    public string url = DatabaseReciever.image;

    IEnumerator Start()
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        using (WWW www = new WWW(url))
        {
            yield return www;
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }
}