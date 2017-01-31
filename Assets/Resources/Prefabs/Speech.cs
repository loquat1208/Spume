using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour {
    public float LiftTime = 2;
    private string _speech;
    private float _timer;

    private void Awake( ) {
        _timer = 0;
    }

    void Update( ) {
        _timer += Time.deltaTime;
        if ( _timer > LiftTime ) {
            Destroy( gameObject );
        }
        gameObject.GetComponentInChildren<Text>( ).text = _speech;
    }

    public void setSpeech( GameObject layer, string speech ) {
        gameObject.transform.parent = layer.transform;
        _speech = speech;
    }
}
