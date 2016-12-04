using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusWindowText : MonoBehaviour {
    private Characters _characters;
    private GameObject inside_system;

    // Use this for initialization
    void Start( ) {
        _characters = GameObject.Find( "Characters" ).gameObject.GetComponent<Characters>( );
        inside_system = GameObject.Find( "InsideLayer" ).gameObject;
    }

    // Update is called once per frame
    void Update( ) {
        InsideManager inside_manager = inside_system.GetComponent<InsideManager>( );
        Status status = _characters.getCharacter( inside_manager.getMenu( ) );
        if ( gameObject.name == "StatusRight" ) {
            gameObject.GetComponent<Text>( ).text = writeStatus( status );
        }

        if ( gameObject.name == "StatusLeft" ) {
            switch ( inside_manager.getMenu( ) ) {
                case 1:
                    gameObject.GetComponent<Text>( ).text = "キャラー１の説明";
                    break;
                case 2:
                    gameObject.GetComponent<Text>( ).text = "キャラー２の説明";
                    break;
                case 3:
                    gameObject.GetComponent<Text>( ).text = "キャラー３の説明";
                    break;
                case 4:
                    gameObject.GetComponent<Text>( ).text = "キャラー４の説明";
                    break;
                case 5:
                    gameObject.GetComponent<Text>( ).text = "キャラー５の説明";
                    break;
                case 6:
                    gameObject.GetComponent<Text>( ).text = "キャラー６の説明";
                    break;
            }
        }
    }

    string writeStatus( Status character ) {
        if ( character.getStatus( ).death ) {
            return "";
        }
        string status_log;
        status_log = "STATUS\n\n"
                   + "  Foods " + character.getStatus( ).foods.ToString( ) + "\n"
                   + "  Water " + character.getStatus( ).water.ToString( ) + "\n"
                   + "  Health " + character.getStatus( ).health.ToString( ) + "\n"
                   + "  Loyalty " + character.getStatus( ).loyalty.ToString( ) + "\n"
                   + "  Disease " + character.getStatus( ).disease.ToString( ) + "\n";
        return status_log;
    }
}
