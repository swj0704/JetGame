using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenualScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeScene(){
        SceneManager.LoadScene("ShowMenual");
    }
}
