using UnityEngine;
using System.Collections;

public class InsideCharacterMenu : MonoBehaviour {
    private GameObject inside_system;
    private InsideManager inside_manager;
    private GameObject menus;

    void Awake( ) {
		inside_system = GameObject.Find( "InsideLayer" ).gameObject;
		menus = GameObject.Find( "InsideMenus" ).gameObject;
	}

    void Start( ) {
        inside_manager = inside_system.GetComponent<InsideManager>( );
    }

	void Update ( ) {

    }

    public void CharacterClickEvent( ) {
        menus.transform.position = gameObject.GetComponent<Status>( ).getMenuPos( );
        for ( int i = 1; i < 7; i++ ) {
            if ( gameObject.name == "Chara" + i.ToString( ) ) {
                inside_manager.setMenu( i );
            }
        }
    }
}
