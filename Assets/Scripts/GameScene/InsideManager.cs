using UnityEngine;
using System.Collections;

public class InsideManager : MonoBehaviour {
    private int _menu;
    private int _status_window;

    public int getMenu( ) {
        return _menu;
    }

    public void setMenu( int menu ) {
        _menu = menu;
    }

    public int getStatusWindow( ) {
        return _status_window;
    }

    public void setStatusWindow( int status_window ) {
        _status_window = status_window;
    }
}