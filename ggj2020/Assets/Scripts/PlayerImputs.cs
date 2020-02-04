using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerImputs : MonoBehaviour
{
    private static KeyCode player1Interact = KeyCode.Mouse0;
    private static KeyCode player1Bucket = KeyCode.LeftShift;
    private static KeyCode player1Discard = KeyCode.Mouse1;

    private static KeyCode player2Interact = KeyCode.JoystickButton5;
    private static KeyCode player2Bucket = KeyCode.JoystickButton4;
    private static KeyCode player2Discard = KeyCode.JoystickButton1;

    private bool pressedIntract;
    private bool pressedBucket;

    public static bool GetBucketKeyDown(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyDown(player1Bucket);
        }
        else
        {
            return Input.GetKeyDown(player2Bucket);
        }
    }

    public static bool GetBucketKeyUp(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyUp(player1Bucket);
        }
        else
        {
            return Input.GetKeyUp(player2Bucket);
        }
    }

    public static bool GetInteractKeyDown(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyDown(player1Interact);
        }
        else
        {
            return Input.GetKeyDown(player2Interact);
        }
    }

    public static bool GetInteractKeyUp(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyUp(player1Interact);
        }
        else
        {
            return Input.GetKeyUp(player2Interact);
        }
    }

    public static bool GetDiscardKeyDown(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyDown(player1Discard);
        }
        else
        {
            return Input.GetKeyDown(player2Discard);
        }
    }

    public static bool GetDiscardKeyUp(PlayerType playerType)
    {
        if (playerType == PlayerType.Player01)
        {
            return Input.GetKeyUp(player1Discard);
        }
        else
        {
            return Input.GetKeyUp(player2Discard);
        }
    }
}
