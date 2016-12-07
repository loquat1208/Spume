using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1Controller : MonoBehaviour {
    public GameObject Camera;
    public float CutMoveSpeed;
    public List<Movie1Cut> Cut;
    public List<Vector3> CutCameraPos;
    public List<float> CutPlayTime;

    private float _timer;
    private int _cut_num;
    private int _select;
    private int _next_cut;

    void Start( ) {
        _select = PlayerPrefs.GetInt( "Select" );
        _timer = 0;
        _cut_num = 0;
        _next_cut = 1;
    }

    void Update( ) {
        if ( Cut.Count <= _cut_num ) {
            return;
        }
        Cut[ _cut_num ].play( );
        _timer += Time.deltaTime;
        cameraMove( _cut_num, _cut_num + _next_cut, CutPlayTime[ _cut_num ] );
    }

    void cameraMove( int start_cut, int end_cut, float end_time ) {
        if ( Cut.Count <= end_cut ) {
            return;
        }
        Vector3 start_pos = Cut[ start_cut ].transform.position - CutCameraPos[ start_cut ];
        Vector3 end_pos = Cut[ end_cut ].transform.position - CutCameraPos[ end_cut ];
        Debug.Log( ( _timer - end_time ) * CutMoveSpeed );
        if ( ( _timer - end_time ) * CutMoveSpeed > 0 ) {
            Camera.transform.position = Vector3.Lerp( start_pos, end_pos, ( _timer - end_time ) * CutMoveSpeed );
        }
        if ( ( _timer - end_time ) * CutMoveSpeed > 1 ) {
            Cut[ start_cut ].stop( );
            _cut_num += _next_cut;
            _next_cut = 1;
            _timer = 0;
        }
    }

    public void Select1( ) {
        int event_num = PlayerPrefs.GetInt( "EventNumber" );
        _select += ( int )Mathf.Pow( 2.0f, ( float )event_num );
        PlayerPrefs.SetInt( "Select", _select );
        PlayerPrefs.Save( );
        cameraMove( _cut_num, _cut_num + _next_cut, 0 );
    }

    public void Select2( ) {
        _next_cut = 2;
        cameraMove( _cut_num, _cut_num + _next_cut, 0 );
    }
}
