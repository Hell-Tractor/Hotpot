using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    public AudioSource SESource { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("Multiple instances of GameManager detected!");
            Destroy(gameObject);
            return;
        }

        SESource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }
}
