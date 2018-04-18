using UnityEngine;
using System.Collections;

public class FireCtrl : MonoBehaviour {

    public GameObject bullet;
    public Transform FirePos;
    public MeshRenderer muzzleFlash;

	// Use this for initialization
	void Start () {

        muzzleFlash.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

	}

    void Fire()
    {
        CreateBullet();

        StartCoroutine(this.ShowMuzzleFlash());
    }

    void CreateBullet()
    {
        Instantiate(bullet, FirePos.position, FirePos.rotation);
    }

    IEnumerator ShowMuzzleFlash()
    {
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));

        muzzleFlash.enabled = false;
    }
}
