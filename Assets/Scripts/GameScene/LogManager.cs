using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {
    [SerializeField]
    private GameObject Speech;
    private bool log_open;
    private bool _is_selected;
    private OutsideManager _outside_manager;

    void Awake( ) {
        _outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
    }

    void Start( ) {
        log_open = false;
    }

	//Logを位置を管理
    public void MoveLog( ) {
        if ( !log_open ) {
            gameObject.SetActive( false );
        }
        if ( log_open ) {
            transform.position = new Vector3( 0, 0, 0 );
            gameObject.SetActive( true );
        }
    }

	//Logが開いてるのかをとる
    public bool isLogOpened( ) {
        return log_open;
    }

	//Logを開く、閉める
    public void setLogOpen( bool flag ) {
        log_open = flag;
        MoveLog( );
    }

	//RandomEventに選択肢を出す、出さない
    public void setSubSelect( bool flag ) {
        _is_selected = flag;
    }

	//選択肢のYes
    public void subSelectYes( ) {
        if ( _is_selected ) {
            return;
        }
		//条件を満足しなかった時、吹き出しを出してreturn
        if ( !_outside_manager.AllTrue( ) ) {
            Vector3 speech_pos = new Vector3( 165, -40, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( gameObject, "条件を満足していない。" );
            return;
        }
        _outside_manager.changeStatusTrue( );
        _is_selected = true;
        gameObject.SetActive( false );
        log_open = false;
    }

	//選択肢のNo
    public void subSelectNo( ) {
        if ( _is_selected ) {
            return;
        }
        _outside_manager.changeStatusFalse( );
        _is_selected = true;
        gameObject.SetActive( false );
        log_open = false;
    }
}
