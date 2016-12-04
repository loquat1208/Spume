using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
    GameObject message;

    void Awake( ) {
        message = GameObject.Find( "Text" ).gameObject;
        if ( PlayerPrefs.GetInt( "GameOver" ) == 0 ) {
            message.GetComponent<Text>( ).text = "一人ぐらいは選んでくれよ。";
        } else {
            message.GetComponent<Text>( ).text = "GAME OVER";
        }
    }
    // Update is called once per frame
    void Update( ) {
        if ( Input.GetMouseButtonUp( 0 ) ) {
            SceneManager.LoadScene( "TitleScene" );
        }

    }

}
