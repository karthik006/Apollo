using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Apollo
{
    private static string currentUser;

    public static string CurrentUser
    {
        get => currentUser;

        set => currentUser = value;
    }
}
