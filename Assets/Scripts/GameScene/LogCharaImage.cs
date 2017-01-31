using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Logにある各キャラのImage管理
public class LogCharaImage : MonoBehaviour {

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
				if ( _characters.getCharacter( 1 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
			case "DeathMark2":
				if ( _characters.getCharacter( 2 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
			case "DeathMark3":
				if ( _characters.getCharacter( 3 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
			case "DeathMark4":
				if ( _characters.getCharacter( 4 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
			case "DeathMark5":
				if ( _characters.getCharacter( 5 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
			case "DeathMark6":
				if ( _characters.getCharacter( 6 ).getStatus( ).death ) {
					gameObject.GetComponent<Image>( ).enabled = true;
				} else { 
					gameObject.GetComponent<Image>( ).enabled = false;
				}
				break;
		}
	}
}
