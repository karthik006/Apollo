using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Apollo
{
    private static string currentUser;
    private static int coins;
    private static float distance;

    public static string CurrentUser
    {
        get => currentUser;

        set => currentUser = value;
    }

    public static int Coins
    {
        get => coins;
        set => coins = value;
    }

    public static float Distance
    {
        get => distance;
        set => distance = value;
    }
}
