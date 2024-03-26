using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public GameManager gameManager;
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int lastDirection = Vector2Int.left;
    public List<Transform> segments { get; } = new List<Transform>();

    public Transform segmentPrefab;

    public int initialSize = 4;

    /// <summary>
    /// Update频率取决于CPU等
    /// </summary>
    private void Update() {
        if (lastDirection != Vector2Int.down && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
            direction = Vector2Int.up;
        } else if (lastDirection != Vector2Int.up && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))) {
            direction = Vector2Int.down;
        } else if (lastDirection != Vector2Int.right && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))) {
            direction = Vector2Int.left;
        } else if (lastDirection != Vector2Int.left && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))) {
            direction = Vector2Int.right;
        }
    }

    /// <summary>
    /// 以固定的频率Update
    /// 用来控制物理轨迹
    /// </summary>
    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) { 
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0f
        );

        lastDirection = direction;
    }

    public void ResetState() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for(int i = 0; i < this.initialSize; i++) {
            segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") { 
            Grow();
            gameManager.IncreaseScore(1);
        } else if (other.tag == "Obstacle") { 
            gameManager.NewGame();
        }
    }

    private void Grow() {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        
        segments.Add(segment);
    }
}
