using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutManager : MonoBehaviour {
    public List<GameObject> Cut;

    private GameObject _fade;
    private int _now_cut;
    private float _speed;

    private void Awake( ) {
        _fade = GameObject.Find( "Fade" ).gameObject;
        _now_cut = 0;
        _speed = 1f;
    }

    private void Start( ) {
        Cut[ _now_cut ].GetComponent<IntroCut>( ).play( );
    }

    void Update( ) {
        if ( !Cut[ _now_cut ].GetComponent<IntroCut>( ).isPlay( ) ) {
            StartCoroutine( "FadeIn" );
        }
    }

    IEnumerator FadeIn( ) {
        for ( float i = 0; i <= 180; i += 1f ) {
            if ( i == 90 ) {
                Cut[ _now_cut ].GetComponent<IntroCut>( ).play( );
                if ( _now_cut >= 1 ) {
                    Cut[ _now_cut - 1 ].SetActive( false );
                }
            }
            _fade.GetComponentInChildren<Image>( ).color = new Color( 0, 0, 0, Mathf.Sin( i * Mathf.PI / 180f ) );
            yield return 0;
        }
    }

    public void NextCut( ) {
        _now_cut++;
    }

    public float getSpeed( ) {
        return _speed;
    }

    public void skipButton( ) {
        if ( _now_cut < 7 ) {
            _speed = 10f;
        }
    }
}
