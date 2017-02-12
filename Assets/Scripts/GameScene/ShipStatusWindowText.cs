using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipStatusWindowText : MonoBehaviour {
    private ShipStatus _ship_status;

    // Use this for initialization
    void Start ( ) {
        _ship_status = GameObject.Find( "GameSystem" ).gameObject.GetComponent<ShipStatus>( );
    }
	
	// Update is called once per frame
	void Update ( ) {

        gameObject.GetComponent<Text>( ).text =
                   ": " +  _ship_status.getResources( ).fuels.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).foods.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).water.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).guns.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).medical_kits.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).rods.ToString( ) + "\n"
                   + ": " + _ship_status.getResources( ).pots.ToString( ) + "\n";
    }
}
