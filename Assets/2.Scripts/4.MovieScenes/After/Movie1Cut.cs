using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Movie1Cut : MonoBehaviour {
    private bool _play;
    private float _timer;

    void Start( ) {
        _play = false;
    }

    void Update( ) {
        if ( !_play ) {
            _timer = 0;
            return;
        }
        _timer += Time.deltaTime;
        Cut( transform.name );
    }

    void Cut( string cut_name ) {
        switch ( cut_name ) {
            case "Cut1":
                GameObject.Find( "Icon1_2" ).gameObject.transform.position += new Vector3( 0.75f, 1f, 0 ) * 0.004f;
                if ( _timer > 1f ) {
                    GameObject.Find( "Icon1_1" ).gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, _timer / 4f - 1f );
                    GameObject.Find( "Icon1_2" ).gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, 2f - _timer / 4f );
                }
                break;
            case "Cut2":
                gameObject.GetComponent<SpriteRenderer>( ).color = new Color( 255, 255, 255, _timer / 2f );
                Color Icon2_1 = new Color( 255, 255, 255, Mathf.Sin( _timer / 4f * Mathf.PI ) );
                Color Icon2_2 = new Color( 255, 255, 255, _timer / 2f - 2f );
                GameObject.Find( "Icon2_1" ).gameObject.GetComponent<SpriteRenderer>( ).color = Icon2_1;
                GameObject.Find( "Icon2_2" ).gameObject.GetComponent<SpriteRenderer>( ).color = Icon2_2;
                break;
            case "Cut3":
                Vector3 Icon3_1 = new Vector3( 0, 0, 0 );
                GameObject.Find( "Icon3_1" ).gameObject.transform.position += Icon3_1;
                Color Icon3_2 = new Color( 255, 255, 255, Mathf.Sin( _timer / 4f * Mathf.PI ) );
                Color Icon3_3 = new Color( 255, 255, 255, _timer / 2f - 2f );
                GameObject.Find( "Icon3_2" ).gameObject.GetComponent<SpriteRenderer>( ).color = Icon3_2;
                GameObject.Find( "Icon3_3" ).gameObject.GetComponent<SpriteRenderer>( ).color = Icon3_3;
                break;
        }
    }

    public void play( ) {
        _play = true;
    }

    public void stop( ) {
        _play = false;
    }
}
