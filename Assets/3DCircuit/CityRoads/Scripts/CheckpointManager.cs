using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private int currentCheckpoint;

    public void SetCheckpoint(int index)
    {
        if (index > currentCheckpoint)
        {
            currentCheckpoint = index;
            Debug.Log("Checkpoint: " + index);
        }
    }
}
