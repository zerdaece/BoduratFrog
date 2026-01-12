using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UIElements; // not needed here

public class PlayerSpriteHandler : MonoBehaviour
{
    public static PlayerSpriteHandler Instance { get; private set; }
    public SpriteRenderer headRenderer;
    public SpriteRenderer dressRenderer;
    public SpriteRenderer poseRenderer;
    public PlayerHead playerHead;
    public PlayerDress playerDress;
    public FrogPose playerPose;
    public List<string> playerPoses = new List<string> { "BallPose", "CoolPose", "SadPose", "StarPose" };
    [SerializeField] private bool randomizeOnStart = true;
    public string currentPose;

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
        // Ensure initial sprites are set in builds
        if (randomizeOnStart)
        {
            ChangeRandomPose();
        }
        else
        {
            SetPoseSprites("BallPose");
        }
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
            headRenderer.transform.rotation = Quaternion.Lerp(headRenderer.transform.rotation, Quaternion.Euler(0, 0, -60), Time.deltaTime * rb.linearVelocity.x);
            dressRenderer.transform.rotation = Quaternion.Lerp(dressRenderer.transform.rotation, Quaternion.Euler(0, 0, -60), Time.deltaTime * rb.linearVelocity.x);
            poseRenderer.transform.rotation = Quaternion.Lerp(poseRenderer.transform.rotation, Quaternion.Euler(0, 0, -60), Time.deltaTime * rb.linearVelocity.x);
        }
        else if (rb.linearVelocity.x < 0)
        {
            headRenderer.transform.rotation = Quaternion.Lerp(headRenderer.transform.rotation, Quaternion.Euler(0, 0, 60), Time.deltaTime * rb.linearVelocity.x * -1);
            dressRenderer.transform.rotation = Quaternion.Lerp(dressRenderer.transform.rotation, Quaternion.Euler(0, 0, 60), Time.deltaTime * rb.linearVelocity.x * -1);
            poseRenderer.transform.rotation = Quaternion.Lerp(poseRenderer.transform.rotation, Quaternion.Euler(0, 0, 60), Time.deltaTime * rb.linearVelocity.x * -1);
        }
        if (rb.linearVelocity.y < 3)
        {
            headRenderer.transform.rotation = Quaternion.Lerp(headRenderer.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rb.linearVelocity.y * -1);
            dressRenderer.transform.rotation = Quaternion.Lerp(dressRenderer.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rb.linearVelocity.y * -1);
            poseRenderer.transform.rotation = Quaternion.Lerp(poseRenderer.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rb.linearVelocity.y * -1);
        }

    }
    public void ChangeRandomPose()
    {

        if (playerPoses == null || playerPoses.Count == 0) { print("No poses available."); return; }

        int randomIndex = UnityEngine.Random.Range(0, playerPoses.Count);
        while (randomIndex == 3)
        {
            randomIndex = UnityEngine.Random.Range(0, playerPoses.Count);
        }
        string selectedPose = playerPoses[randomIndex];
        if (selectedPose == currentPose)
        {
            randomIndex = (randomIndex + 1) % playerPoses.Count;
            selectedPose = playerPoses[randomIndex];
        }
        currentPose = selectedPose;
        SetPoseSprites(selectedPose);
    }

    public void SetPoseSprites(string poseKey)
    {
        print("Setting pose sprites for: " + poseKey);
        if (playerHead == null || playerDress == null || playerPose == null)
        {
            Debug.LogError($"Pose change aborted. Missing refs -> head:{playerHead == null} dress:{playerDress == null} pose:{playerPose == null}");
            return;
        }

        Debug.Log($"Pose change request [{poseKey}] | assets: headSO={playerHead.name}, dressSO={playerDress.name}, poseSO={playerPose.name} | current sprites: headR={(headRenderer ? headRenderer.sprite?.name : "null renderer")}, dressR={(dressRenderer ? dressRenderer.sprite?.name : "null renderer")}, poseR={(poseRenderer ? poseRenderer.sprite?.name : "null renderer")}");

        Sprite headSprite;
        Sprite dressSprite;
        Sprite poseSprite;

        switch (poseKey)
        {
            case "BallPose":
                headSprite = playerHead.BallPose;
                dressSprite = playerDress.BallPose;
                poseSprite = playerPose.BallPose;
                break;
            case "CoolPose":
                headSprite = playerHead.CoolPose;
                dressSprite = playerDress.CoolPose;
                poseSprite = playerPose.CoolPose;
                break;
            case "SadPose":
                headSprite = playerHead.SadPose;
                dressSprite = playerDress.SadPose;
                poseSprite = playerPose.SadPose;
                break;
            case "StarPose":
                headSprite = playerHead.StarPose;
                dressSprite = playerDress.StarPose;
                poseSprite = playerPose.StarPose;
                break;
            default:
                headSprite = playerHead.IdlePose;
                dressSprite = playerDress.IdlePose;
                poseSprite = playerPose.IdlePose;
                print("Pose key not recognized, setting to IdlePose.");
                break;
        }

        if (headRenderer == null || dressRenderer == null || poseRenderer == null)
        {
            Debug.LogError($"Pose change aborted. Missing renderers -> headR:{headRenderer == null} dressR:{dressRenderer == null} poseR:{poseRenderer == null}");
            return;
        }

        if (headSprite != null) { headRenderer.sprite = headSprite; print("Head sprite set to " + headSprite.name); }
        if (dressSprite != null) { dressRenderer.sprite = dressSprite; print("Dress sprite set to " + dressSprite.name); }
        if (poseSprite != null) { poseRenderer.sprite = poseSprite; print("Pose sprite set to " + poseSprite.name); }

        Debug.Log($"Pose change applied [{poseKey}] | new sprites: head={headRenderer.sprite?.name}, dress={dressRenderer.sprite?.name}, pose={poseRenderer.sprite?.name}");

        if (headSprite == null || dressSprite == null || poseSprite == null)
        {
            Debug.LogWarning("One or more sprites are missing for the pose: " + poseKey);
        }
    }
}