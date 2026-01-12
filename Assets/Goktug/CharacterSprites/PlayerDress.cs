using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDress", menuName = "ScriptableObjects/PlayerDress", order = 1)]
public class PlayerDress : ScriptableObject
{
    public Sprite BallPose;
    public Sprite CoolPose;
    public Sprite SadPose;
    public Sprite StarPose;
    public Sprite IdlePose;
    public string DressName;
    public bool acquired;
}
