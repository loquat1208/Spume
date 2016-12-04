using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipStatusWindowText : MonoBehaviour {
    private ShipStatus ship_status;
    // Use this for initialization
    void Start ( ) {
        ship_status = GameObject.Find( "Ship" ).gameObject.GetComponent<ShipStatus>( );
    }
	
	// Update is called once per frame
	void Update ( ) {
        gameObject.GetComponent<Text>( ).text =
                      "Fuels          : " + ship_status.getResources( ).fuels.ToString( ) + "\n"
                    + "Foods          : " + ship_status.getResources( ).foods.ToString( ) + "\n"
                    + "Water          : " + ship_status.getResources( ).water.ToString( ) + "\n"
                    + "Medical Kits   : " + ship_status.getResources( ).medical_kits.ToString( ) + "\n"
                    + "Repair Tools   : " + ship_status.getResources( ).repair_tools.ToString( ) + "\n"
                    + "Rods           : " + ship_status.getResources( ).rods.ToString( ) + "\n"
                    + "Pots           : " + ship_status.getResources( ).pots.ToString( ) + "\n";
    }
}
