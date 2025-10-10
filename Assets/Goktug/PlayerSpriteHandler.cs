using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpriteHandler : MonoBehaviour
{
    public static PlayerSpriteHandler Instance { get; private set; }
    public SpriteRenderer headRenderer;
    public SpriteRenderer dressRenderer;
    public SpriteRenderer poseRenderer;
    public PlayerHead playerHead;
    public PlayerDress playerDress;
    public PlayerPose playerPose;
    public List<string> playerPoses = new List<string> { "BallPose", "CoolPose", "SadPose", "StarPose" };

    [SerializeField] Rigidbody2D rb { get => transform.GetComponent<Rigidbody2D>(); set => rb = value; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        headRenderer.sprite = playerHead.BallPose;
        dressRenderer.sprite = playerDress.BallPose;
        poseRenderer.sprite = playerPose.BallPose;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleSpriteRotation();
    }

    void HandleSpriteRotation()
    {
        if (rb.linearVelocity.x > 0)
        {
            headRenderer.transform.rotation = Quaternion.Lerp(headRenderer.transform.rotation, Quaternion.Euler(0, 0, headRenderer.transform.rotation.eulerAngles.z - 60), Time.deltaTime * rb.linearVelocity.x);
            dressRenderer.transform.rotation = Quaternion.Lerp(dressRenderer.transform.rotation, Quaternion.Euler(0, 0, dressRenderer.transform.rotation.eulerAngles.z - 60), Time.deltaTime * rb.linearVelocity.x);
            poseRenderer.transform.rotation = Quaternion.Lerp(poseRenderer.transform.rotation, Quaternion.Euler(0, 0, poseRenderer.transform.rotation.eulerAngles.z - 60), Time.deltaTime * rb.linearVelocity.x);
        }
        else if (rb.linearVelocity.x < 0)
        {
            headRenderer.transform.rotation = Quaternion.Lerp(headRenderer.transform.rotation, Quaternion.Euler(0, 0, headRenderer.transform.rotation.eulerAngles.z + 60), Time.deltaTime * rb.linearVelocity.x * -1);
            dressRenderer.transform.rotation = Quaternion.Lerp(dressRenderer.transform.rotation, Quaternion.Euler(0, 0, dressRenderer.transform.rotation.eulerAngles.z + 60), Time.deltaTime * rb.linearVelocity.x * -1);
            poseRenderer.transform.rotation = Quaternion.Lerp(poseRenderer.transform.rotation, Quaternion.Euler(0, 0, poseRenderer.transform.rotation.eulerAngles.z + 60), Time.deltaTime * rb.linearVelocity.x * -1);
        }
    }
    public void ChangeRandomPose()
    {
        int randomIndex = UnityEngine.Random.Range(0, playerPoses.Count);
        string selectedPose = playerPoses[randomIndex];
        headRenderer.sprite = playerHead.GetType().GetField(selectedPose).GetValue(playerHead) as Sprite;
        dressRenderer.sprite = playerDress.GetType().GetField(selectedPose).GetValue(playerDress) as Sprite;
        poseRenderer.sprite = playerPose.GetType().GetField(selectedPose).GetValue(playerPose) as Sprite;
    }
}

[CreateAssetMenu(fileName = "PlayerPose", menuName = "ScriptableObjects/PlayerPose", order = 2)]
public class PlayerPose : ScriptableObject
{
    public Sprite BallPose;
    public Sprite CoolPose;
    public Sprite SadPose;
    public Sprite StarPose;
    public Sprite IdlePose;
}



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