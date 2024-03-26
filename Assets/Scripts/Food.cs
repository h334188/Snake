using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Food : MonoBehaviour {

    public BoxCollider2D gridArea;

    public Snake snake;

    private void Start () {
        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            RandomizePosition();
        }
    }

    private void RandomizePosition() {
        Vector3 randomPosition = GetRandomPosition();

        while (isSamePosition(randomPosition)) {
            randomPosition = GetRandomPosition();
        }

        this.transform.position = randomPosition;
    }

    private bool isSamePosition(Vector3 position) {
        //List<Transform> snakeSegments = snake.GetSegments();
        List<Transform> snakeSegments = snake.segments;

        for (int i = 0; i < snakeSegments.Count; i++) {
            Transform snakeSegmentTransform = snakeSegments[i];
            if (snakeSegmentTransform.position == position) { 
                return true; 
            }
            //Debug.Log(snakeSegmentTransform.position + "@@" + randomPosition);
        }
        return false;
    }

    private Vector3 GetRandomPosition() {
        // ����Ҫ����Ⱦ������ֱ�ӻ�ȡ�������λ�á���С����ת����Ϣ��
        // ͨ����ȡ�����bounds����Ҫͨ��GetComponent<Renderer>()
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }
}
