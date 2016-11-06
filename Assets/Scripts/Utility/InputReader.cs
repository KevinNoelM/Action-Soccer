using UnityEngine;
using System.Collections;


public class InputReader : MonoBehaviour
{
    //
    static InputReader instance;
    //
    static public KeyCode keyPlayer1Fire = KeyCode.T;
    static public KeyCode keyPlayer2Fire = KeyCode.Keypad3;
    static public KeyCode keyPlayer1Jump = KeyCode.G;
    static public KeyCode keyPlayer2Jump = KeyCode.Keypad2;
    static public KeyCode keyPlayer1Kick = KeyCode.F;
    static public KeyCode keyPlayer2Kick = KeyCode.Keypad1;
    static public KeyCode keyPlayer2Up = KeyCode.UpArrow;
    static public KeyCode keyPlayer1Up = KeyCode.W;
    static public KeyCode keyPlayer2Down = KeyCode.DownArrow;
    static public KeyCode keyPlayer1Down = KeyCode.S;
    static public KeyCode keyPlayer2Left = KeyCode.LeftArrow;
    static public KeyCode keyPlayer1Left = KeyCode.A;
    static public KeyCode keyPlayer2Right = KeyCode.RightArrow;
    static public KeyCode keyPlayer1Right = KeyCode.D;
    static public KeyCode keyPlayer1Dash = KeyCode.LeftShift;
    static public KeyCode keyPlayer2Dash = KeyCode.RightControl;
    //
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //
    private static bool IsBlocked
    {
        get
        {
            return false;
        }
    }
    //
    public static void GetInput(int controllerNum, ref bool up, ref bool down, ref bool left, ref bool right, ref bool fire, ref bool jump, ref bool dash, ref bool kick)
    {
        up = down = left = right = fire = jump = dash = kick = false;
        // 
        if (controllerNum == 0)
        {
            GetKeyboard1Input(ref up, ref down, ref left, ref right, ref fire, ref jump, ref dash, ref kick);
            GetXBoxControllerInput("_1", ref up, ref down, ref left, ref right, ref fire, ref jump, ref dash, ref kick);
        }
        if (controllerNum == 1)
        {
            GetKeyboard2Input(ref up, ref down, ref left, ref right, ref fire, ref jump, ref dash, ref kick);
            GetXBoxControllerInput("_2", ref up, ref down, ref left, ref right, ref fire, ref jump, ref dash, ref kick);
        }
        // 
    }
    //
    public static bool GetMenuInputStandardKeys(ref bool up, ref bool down, ref bool left, ref bool right, ref bool accept, ref bool decline)
    {
        if (IsBlocked)
            return false;

        up = Input.GetKeyDown(KeyCode.UpArrow);
        down = Input.GetKeyDown(KeyCode.DownArrow);
        left = Input.GetKeyDown(KeyCode.LeftArrow);
        right = Input.GetKeyDown(KeyCode.RightArrow);
        accept = Input.GetKeyDown(KeyCode.Return);
        decline = Input.GetKeyDown(KeyCode.Escape);

        return up | down | left | right | accept | decline;
    }
    //
    public static void GetKeyboard1Input(ref bool up, ref bool down, ref bool left, ref bool right, ref bool fire, ref bool jump, ref bool dash, ref bool kick)
    {
        if (IsBlocked)
            return;
        //
        fire = fire || Input.GetKey(keyPlayer1Fire);
        kick = kick || Input.GetKey(keyPlayer1Kick);
        up = up || Input.GetKey(keyPlayer1Up);
        jump = jump || Input.GetKey(keyPlayer1Jump);

        down = down || Input.GetKey(keyPlayer1Down);
        left = left || Input.GetKey(keyPlayer1Left);
        right = right || Input.GetKey(keyPlayer1Right);

        dash = Input.GetKey(keyPlayer1Dash);

    }
    //
    public static void GetKeyboard2Input(ref bool up, ref bool down, ref bool left, ref bool right, ref bool fire, ref bool jump, ref bool dash, ref bool kick)
    {

        if (IsBlocked)
            return;
        //
        fire = fire || Input.GetKey(keyPlayer2Fire);
        kick = kick || Input.GetKey(keyPlayer2Kick);
        up = up || Input.GetKey(keyPlayer2Up);
        jump = jump || Input.GetKey(keyPlayer2Jump);

        down = down || Input.GetKey(keyPlayer2Down);
        left = left || Input.GetKey(keyPlayer2Left);
        right = right || Input.GetKey(keyPlayer2Right);

        dash = Input.GetKey(keyPlayer2Dash);
    }
    //
    public static void GetXBoxControllerInput(string controllerIDString, ref bool up, ref bool down, ref bool left, ref bool right, ref bool fire, ref bool jump, ref bool dash, ref bool kick)
    {
        //
        //  *********************   controllerIDString  equals "_1" to "_4"
        //
        if (IsBlocked)
            return;
        //
        fire = fire || (Input.GetButton("X" + controllerIDString));
        jump = jump || Input.GetButtonDown("A" + controllerIDString);
        kick = kick || Input.GetButtonDown("B" + controllerIDString);
        //
        up = up || Input.GetAxisRaw("DPad_YAxis" + controllerIDString) > 0.25f || Input.GetAxisRaw("L_YAxis" + controllerIDString) < -0.25f;
        down = down || Input.GetAxisRaw("DPad_YAxis" + controllerIDString) < -0.25f || Input.GetAxisRaw("L_YAxis" + controllerIDString) > 0.25f;
        left = left || Input.GetAxisRaw("DPad_XAxis" + controllerIDString) < -0.25f || Input.GetAxisRaw("L_XAxis" + controllerIDString) < -0.25f;
        right = right || Input.GetAxisRaw("DPad_XAxis" + controllerIDString) > 0.25f || Input.GetAxisRaw("L_XAxis" + controllerIDString) > 0.25f;
        //
        dash = dash || Input.GetAxisRaw("TriggersR" + controllerIDString) > 0.25f || Input.GetAxisRaw("TriggersL" + controllerIDString) > 0.25f;
        //
    }
    //
    public static int GetControllerPressingStart()
    {
        if (IsBlocked)
            return -1;
        //

        if (Input.GetButtonDown("Start_1"))
        {
            return 2;
        }
        if (Input.GetButtonDown("Start_2"))
        {
            return 3;
        }
        if (Input.GetButtonDown("Start_3"))
        {
            return 4;
        }
        if (Input.GetButtonDown("Start_4"))
        {
            return 5;
        }

        return -1;
    }
    //
    public static bool GetKeyDown(KeyCode key)
    {
        if (IsBlocked)
            return false;
        //
        //
        return Input.GetKeyDown(key);
    }
    //
    public static bool GetButtonDown(string button)
    {
        if (IsBlocked)
            return false;
        //
        return Input.GetButtonDown(button);
    }
    //
}