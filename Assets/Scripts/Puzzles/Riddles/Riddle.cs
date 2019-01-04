using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Riddle", menuName = "Riddle")]
public class Riddle : ScriptableObject {

    public string riddle;
    public string[] answers;
    public int correct;
    [Range(0.1f,2.5f)]
    public float difficulty;
}
