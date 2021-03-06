using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    bool w, a, s, d;

    RollMovementBAse character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<RollMovementBAse>();
    }

    Vector2 getDir(Vector2 start)
    {
        Vector2 v = Vector2.zero;
        if (w) v.y += 1;
        if (s) v.y -= 1;
        if (d) v.x += 1;
        if (a) v.x -= 1;
        if (v == Vector2.zero) v = start;
        return v;
    }

    private void FixedUpdate()
    {
        character.move(getDir(Vector2.zero));
    }

    // Update is called once per frame
    void Update()
    {
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);
        if (Input.GetMouseButtonDown(1))
        {
            character.startRoll(getDir(Vector2.one));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("S1");
        }
    }
}
