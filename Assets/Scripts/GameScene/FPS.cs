using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {
    private float deltaTime = 0.0f;
    private float msec;
    private float fps;
    private string text;

    void Update( ) {
        deltaTime += ( Time.deltaTime - deltaTime ) * 0.1f;
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        text = string.Format( "{0:0.0} ms ({1:0.} fps)", msec, fps );
        gameObject.GetComponent<Text>( ).text = text;
    }
}