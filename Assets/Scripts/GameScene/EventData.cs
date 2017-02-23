using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//RandomEvent Data
public enum EVENTDATA {
	NUMBER,
	ARTICLE,
    TIME,
	STORY,
	SUBSTORY,
	ACTIVE,
    AFTERNOON,
    NIGHT,
    MALE,
    FEMALE,
    UPDOWN,
	S_SHIP_FOODS,
	S_SHIP_WATER,
    S_FUEL,
    S_GUNS,
	S_MEDICAL_KITS,
	S_REPAIR_TOOLS,
	S_RODS,
	S_POTS,
	S_DISEASE,
	S_CHARA_FOODS,
	S_CHARA_WATER,
	S_HEALTH,
    F_SHIP_FOODS,
    F_SHIP_WATER,
    F_FUEL,
    F_GUNS,
    F_MEDICAL_KITS,
    F_REPAIR_TOOLS,
    F_RODS,
    F_POTS,
    F_SHIP_BREAK,
    F_DISEASE,
    F_HEALTH,
    T_SHIP_FOODS,
    T_SHIP_WATER,
    T_FUEL,
    T_GUNS,
    T_MEDICAL_KITS,
    T_REPAIR_TOOLS,
    T_RODS,
    T_POTS,
    T_SHIP_BREAK,
    T_DISEASE,
    T_HEALTH,
}
	
public enum CONTENTS {
    NONE,
    ISLAND,
    SHIP,
    CONTENTS_MAX,
}

public class EventData : MonoBehaviour {
	List<Dictionary<string,object>> data;
    private int data_max;

    // Use this for initialization
    void Start ( ) {
		data = CSVReader.Read( "Event_Data" );
        data_max = data.Count;
	}

    public int getMaxData( ) {
        return data_max;
    }

    public object getData( int i, EVENTDATA _event_data ) {
		switch ( _event_data ) {
		    case EVENTDATA.NUMBER:
		    	return data[ i ][ "No" ];
		    case EVENTDATA.ARTICLE:
                return data[ i ][ "Article" ];
            case EVENTDATA.TIME:
                return data[ i ][ "Time" ];
		    case EVENTDATA.STORY:
                return data[ i ][ "Story" ];
            case EVENTDATA.SUBSTORY:
                return data[ i ][ "SubStory" ];
            case EVENTDATA.ACTIVE:
                return data[ i ][ "Active" ];
            case EVENTDATA.AFTERNOON:
                return data[ i ][ "Afternoon" ];
            case EVENTDATA.NIGHT:
                return data[ i ][ "Night" ];
            case EVENTDATA.MALE:
                return data[ i ][ "Male" ];
            case EVENTDATA.FEMALE:
                return data[ i ][ "Female" ];
            case EVENTDATA.UPDOWN:
                return data[ i ][ "UpDown" ];
            case EVENTDATA.S_SHIP_FOODS:
                return data[ i ][ "S_ShipFoods" ];
            case EVENTDATA.S_SHIP_WATER:
                return data[ i ][ "S_ShipWater" ];
            case EVENTDATA.S_FUEL:
                return data[ i ][ "S_Fuel" ];
            case EVENTDATA.S_GUNS:
                return data[ i ][ "S_Gun" ];
            case EVENTDATA.S_MEDICAL_KITS:
                return data[ i ][ "S_MedicalKit" ];
            case EVENTDATA.S_REPAIR_TOOLS:
                return data[ i ][ "S_RepairTool" ];
            case EVENTDATA.S_RODS:
                return data[ i ][ "S_Rod" ];
            case EVENTDATA.S_POTS:
                return data[ i ][ "S_Pot" ];
            case EVENTDATA.S_DISEASE:
                return data[ i ][ "S_Disease" ];
            case EVENTDATA.S_CHARA_FOODS:
                return data[ i ][ "S_CharaFoods" ];
            case EVENTDATA.S_CHARA_WATER:
                return data[ i ][ "S_CharaWater" ];
            case EVENTDATA.S_HEALTH:
                return data[ i ][ "S_Health" ];
            case EVENTDATA.F_SHIP_FOODS:
                return data[ i ][ "F_ShipFoods" ];
            case EVENTDATA.F_SHIP_WATER:
                return data[ i ][ "F_ShipWater" ];
            case EVENTDATA.F_FUEL:
                return data[ i ][ "F_Fuel" ];
            case EVENTDATA.F_GUNS:
                return data[ i ][ "F_Gun" ];
            case EVENTDATA.F_MEDICAL_KITS:
                return data[ i ][ "F_MedicalKit" ];
            case EVENTDATA.F_REPAIR_TOOLS:
                return data[ i ][ "F_RepairTool" ];
            case EVENTDATA.F_RODS:
                return data[ i ][ "F_Rod" ];
            case EVENTDATA.F_POTS:
                return data[ i ][ "F_Pot" ];
            case EVENTDATA.F_SHIP_BREAK:
                return data[ i ][ "F_ShipBreak" ];
            case EVENTDATA.F_DISEASE:
                return data[ i ][ "F_Disease" ];
            case EVENTDATA.F_HEALTH:
                return data[ i ][ "F_Health" ];
            case EVENTDATA.T_SHIP_FOODS:
                return data[ i ][ "T_ShipFoods" ];
            case EVENTDATA.T_SHIP_WATER:
                return data[ i ][ "T_ShipWater" ];
            case EVENTDATA.T_FUEL:
                return data[ i ][ "T_Fuel" ];
            case EVENTDATA.T_GUNS:
                return data[ i ][ "T_Gun" ];
            case EVENTDATA.T_MEDICAL_KITS:
                return data[ i ][ "T_MedicalKit" ];
            case EVENTDATA.T_REPAIR_TOOLS:
                return data[ i ][ "T_RepairTool" ];
            case EVENTDATA.T_RODS:
                return data[ i ][ "T_Rod" ];
            case EVENTDATA.T_POTS:
                return data[ i ][ "T_Pot" ];
            case EVENTDATA.T_SHIP_BREAK:
                return data[ i ][ "T_ShipBreak" ];
            case EVENTDATA.T_DISEASE:
                return data[ i ][ "T_Disease" ];
            case EVENTDATA.T_HEALTH:
                return data[ i ][ "T_Health" ];
        }
        return "";
	}

    public CONTENTS getContent( int i ) {
        switch ( ( string ) data[ i ][ "Article" ] ) {
            case "Island":
			    return CONTENTS.ISLAND;
		    case "Ship":
                return CONTENTS.SHIP;
            default:
                return CONTENTS.NONE;
		}
    }
}
