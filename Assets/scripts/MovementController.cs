using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    bool w, a, s, d;

    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        Vector2 v = Vector2.zero;
        if (w) v.y += 1;
        if (s) v.y -= 1;
        if (d) v.x += 1;
        if (a) v.x -= 1;
        character.move(v);
    }

    // Update is called once per frame
    void Update()
    {
        w = Input.GetKey(KeyCode.W);
        a = Input.GetKey(KeyCode.A);
        s = Input.GetKey(KeyCode.S);
        d = Input.GetKey(KeyCode.D);
    }
}
