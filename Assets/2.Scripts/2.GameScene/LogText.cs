using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LogText : MonoBehaviour {
    private OutsideManager _outside_manager;
    private LogManager _log_manager;
    private GameSystem _game_system;
    private EventData _event_data;
    private GameObject _log_sub_select;

    void Awake( ) {
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        _event_data = GameObject.Find( "EventSystem" ).gameObject.GetComponent<EventData>( );
        _outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
        _log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
        _log_sub_select = GameObject.Find( "SubSelect" ).gameObject;
    }

    void Start( ) {
        _log_sub_select.SetActive( false );
    }

    void Update( ) {
        //Logが開いてないとUPDATEしない。
        if ( !_log_manager.GetComponent<LogManager>( ).isLogOpened( ) ){
            return;
        }

        //各テキストの内容を書く。
        if ( gameObject.name == "LeftPage" ) {
            gameObject.GetComponent<Text>( ).text = "Sub Story";
        }
        if ( gameObject.name == "LeftDownPage" ) {
            gameObject.GetComponent<Text>( ).text = writeRightPage( );
        }
        if ( gameObject.name == "RightPage" ) {
            updateRightUp( );
        }
        if ( gameObject.name == "RightDownPage" ) {
            updateRightDown( );
        }

        //ACTIVEイベントの時、選択肢を出す。
        if ( ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE ) == 1 ) {
			if ( !_outside_manager.isEventUpdate( ) ) {
                return;
            }
            _log_sub_select.SetActive( true );
        } else {
            _log_sub_select.SetActive( false );
        }
    }

	void updateRightUp( ) {
        //Eventに必要な時間が過ぎていないと表示いない。
        if ( !_outside_manager.isEventUpdate( ) ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        gameObject.GetComponent<Text>( ).text = _event_data.getData( _game_system.randEvent( ), EVENTDATA.STORY ).ToString( );
    }

    void updateRightDown( ) {
        //Eventに必要な時間が過ぎていないと表示いない。
        if ( !_outside_manager.isEventUpdate( ) ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        //イベント化Activeイベントだったら、表示しない。
        if ( ( int )_event_data.getData( _game_system.randEvent( ), EVENTDATA.ACTIVE ) == 1 ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        //イベントの条件を全部満足しないと表示しない。
        if ( !_outside_manager.AllTrue( ) ) {
            gameObject.GetComponent<Text>( ).text = "";
            return;
        }
        gameObject.GetComponent<Text>( ).text = _event_data.getData( _game_system.randEvent( ), EVENTDATA.SUBSTORY ).ToString( );
    }

    string writeRightPage( ) {
        string article = _event_data.getData( _game_system.randEvent( ), EVENTDATA.ARTICLE ).ToString( );
        switch ( article ) {
            case "Island":
                return "島が発見されました。";
            case "Ship":
                return "船が発見されました。";
            default:
                return "何も見つけませんでした。";
        }
    }
}
