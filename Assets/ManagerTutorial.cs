using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerTutorial : MonoBehaviour
{
    public List<GameObject> images;
    public List<GameObject> texts;

    public int index = 0;

    private void Start()
    {
        index = 0;
        images[0].SetActive(true);
        texts[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit"))
        {
            index++;
            if (index > images.Count - 1)
            {
                SceneManager.LoadScene(2);
                return;
            }
            else
            {
                ChangeImage();
            }
        }

    }

    void ChangeImage()
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (i == index)
            {
                images[i].SetActive(true);
                texts[i].SetActive(true);
            }
            else
            {
                images[i].SetActive(false);
                texts[i].SetActive(false);
            }

        }


    }
}
