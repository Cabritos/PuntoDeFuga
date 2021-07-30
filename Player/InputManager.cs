using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Vertical { get; private set; }
    public static float Horizontal { get; private set; }
    public static float VerticalCamera { get; private set; }
    public static float HorizontalCamera { get; private set; }

    [SerializeField] private static float _cooldownRate = 0.2f;
    private static float _nextPress;

    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");

        var vc = 0f;
        vc += Input.GetAxis("VerticalCamera");
        vc += Input.GetAxis("VerticalCameraJoystick");
        VerticalCamera = Mathf.Clamp(vc, -1f, 1f);

        var hc = 0f;
        hc += Input.GetAxis("HorizontalCamera");
        hc += Input.GetAxis("HorizontalCameraJoystick");
        HorizontalCamera = Mathf.Clamp(hc, -1f, 1f);
    }

    public static bool Action()
    {
       return (Input.GetButtonDown("Action"));
    }

    public static bool Enter()
    {
        return (Input.GetKeyDown(KeyCode.Return));
    }

    public static bool Cancel()
    {
        return Input.GetButtonDown("Cancel");
    }

    public static bool Pause()
    {
        return Input.GetButtonDown("Pause");
    }

    public static bool Up()
    {
        if (Vertical > 0.3 && Time.time > _nextPress)
        {
            _nextPress = Time.time + _cooldownRate;
            return true;
        }

        return Input.GetKeyDown("w");
    }

    public static bool Ups()
    {
        return Up() || Input.GetKeyDown(KeyCode.UpArrow);
    }

    public static bool Down()
    {
        if (Vertical < -0.3 && Time.time > _nextPress)
        {
            _nextPress = Time.time + _cooldownRate;
            return true;
        }

        return Input.GetKeyDown("s");
    }

    public static bool Downs()
    {
        return Down() || Input.GetKeyDown(KeyCode.DownArrow);
    }

    public static bool Left()   
    {
        if (Horizontal < -0.3 && Time.time > _nextPress)
        {
            _nextPress = Time.time + _cooldownRate;
            return true;
        }

        return Input.GetKeyDown("a");
    }

    public static bool Right()
    {
        if (Horizontal > 0.3 && Time.time > _nextPress)
        {
            _nextPress = Time.time + _cooldownRate;
            return true;
        }

        return Input.GetKeyDown("d");
    }
}
