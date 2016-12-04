using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InsideButton : MonoBehaviour {
    [SerializeField]
    private GameObject Plus1;
    [SerializeField]
    private List<Vector3> PlusPos;

    private GameObject status_window;
    private GameObject status_window_Face;
    private GameObject log;
    private GameSystem _game_system;
    private ShipStatus ship_status;
    private GameObject menus;
	private InsideManager inside_manager;
	private OutsideManager outside_manager;
    private Characters chara_manager;

    void Awake( ) {
        status_window = GameObject.Find( "StatusWindow" ).gameObject;
        status_window_Face = GameObject.Find( "Face" ).gameObject;
        menus = GameObject.Find( "InsideMenus" ).gameObject;
        log = GameObject.Find( "Log" ).gameObject;
        ship_status = GameObject.Find( "Ship" ).gameObject.GetComponent<ShipStatus>( );
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        chara_manager = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        inside_manager = GameObject.Find( "InsideLayer" ).gameObject.GetComponent<InsideManager>( );
		outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
    }

	void Start ( ) {
        status_window.SetActive( false );
    }

    public void FoodsButtonEvent( ) {
        if ( ship_status.getResources( ).foods <= 0 ) {
            return;
        }
        ship_status.setFoods( ship_status.getResources( ).foods - 1 );
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        Vector3 pos = status.getPos( ) + new Vector3( 0, 100, 0 );
		GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
		_plus.transform.parent = inside_manager.transform;
        status.setFoods( status.getStatus( ).foods + 1 );
    }

    public void WaterButtonEvent( ) {
        if ( ship_status.getResources( ).water <= 0 ) {
            return;
        }
        ship_status.setWater( ship_status.getResources( ).water - 1 );
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
		Vector3 pos = status.getPos( ) + new Vector3( 0, 100, 0 );
		GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
		_plus.transform.parent = inside_manager.transform;
        status.setWater( status.getStatus( ).water + 1 );
    }

    public void StatusButtonEvent( ) {
        if ( log.GetComponent<LogManager>( ).isLogOpened( ) ) {
            return;
        }
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        status_window_Face.GetComponent<Image>( ).sprite = status.getStatusWindowImage( );
        if ( status_window.activeSelf ) {
            status_window.SetActive( false );
        } else {
            status_window.transform.position = new Vector3( 0, 0, 0 );
            status_window.SetActive( true );
        }
    }

    public void OutsideButtonEvent( ) {
		if ( outside_manager.getOutsideState( ) != OUTSIDE_STATE.NONE ) {
			return;
		}
		for ( int i = 1; i < 7; i++ ) {
			chara_manager.getCharacter( i ).setPlace( LAYER.INSIDE );
		}
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        menus.transform.position = new Vector3( 1600.0f, 0.0f, 0.0f );
        status.setPlace( LAYER.OUTSIDE );
		_game_system.setLayer( LAYER.OUTSIDE );
    }

    public void CureButtonEvent( ) {
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        if ( !status.getStatus( ).disease ) {
            return;
        }
        if ( ship_status.getResources( ).medical_kits <= 0 ) {
            return;
        }
        ship_status.setMedicalKits( ship_status.getResources( ).medical_kits - 1 );
        status.setDisease( false );
    }
}
