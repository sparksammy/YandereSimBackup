﻿using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x060000D4 RID: 212 RVA: 0x000116BB File Offset: 0x0000F8BB
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000116C3 File Offset: 0x0000F8C3
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000116CC File Offset: 0x0000F8CC
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mSprite != null)
		{
			this.mAtlas = this.mSprite.atlas;
			this.mSpriteName = this.mSprite.spriteName;
			if (this.mSprite.depth < 100)
			{
				this.mSprite.depth = 100;
			}
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00011764 File Offset: 0x0000F964
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.orthographic)
			{
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
				return;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			this.mTrans.localPosition = mousePosition;
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0001187C File Offset: 0x0000FA7C
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000118B8 File Offset: 0x0000FAB8
	public static void Set(INGUIAtlas atlas, string sprite)
	{
		if (UICursor.instance != null && UICursor.instance.mSprite)
		{
			UICursor.instance.mSprite.atlas = atlas;
			UICursor.instance.mSprite.spriteName = sprite;
			UICursor.instance.mSprite.MakePixelPerfect();
			UICursor.instance.Update();
		}
	}

	// Token: 0x04000270 RID: 624
	public static UICursor instance;

	// Token: 0x04000271 RID: 625
	public Camera uiCamera;

	// Token: 0x04000272 RID: 626
	private Transform mTrans;

	// Token: 0x04000273 RID: 627
	private UISprite mSprite;

	// Token: 0x04000274 RID: 628
	private INGUIAtlas mAtlas;

	// Token: 0x04000275 RID: 629
	private string mSpriteName;
}
