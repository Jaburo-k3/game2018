using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_camera_production : MonoBehaviour {

    Camera camera;
    float wait_time = 6f;

    IEnumerator wipe_size_up() {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            Rect rect = new Rect(camera.rect.x, camera.rect.y, camera.rect.width + 0.01f, camera.rect.height + 0.01f);
            camera.rect = rect;
            if (camera.rect.width > 0.3) {
                rect = new Rect(camera.rect.x, camera.rect.y, 0.3f, 0.3f);
                camera.rect = rect;
                break;
            }
        }
        yield return new WaitForSeconds(wait_time); 
        StartCoroutine(wipe_size_down());
    }

    IEnumerator wipe_size_down() {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            Rect rect = new Rect(camera.rect.x, camera.rect.y, camera.rect.width - 0.01f, camera.rect.height - 0.01f);
            camera.rect = rect;
            if (camera.rect.width < 0.0f)
            {
                rect = new Rect(camera.rect.x, camera.rect.y, 0f, 0f);
                camera.rect = rect;
                break;
            }
        }
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        camera = this.gameObject.GetComponent<Camera>();
        StartCoroutine(wipe_size_up());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
