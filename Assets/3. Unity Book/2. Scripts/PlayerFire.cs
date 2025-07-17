using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : Singleton<PlayerFire>
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    public int poolSize = 10;
    
    // public GameObject[] bulletObjectPool;
    // public List<GameObject> bulletObjectPool;
    public Queue<GameObject> bulletObjectPool;

    void Start()
    {
        // bulletObjectPool = new GameObject[poolSize];
        // bulletObjectPool = new List<GameObject>();
        bulletObjectPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            
            // bulletObjectPool[i] = bullet;
            // bulletObjectPool.Add(bullet);
            bulletObjectPool.Enqueue(bullet);
            
            bullet.SetActive(false);
        }
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (bulletObjectPool.Count > 0)
            {
                GameObject bullet = bulletObjectPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
            }
            
            // if (bulletObjectPool.Count > 0)
            // {
            //     GameObject bullet = bulletObjectPool[0]; // 가져올 오브젝트 선택
            //     bullet.SetActive(true); // 오브젝트 사용
            //     
            //     bulletObjectPool.Remove(bullet); // Pool에서 오브젝트 제거
            //     
            //     bullet.transform.position = firePosition.transform.position;
            // }
            
            // for (int i = 0; i < poolSize; i++)
            // {
            //     GameObject bullet = bulletObjectPool[i];
            //     
            //     if (!bullet.activeSelf) // 선택한 총알 오브젝트가 비활성화 상태인지 확인
            //     {
            //         bullet.SetActive(true); // 총알을 사용하기 위해 활성화
            //         bullet.transform.position = firePosition.transform.position; // 발사 위치 조정
            //
            //         break; // 반복문 종료
            //     }
            // }
        }
    }
}
