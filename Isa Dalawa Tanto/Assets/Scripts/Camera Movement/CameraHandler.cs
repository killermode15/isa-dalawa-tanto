using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public static CameraHandler Instance => instance;
    private static CameraHandler instance;


    [Header("Follow Parameters")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private float followSpeed = 10;
    [SerializeField] private float peekRange = 2;

    [Header("Clamping Parameters")]
    //[SerializeField] private float deadZoneRange = 0.05f;
    [SerializeField] private Vector2 horizontalClamping = new Vector2(-1, 1);

    private bool isEnabled = true;

    //public float DeadZoneRange { get => deadZoneRange; }
    public Vector2 HorizontalClamp
    {
        get => horizontalClamping;
        set => horizontalClamping = value;
    }

    private Camera camera;
    private bool isInitialized = false;
    private const float ORTHO_SIZE_ADJUST_VALUE = 0.308125f * 12.5f;

    private void OnValidate()
    {
        //if (horizontalDeadZone.x < 0)
        //    horizontalDeadZone.x = 0;

        //if (horizontalDeadZone.y < 0.1)
        //    horizontalDeadZone.y = 0.1f;

        //if (horizontalDeadZone.y > 1)
        //    horizontalDeadZone.y = 1;

        //if (horizontalDeadZone.x >= horizontalDeadZone.y)
        //    horizontalDeadZone.x = horizontalDeadZone.y - 0.1f;


        //if (verticalDeadZone.x < 0)
        //    verticalDeadZone.x = 0;

        //if (verticalDeadZone.y < 0.1)
        //    verticalDeadZone.y = 0.1f;

        //if (verticalDeadZone.y > 1)
        //    verticalDeadZone.y = 1;

        //if (verticalDeadZone.x >= verticalDeadZone.y)
        //    verticalDeadZone.x = verticalDeadZone.y - 0.1f;
    }

    private void Delay()
    {
        isInitialized = true;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        transform.position = followTarget.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget == null)
            return;

        //if (IsTargetInDeadZone(followTarget))
        //    return;
        FollowTarget(followTarget);

    }

    private void FollowTarget(Transform target)
    {
        Vector3 finalMovePosition = transform.position;
        float verticalPeek = GetVerticalPeekValue();
        // Debug.Log(verticalPeek);

        // If the camera is beyond the clamped zones
        if (IsBeyondClampZone(camera, out int dir))
        {
            // Get the direction where the camera overstepped
            float clampValue = dir == 1 ? horizontalClamping.x : horizontalClamping.y;

            // Set the clamped position as the target position
            Vector3 adjustTargetPosition = new Vector3(clampValue + (dir * (camera.orthographicSize + (dir * ORTHO_SIZE_ADJUST_VALUE))),
                                                        target.position.y + verticalPeek,
                                                        -10);
            finalMovePosition = adjustTargetPosition;
            //transform.position = Vector3.MoveTowards(transform.position, adjustTargetPosition, Time.deltaTime * followSpeed);
        }
        else
        {

            Vector3 movedPosition = target.position + (Vector3.forward * -10) + (Vector3.up * verticalPeek);


            // Check if that moved position would result in the camera going beyond the clamped zones
            //if (isInitialized)
            {
                if (IsBeyondClampZone(movedPosition))
                {
                    movedPosition.x = transform.position.x;
                }
            }

            finalMovePosition = movedPosition;
        }

        transform.position = finalMovePosition;
    }

    private float GetVerticalPeekValue()
    {
        return isEnabled ? Input.GetAxis("Vertical") * peekRange : 0;
    }

    // Returns true if the indicated position oversteps the clamped zone.
    private bool IsBeyondClampZone(Vector3 newPosition)
    {
        float orthoSize = camera.orthographicSize;

        float leftPos = newPosition.x - orthoSize - ORTHO_SIZE_ADJUST_VALUE;
        float rightPos = newPosition.x + orthoSize + ORTHO_SIZE_ADJUST_VALUE;

        return leftPos < horizontalClamping.x || rightPos > horizontalClamping.y;
    }

    // Returns if the camera has reached the clamped zone and also returns the direction
    // where the camera overstepped the clamped zones
    private bool IsBeyondClampZone(Camera camera, out int direction)
    {

        float currLeftPosition = camera.transform.position.x - (camera.orthographicSize + ORTHO_SIZE_ADJUST_VALUE);
        float currRightPosition = camera.transform.position.x + (camera.orthographicSize + ORTHO_SIZE_ADJUST_VALUE);

        if (currLeftPosition < horizontalClamping.x)
            direction = -1;
        else if (currRightPosition > horizontalClamping.y)
            direction = 1;
        else
            direction = 0;

        return currLeftPosition < horizontalClamping.x || currRightPosition > horizontalClamping.y;
    }

    public void Enabled(bool state)
    {
        isEnabled = state;
    }

    // Returns true if the target is within the designated dead zone
    //private bool IsTargetInDeadZone(Transform target)
    //{
    //    Vector2 screenPosition = Camera.main.WorldToViewportPoint(target.position + (Vector3.up * GetVerticalPeekValue()));


    //    // Adjust the center of the screen position
    //    screenPosition.x -= 0.5f;
    //    screenPosition.y -= 0.5f;


    //    return (Vector2.Distance(Vector2.zero, screenPosition) < deadZoneRange);
    //}
}
