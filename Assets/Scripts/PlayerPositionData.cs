using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerPositionData", menuName = "ScriptableObjects/PlayerPositionData", order = 0)]
public class PlayerPositionData : ScriptableObject
{
    public string PositionName = "PlayerPosition";

    [Range(0.0f, 1.0f)]
    public float MinX = 0.0f, MaxX = 1.0f, MinZ = 0.0f, MaxZ = 1.0f;

    [SerializeField]
    protected AnimationCurve curveX;
    public float XCurveAt(float ball)
    {
        return curveX.Evaluate(ball);
    }

    [SerializeField]
    protected AnimationCurve curveZ;
    public float ZCurveAt(float ball)
    {
        return curveZ.Evaluate(ball);
    }

    public float XLength
    {
        get { return MaxX - MinX; }
    }

    public float ZLength
    {
        get { return MaxZ - MinZ; }
    }
}
