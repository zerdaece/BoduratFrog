using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHead", menuName = "ScriptableObjects/PlayerHead", order = 0)]
public class PlayerHead : ScriptableObject
{
    public Sprite BallPose;
    public Sprite CoolPose;
    public Sprite SadPose;
    public Sprite StarPose;
    public Sprite IdlePose;
    public string HeadName;
    public bool acquired;
}
