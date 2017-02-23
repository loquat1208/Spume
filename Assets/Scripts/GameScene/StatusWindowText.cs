using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusWindowText : MonoBehaviour {
    private Characters _characters;
    private GameObject inside_system;
    private GameObject status_window_Face;

    void Start( ) {
        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        inside_system = GameObject.Find( "InsideLayer" ).gameObject;
    }
		
    void Update( ) {
        InsideManager inside_manager = inside_system.GetComponent<InsideManager>( );
		Status status = _characters.getCharacter( inside_manager.getMenu( ) );
		if ( status.getStatus( ).death ) {
			gameObject.GetComponent<Text>( ).text = "";
			return;
		}
		switch( gameObject.name ) {
			case "FoodsText":
				gameObject.GetComponent<Text>( ).text = status.getStatus( ).foods.ToString( );
				break;
			case "WaterText":
				gameObject.GetComponent<Text>( ).text = status.getStatus( ).water.ToString( );
				break;
			case "HealthText":
				gameObject.GetComponent<Text>( ).text = status.getStatus( ).health.ToString( );
				break;
			case "FriendlyText":
				gameObject.GetComponent<Text>( ).text = status.getStatus( ).loyalty.ToString( );
				break;
			case "DiseaseText":
				if ( status.getStatus( ).disease ) {
					gameObject.GetComponent<Text>( ).text = "YES";
				} else {
					gameObject.GetComponent<Text>( ).text = "NO";
				}
				break;
		}
    }
}
