using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
	public int id;

	public GameObject iconKnight;
	public GameObject iconWizard;
	public GameObject iconFairy;

	public void SetIcon()
	{
		if(id == 1)
		{
			iconFairy.SetActive(true);
		}

		if (id == 2)
		{
			iconWizard.SetActive(true);
		}

		if (id == 3)
		{
			iconKnight.SetActive(true);
		}
	}

	public void RemoveIcon()
	{
		if (id == 1)
		{
			iconFairy.SetActive(false);
		}

		if (id == 2)
		{
			iconWizard.SetActive(false);
		}

		if (id == 3)
		{
			iconKnight.SetActive(false);
		}
	}
}
