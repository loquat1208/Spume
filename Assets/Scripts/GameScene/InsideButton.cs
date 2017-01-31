using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InsideButton : MonoBehaviour {
    [SerializeField]
    private GameObject Plus1;
    [SerializeField]
    private GameObject Speech;

    private GameSystem _game_system;
    private ShipStatus _ship_status;
    private GameObject menus;
    private InsideManager inside_manager;
	private OutsideManager outside_manager;
    private GameObject outside_chara;
    private Characters chara_manager;

    void Awake( ) {
        menus = GameObject.Find( "InsideMenus" ).gameObject;
        _game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
        _ship_status = _game_system.GetComponent<ShipStatus>( );
        chara_manager = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        inside_manager = GameObject.Find( "InsideLayer" ).gameObject.GetComponent<InsideManager>( );
		outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
		outside_chara = GameObject.Find( "Character" ).gameObject;
    }

    public void FoodsButtonEvent( ) {
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        if ( _ship_status.getResources( ).foods <= 0 ) {
            //吹き出し
            Vector3 speech_pos = status.getPos( ) + new Vector3( -50, 200, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( inside_manager.gameObject, "残った食糧がない。" );
            return;
        }
		//+1
        Vector3 pos = status.getPos( ) + new Vector3( 0, 100, 0 );
		GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
		_plus.transform.parent = inside_manager.transform;

		_ship_status.setFoods( _ship_status.getResources( ).foods - 1 );
        status.setFoods( status.getStatus( ).foods + 1 );
    }

    public void WaterButtonEvent( ) {
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        if ( _ship_status.getResources( ).water <= 0 ) {
            //吹き出し
            Vector3 speech_pos = status.getPos( ) + new Vector3( -40, 200, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( inside_manager.gameObject, "水はもうないよ。" );
            return;
        }
		//+1
		Vector3 pos = status.getPos( ) + new Vector3( 0, 100, 0 );
		GameObject _plus = Instantiate( Plus1, pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
		_plus.transform.parent = inside_manager.transform;

		_ship_status.setWater( _ship_status.getResources( ).water - 1 );
        status.setWater( status.getStatus( ).water + 1 );
    }

    public void OutsideButtonEvent( ) {
		if ( outside_manager.getOutsideState( ) != OUTSIDE_STATE.NONE ) {
			return;
		}
		for ( int i = 1; i < 7; i++ ) {
			chara_manager.getCharacter( i ).setPlace( LAYER.INSIDE );
		}
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        LayerChangeButton layer_change_button = GameObject.Find( "LayerChangeButton" ).gameObject.GetComponent<LayerChangeButton>( );
        layer_change_button.setImageInside( );
        menus.transform.position = new Vector3( 1600.0f, 0.0f, 0.0f );
        status.setPlace( LAYER.OUTSIDE );
        _game_system.setLayer( LAYER.OUTSIDE );
        inside_manager.gameObject.transform.position = new Vector3( 1600.0f, 0.0f, 0.0f );
		outside_manager.gameObject.transform.position = new Vector3( 0.0f, 0.0f, 0.0f );
        outside_chara.SetActive( true );
    }

    public void CureButtonEvent( ) {
        Status status = chara_manager.getCharacter( inside_manager.getMenu( ) );
        if ( !status.getStatus( ).disease ) {
            //吹き出し
            Vector3 speech_pos = status.getPos( ) + new Vector3( -50, 200, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( inside_manager.gameObject, "痛いところがない。" );
            return;
        }
        if ( _ship_status.getResources( ).medical_kits <= 0 ) {
            //吹き出し
            Vector3 speech_pos = status.getPos( ) + new Vector3( -50, 200, 0 );
            GameObject _speech = Instantiate( Speech, speech_pos, new Quaternion( 0, 0, 0, 0 ) ) as GameObject;
            _speech.GetComponent<Speech>( ).setSpeech( inside_manager.gameObject, "医療キットがないんだ。" );
            return;
        }
        _ship_status.setMedicalKits( _ship_status.getResources( ).medical_kits - 1 );
        status.setDisease( false );
    }
}
