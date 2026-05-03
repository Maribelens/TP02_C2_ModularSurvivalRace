using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager checkpointManager;
    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointManager.SetCheckpoint(index);
        }
    }
}
