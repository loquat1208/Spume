using UnityEngine;
using System.Collections;

public class InsideManager : MonoBehaviour {
    private int _menu;

    // Use this for initialization
    void Awake( ) {

    }

    public int getMenu( ) {
        return _menu;
    }

    public void setMenu( int menu ) {
        _menu = menu;
    }
}