﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: A linear mapping value that is used by other components
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearMapping : MonoBehaviour
	{
		public float value;

		public void resetAll()
		{
			transform.localPosition = Vector3.zero;
			value = 0.5f;
		}
	}
}
