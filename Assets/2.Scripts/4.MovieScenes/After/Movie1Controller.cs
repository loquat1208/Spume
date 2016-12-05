using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie1Controller : MonoBehaviour {

    //CutPlayTime >> 9, 8, 8;

    public GameObject Camera;
    public List<Movie1Cut> Cut;
    public List<Vector3> CutCameraPos;
    public List<float> CutPlayTime;
    public List<float> CutMoveSpeed;

    private float _timer;
    private int _cut_num;

    void Start( ) {
        _timer = 0;
        _cut_num = 0;
    }

    void Update( ) {
        if ( Cut.Count <= _cut_num ) {
            return;
        }
        Cut[ _cut_num ].play( );
        _timer += Time.deltaTime;
        cameraMove( _cut_num, _cut_num + 1, CutPlayTime[ _cut_num ] );
    }

    void cameraMove( int start_cut, int end_cut, float end_time ) {
        if ( Cut.Count <= end_cut ) {
            return;
        }
        Vector3 start_pos = Cut[ start_cut ].transform.position - CutCameraPos[ start_cut ];
        Vector3 end_pos = Cut[ end_cut ].transform.position - CutCameraPos[ end_cut ];
        Camera.transform.position = Vector3.Lerp( start_pos, end_pos, ( _timer - end_time ) * CutMoveSpeed[ start_cut ] );
        if ( ( _timer - end_time ) * CutMoveSpeed[ start_cut ] > 1 ) {
            Cut[ start_cut ].stop( );
            _cut_num++;
            _timer = 0;
        } 
    }
}
