using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buffer: MonoBehaviour
{
    public static bool IsHealthyFood { get; private set; }

    public static void ChangeFood(bool IsHealthy)
    {
        IsHealthyFood = IsHealthy;
    }
}
