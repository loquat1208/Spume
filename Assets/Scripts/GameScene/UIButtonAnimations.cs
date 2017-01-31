using UnityEngine;
using System.Collections;

public class UIButtonAnimations : MonoBehaviour {
    public GameObject page;

    public void PointEnterUP( ) {
        Vector3 pos = transform.position;
        Vector3 page_pos = page.transform.position;
        pos.y += 10;
        page_pos.y += 10;
        transform.position = pos;
        page.transform.position = page_pos;
    }

    public void PointExitDown( ) {
        Vector3 pos = transform.position;
        Vector3 page_pos = page.transform.position;
        pos.y -= 10;
        page_pos.y -= 10;
        transform.position = pos;
        page.transform.position = page_pos;
    }

    public void PointEnterRight( ) {
        Vector3 pos = transform.position;
        Vector3 page_pos = page.transform.position;
        pos.x += 10;
        page_pos.x += 10;
        transform.position = pos;
        page.transform.position = page_pos;
    }

    public void PointExitLeft( ) {
        Vector3 pos = transform.position;
        Vector3 page_pos = page.transform.position;
        pos.x -= 10;
        page_pos.x -= 10;
        transform.position = pos;
        page.transform.position = page_pos;
    }
}
