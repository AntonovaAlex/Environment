using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{

	public class GameController : MonoBehaviour
	{

		public GameObject playerObj;
		public GameObject creeperObj;
		public GameObject skeletonObj;

		//Список, который будет содержать всех врагов
		List<Enemy> enemies = new List<Enemy>();

		void Start()
		{
			//Добавление врагов
			enemies.Add(new Creeper(creeperObj.transform));
			enemies.Add(new Skeleton(skeletonObj.transform));
		}

		void Update()
		{
			// Обновление сосотояния всех врагов
			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].UpdateEnemy(playerObj.transform);
			}
		}
	}
}
