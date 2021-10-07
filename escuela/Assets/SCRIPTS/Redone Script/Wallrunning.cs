using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrunning : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    bool wallLeft = false;

    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, wallDistance);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallRun())
        {
            if (wallLeft)
            {
                print("Wall on the left");
            }

        }
    }
}
