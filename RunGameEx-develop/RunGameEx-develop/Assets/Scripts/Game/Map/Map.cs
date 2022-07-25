using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public Transform cloud1;
    public Transform cloud2;
    public float cloudWidth = 20f;
    public float cloudSpeed1 = 0.02f;
    public float cloudSpeed2 = 0.03f;

    public Transform middle1;
    public Transform middle2;
    public float middleWidth = 20f;
    public float middleSpeed1 = 0.001f;
    public float middleSpeed2 = 0.01f;

    public Transform ground1;
    public Transform ground2;
    public float groundWidth = 10f;
    public float groundSpeed = 0.04f;

    private Transform curGround;
    private Transform nextGround;

    public Transform NextGround
    {
        get { return nextGround; }
    }

    public Transform mainCam;

    private void Start()
    {
        if (ground1 != null)
        {
            curGround = ground1;

            Vector3 pos1 = new Vector3(0f, 0f, 0f);
            curGround.position = pos1;
        }

        if (ground2 != null)
        {
            nextGround = ground2;

            Vector3 pos = new Vector3(curGround.position.x + groundWidth, 0f, 0f);
            nextGround.position = pos;
        }
        else
        {
            nextGround = ground1;
            Vector3 pos = new Vector3(curGround.position.x + groundWidth, 0f, 0f);
            nextGround.position = pos;
        }

        if (cloud1 != null)
        {
            Vector3 pos = new Vector3(0f, 0f, 1f);
            cloud1.position = pos;
        }

        if (cloud2 != null)
        {
            Vector3 pos = new Vector3(cloud1.position.x + middleWidth, 0f, 1f);
            cloud2.position = pos;
        }

        if (middle1 != null)
        {
            Vector3 pos = new Vector3(0f, 0f, 2f);
            middle1.position = pos;
        }

        if (middle2 != null)
        {
            Vector3 pos = new Vector3(middle1.position.x + middleWidth, 0f, 2f);
            middle2.position = pos;
        }

        if (mainCam == null)
            mainCam = GameObject.Find("MainCamera").transform;
    }

    void MoveGround()
    {
        if (curGround != null && nextGround != null)
        {
            Vector3 vector = curGround.position;
            if (mainCam.position.x - curGround.position.x < groundWidth)
            {
                vector.x -= groundSpeed;
                curGround.position = vector;
            }
            else
            {
                Transform temp = curGround;
                curGround = nextGround;
                nextGround = temp;
                nextGround.Translate(curGround.position.x + (2 * groundWidth), 0f, 0f);
            }

            Vector3 vector2 = nextGround.position;
            if (mainCam.position.x - nextGround.position.x < groundWidth)
            {
                vector2.x -= groundSpeed;
                nextGround.position = vector2;
            }

        }
    }

    void MoveCloud()
    {
        if (cloud1 != null)
        {
            Vector3 vector = cloud1.position;
            if (mainCam.position.x - cloud1.position.x < cloudWidth)
            {
                vector.x -= cloudSpeed1;
                cloud1.position = vector;
            }
            else
            {
                cloud1.Translate(mainCam.position.x + (2 * cloudWidth), 0f, 0f);
            }
        }

        if (cloud2 != null)
        {
            Vector3 vector = cloud2.position;
            if (mainCam.position.x - cloud2.position.x < cloudWidth)
            {
                vector.x -= cloudSpeed2;
                cloud2.position = vector;
            }
            else
            {
                cloud2.Translate(mainCam.position.x + (2 * cloudWidth), 0f, 0f);
            }
        }
    }

    void MoveMiddle()
    {
        if (middle1 != null)
        {
            Vector3 vector = middle1.position;
            if (mainCam.position.x - middle1.position.x < middleWidth)
            {
                vector.x -= middleSpeed1;
                middle1.position = vector;
            }
            else
            {
                middle1.Translate(mainCam.position.x + (2 * middleWidth), 0f, 0f);
            }
        }

        if (middle2 != null)
        {
            Vector3 vector = middle2.position;
            if (mainCam.position.x - middle2.position.x < middleWidth)
            {
                vector.x -= middleSpeed2;
                middle2.position = vector;
            }
            else
            {
                middle2.Translate(mainCam.position.x + (2 * middleWidth), 0f, 0f);
            }
        }
    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver == true)
        {
            return;
        }
        MoveMiddle();
        MoveGround();
        MoveCloud();
    }
}
