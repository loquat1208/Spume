using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogManager : MonoBehaviour {
    private Vector3 log_pos;
    private bool log_open;
    private bool _is_selected;
    private OutsideManager _outside_manager;

    void Awake( ) {
        _outside_manager = GameObject.Find( "OutsideLayer" ).gameObject.GetComponent<OutsideManager>( );
    }

    // Use this for initialization
    void Start( ) {
        log_open = false;
    }
	
	// Update is called once per frame
    void Update( ) {
        MoveLog( );
    }

    void MoveLog( ) {
        if ( !log_open ) {
            if( transform.position.x <= -1350.0f ) {
                return;
            }
            log_pos = transform.position;
            log_pos.x -= 20f;
            transform.position = log_pos;
        }
        
        if ( log_open ) {
            if( transform.position.x >= 0 ) {
                return;
            }
            log_pos = transform.position;
            log_pos.x += 20f;
            transform.position = log_pos;
        }
    }

    public bool isLogOpened( ) {
        return log_open;
    }

    public void setLogOpen( bool flag ) {
        log_open = flag;
    }

    public void setSubSelect( bool flag ) {
        _is_selected = flag;
    }

    public void subSelectYes( ) {
        if ( _is_selected ) {
            return;
        }
        if ( !_outside_manager.AllTrue( ) ) {
            return;
        }
        _outside_manager.changeStatusTrue( );
        _is_selected = true;
    }

    public void subSelectNo( ) {
        if ( _is_selected ) {
            return;
        }
        _outside_manager.changeStatusFalse( );
        _is_selected = true;
    }
}
