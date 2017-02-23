using UnityEngine;
using System.Collections;

public class InsideObject : MonoBehaviour {
    [SerializeField]
    private GameObject Light;
	[SerializeField]
	private GameObject Gun;
	[SerializeField]
	private GameObject MediKit;
	[SerializeField]
	private GameObject Rod;
	[SerializeField]
	private GameObject Pot;
	[SerializeField]
	private GameObject RepairTool;

	private GameSystem _game_system;
	private ShipStatus _ship_status;

	void Awake( ) {
		_game_system = GameObject.Find( "GameSystem" ).gameObject.GetComponent<GameSystem>( );
		_ship_status = GameObject.Find( "GameSystem" ).gameObject.GetComponent<ShipStatus>( );
	}

	void Update( ) {
		//insideじゃないとupdateしない。
		if ( _game_system.getLayer( ) != LAYER.INSIDE ) {
			return;
		}

		//medical kitの有無
		if ( _ship_status.getResources( ).medical_kits == 0 ) {
			MediKit.SetActive( false );
		} else {
			MediKit.SetActive( true );
		}

		//gunの有無
		if ( _ship_status.getResources( ).guns == 0 ) {
			Gun.SetActive( false );
		} else {
			Gun.SetActive( true );
		}

		//Rodの有無
		if ( _ship_status.getResources( ).rods == 0 ) {
			Rod.SetActive( false );
		} else {
			Rod.SetActive( true );
		}

		//Potの有無
		if ( _ship_status.getResources( ).pots == 0 ) {
			Pot.SetActive( false );
		} else {
			Pot.SetActive( true );
		}

		//repair toolの有無
		if ( _ship_status.getResources( ).repair_tools == 0 ) {
			RepairTool.SetActive( false );
		} else {
			RepairTool.SetActive( true );
		}
	}

	//insideにある電気
    public void OnOffLight( ) {
        if ( Light.activeSelf ) {
            Light.SetActive( false );
        } else {
            Light.SetActive( true );
        }
    }
}