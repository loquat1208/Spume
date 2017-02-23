using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Logにある各キャラのImage管理
public class LogCharaImage : MonoBehaviour {
	public Sprite Death;
	public Sprite Sick;
	private LogManager _log_manager;
	private Characters _characters;

	void Awake( ) {
		_log_manager = GameObject.Find( "Log" ).gameObject.GetComponent<LogManager>( );
		_characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
	}

	void Update( ) {
		//Logが開いていないとUpdateしない
		if ( !_log_manager.isLogOpened( ) ) {
			return;
		}
		switch ( gameObject.name ) {
			case "DeathMark1":
				setImage( 1 );
				break;
			case "DeathMark2":
				setImage( 2 );
				break;
			case "DeathMark3":
				setImage( 3 );
				break;
			case "DeathMark4":
				setImage( 4 );
				break;
			case "DeathMark5":
				setImage( 5 );
				break;
			case "DeathMark6":
				setImage( 6 );
				break;
		}
	}

	void setImage( int num ) {
		if ( _characters.getCharacter( num ).getStatus( ).disease ) {
			gameObject.GetComponent<Image>( ).sprite = Sick;
			gameObject.GetComponent<Image>( ).enabled = true;
		} else {
			gameObject.GetComponent<Image>( ).enabled = false;
		}
		if ( _characters.getCharacter( num ).getStatus( ).death ) {
			gameObject.GetComponent<Image>( ).sprite = Death;
			gameObject.GetComponent<Image>( ).enabled = true;
		}
	}
}
