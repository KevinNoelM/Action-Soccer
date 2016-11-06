using UnityEngine;
using System.Collections;


public class CharacterInput : MonoBehaviour
{

    bool left = false;
    bool right = false;
    bool up = false;
    bool down = false;
    bool fire = false;
    bool kick = false;
    bool jump = false;
    bool dash = false;
    //
    bool wasJump = false;
    bool wasFire = false;
    bool wasKick = false;
    bool wasUp = false;
    bool wasLeft = false;
    bool wasRight = false;
    //
    public int controllerID = 0;
    //
    void Start()
    {

    }


    void Update()
    {
        //
        wasLeft = left;
        wasRight = right;
        //  
        wasFire = fire;
        wasKick = kick;
        wasJump = jump;
        wasUp = up;
        up = down = left = right = fire = jump = dash = false;
        //

        if (controllerID < 5)
        {
            InputReader.GetInput(controllerID, ref up, ref down, ref left, ref right, ref fire, ref jump, ref dash, ref kick);
        }
    }
    //
    public bool Jump()
    {
        return (jump && !wasJump) || (up && !wasUp);
    }
    //
    public bool PressFire()
    {
        return fire && !wasFire;
    }
    //
    public bool ReleaseFire()
    {
        return !fire && wasFire;
    }
    //
    public bool PressKick()
    {
        return kick && !wasKick;
    }
    //
    public bool ReleaseKick()
    {
        return !kick && wasKick;
    }
    //
    public bool PressJump()
    {
        return jump && !wasJump;
    }
    //
    public bool ReleaseJump()
    {
        return !jump && wasJump;
    }
    //
    public bool HoldFire()
    {
        return fire;
    }
    public bool PressLeft()
    {
        return left && !wasLeft;
    }
    public bool PressRight()
    {
        return right && !wasRight;
    }
    public bool HoldLeft()
    {
        return left && !right;
    }
    //
    public bool HoldRight()
    {
        return right && !left;
    }
    //
    public bool HoldUp()
    {
        return up && !down;
    }
    //
    public bool HoldDown()
    {
        return down && !up;
    }
    //
    public bool HoldDash()
    {
        return dash;
    }
}
