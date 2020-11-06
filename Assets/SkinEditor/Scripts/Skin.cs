
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
	public int rotationSpeed = 10;
	private Camera mainCamera;
	private Color currentColor;
	public Texture2D skin;
	public GameObject quadPrefab;
	public GameObject layer1, layer2;
	public GameObject head1, head2, torso1, torso2, leftArm1, leftArm2, rightArm1, rightArm2, leftLeg1, leftLeg2, rightLeg1, rihgtLeg2;
	private float quadDiameter;
	private Texture2D outSkin;
	private Dictionary<string, int[]> parts = new Dictionary<string, int[]>();
	public Transform HeadTop, HeadBottom, HeadRight, HeadFront, HeadLeft, HeadBack,
					HelmTop, HelmBottom, HelmRight, HelmFront, HelmLeft, HelmBack,
					RightLegTop, RightLegBottom, RightLegRight, RightLegFront, RightLegLeft, RightLegBack,
					TorsoTop, TorsoBottom, TorsoRight, TorsoFront, TorsoLeft, TorsoBack,
					RightArmTop, RightArmBottom, RightArmRight, RightArmFront, RightArmLeft, RightArmBack,
					LeftLegTop, LeftLegBottom, LeftLegRight, LeftLegFront, LeftLegLeft, LeftLegBack,
					LeftArmTop, LeftArmBottom, LeftArmRight, LeftArmFront, LeftArmLeft, LeftArmBack,
					RightLegLayerTop, RightLegLayerBottom, RightLegLayerRight, RightLegLayerFront, RightLegLayerLeft, RightLegLayerBack,
					TorsoLayerTop, TorsoLayerBottom, TorsoLayerRight, TorsoLayerFront, TorsoLayerLeft, TorsoLayerBack,
					RightArmLayerTop, RightArmLayerBottom, RightArmLayerRight, RightArmLayerFront, RightArmLayerLeft, RightArmLayerBack,
					LeftLegLayerTop, LeftLegLayerBottom, LeftLegLayerRight, LeftLegLayerFront, LeftLegLayerLeft, LeftLegLayerBack,
					LeftArmLayerTop, LeftArmLayerBottom, LeftArmLayerRight, LeftArmLayerFront, LeftArmLayerLeft, LeftArmLayerBack;

	private Transform[] transforms;
    private Vector3 startPos;
    private Vector3 direction;
    private bool isRotating;
    private bool isDrawing;

    private void Start()
    {
		#region 
		parts.Add("HeadTop", new int[] { 8, 56, 16, 64 });
		parts.Add("HeadBottom", new int[] { 16, 56, 24, 64 });
		parts.Add("HeadRight", new int[] { 0, 48, 8, 56 });
		parts.Add("HeadFront", new int[] { 8, 48, 16, 56 });
		parts.Add("HeadLeft", new int[] { 16, 48, 24, 56 });
		parts.Add("HeadBack", new int[] { 24, 48, 32, 56 });
		parts.Add("HelmTop", new int[] { 40, 56, 48, 64 });
		parts.Add("HelmBottom", new int[] { 48, 56, 56, 64 });
		parts.Add("HelmRight", new int[] { 32, 48, 40, 56 });
		parts.Add("HelmFront", new int[] { 40, 48, 48, 56 });
		parts.Add("HelmLeft", new int[] { 48, 48, 56, 56 });
		parts.Add("HelmBack", new int[] { 56, 48, 64, 56 });
		parts.Add("RightLegTop", new int[] { 4, 44, 8, 48 });
		parts.Add("RightLegBottom", new int[] { 8, 44, 12, 48 });
		parts.Add("RightLegRight", new int[] { 0, 32, 4, 44 });
		parts.Add("RightLegFront", new int[] { 4, 32, 8, 44 });
		parts.Add("RightLegLeft", new int[] { 8, 32, 12, 44 });
		parts.Add("RightLegBack", new int[] { 12, 32, 16, 44 });
		parts.Add("TorsoTop", new int[] { 20, 44, 28, 48 });
		parts.Add("TorsoBottom", new int[] { 28, 44, 36, 48 });
		parts.Add("TorsoRight", new int[] { 16, 32, 20, 44 });
		parts.Add("TorsoFront", new int[] { 20, 32, 28, 44 });
		parts.Add("TorsoLeft", new int[] { 28, 32, 32, 44 });
		parts.Add("TorsoBack", new int[] { 32, 32, 40, 44 });
		parts.Add("RightArmTop", new int[] { 44, 44, 48, 48 });
		parts.Add("RightArmBottom", new int[] { 48, 44, 52, 48 });
		parts.Add("RightArmRight", new int[] { 40, 32, 44, 44 });
		parts.Add("RightArmFront", new int[] { 44, 32, 48, 44 });
		parts.Add("RightArmLeft", new int[] { 48, 32, 52, 44 });
		parts.Add("RightArmBack", new int[] { 52, 32, 56, 44 });
		parts.Add("LeftLegTop", new int[] { 20, 12, 24, 16 });
		parts.Add("LeftLegBottom", new int[] { 24, 12, 28, 16 });
		parts.Add("LeftLegRight", new int[] { 16, 0, 20, 12 });
		parts.Add("LeftLegFront", new int[] { 20, 0, 24, 12 });
		parts.Add("LeftLegLeft", new int[] { 24, 0, 28, 12 });
		parts.Add("LeftLegBack", new int[] { 28, 0, 32, 12 });
		parts.Add("LeftArmTop", new int[] { 36, 12, 40, 16 });
		parts.Add("LeftArmBottom", new int[] { 40, 12, 44, 16 });
		parts.Add("LeftArmRight", new int[] { 32, 0, 36, 12 });
		parts.Add("LeftArmFront", new int[] { 36, 0, 40, 12 });
		parts.Add("LeftArmLeft", new int[] { 40, 0, 44, 12 });
		parts.Add("LeftArmBack", new int[] { 44, 0, 48, 12 });
		parts.Add("RightLegLayer2Top", new int[] { 4, 28, 8, 32 });
		parts.Add("RightLegLayer2Bottom", new int[] { 8, 28, 12, 32 });
		parts.Add("RightLegLayer2Right", new int[] { 0, 16, 4, 28 });
		parts.Add("RightLegLayer2Front", new int[] { 4, 16, 8, 28 });
		parts.Add("RightLegLayer2Left", new int[] { 8, 16, 12, 28 });
		parts.Add("RightLegLayer2Back", new int[] { 12, 16, 16, 28 });
		parts.Add("TorsoLayer2Top", new int[] { 20, 28, 28, 32 });
		parts.Add("TorsoLayer2Bottom", new int[] { 28, 28, 36, 32 });
		parts.Add("TorsoLayer2Right", new int[] { 16, 16, 20, 28 });
		parts.Add("TorsoLayer2Front", new int[] { 20, 16, 28, 28 });
		parts.Add("TorsoLayer2Left", new int[] { 28, 16, 32, 28 });
		parts.Add("TorsoLayer2Back", new int[] { 32, 16, 40, 28 });
		parts.Add("RightArmLayer2Top", new int[] { 44, 28, 48, 32 });
		parts.Add("RightArmLayer2Bottom", new int[] { 48, 28, 52, 32 });
		parts.Add("RightArmLayer2Right", new int[] { 40, 16, 44, 28 });
		parts.Add("RightArmLayer2Front", new int[] { 44, 16, 48, 28 });
		parts.Add("RightArmLayer2Left", new int[] { 48, 16, 52, 28 });
		parts.Add("RightArmLayer2Back", new int[] { 52, 16, 56, 28 });
		parts.Add("LeftLegLayer2Top", new int[] { 4, 12, 8, 16 });
		parts.Add("LeftLegLayer2Bottom", new int[] { 8, 12, 12, 16 });
		parts.Add("LeftLegLayer2Right", new int[] { 0, 0, 4, 12 });
		parts.Add("LeftLegLayer2Front", new int[] { 4, 0, 8, 12 });
		parts.Add("LeftLegLayer2Left", new int[] { 8, 0, 12, 12 });
		parts.Add("LeftLegLayer2Back", new int[] { 12, 0, 16, 12 });
		parts.Add("LeftArmLayer2Top", new int[] { 52, 12, 56, 16 });
		parts.Add("LeftArmLayer2Bottom", new int[] { 56, 12, 60, 16 });
		parts.Add("LeftArmLayer2Right", new int[] { 48, 0, 52, 12 });
		parts.Add("LeftArmLayer2Front", new int[] { 52, 0, 56, 12 });
		parts.Add("LeftArmLayer2Left", new int[] { 56, 0, 60, 12 });
		parts.Add("LeftArmLayer2Back", new int[] { 60, 0, 64, 12 });

		transforms = new Transform[] {HeadTop, HeadBottom, HeadRight, HeadFront, HeadLeft, HeadBack,
					HelmTop, HelmBottom, HelmRight, HelmFront, HelmLeft, HelmBack,
					RightLegTop, RightLegBottom, RightLegRight, RightLegFront, RightLegLeft, RightLegBack,
					TorsoTop, TorsoBottom, TorsoRight, TorsoFront, TorsoLeft, TorsoBack,
					RightArmTop, RightArmBottom, RightArmRight, RightArmFront, RightArmLeft, RightArmBack,
					LeftLegTop, LeftLegBottom, LeftLegRight, LeftLegFront, LeftLegLeft, LeftLegBack,
					LeftArmTop, LeftArmBottom, LeftArmRight, LeftArmFront, LeftArmLeft, LeftArmBack,
					RightLegLayerTop, RightLegLayerBottom, RightLegLayerRight, RightLegLayerFront, RightLegLayerLeft, RightLegLayerBack,
					TorsoLayerTop, TorsoLayerBottom, TorsoLayerRight, TorsoLayerFront, TorsoLayerLeft, TorsoLayerBack,
					RightArmLayerTop, RightArmLayerBottom, RightArmLayerRight, RightArmLayerFront, RightArmLayerLeft, RightArmLayerBack,
					LeftLegLayerTop, LeftLegLayerBottom, LeftLegLayerRight, LeftLegLayerFront, LeftLegLayerLeft, LeftLegLayerBack,
					LeftArmLayerTop, LeftArmLayerBottom, LeftArmLayerRight, LeftArmLayerFront, LeftArmLayerLeft, LeftArmLayerBack};
		#endregion
		outSkin = new Texture2D(64, 64, TextureFormat.RGBA32, false);
		outSkin.SetPixels32(new Color32[64 * 64]);
		mainCamera = Camera.main;
		quadDiameter = quadPrefab.transform.localScale.x;
		currentColor = Color.white;
		Refresh();
	}
	void Update()
	{
		if (Input.touchCount == 1)
		{
			Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
				isRotating = false;
				isDrawing = false;
            }
			else if (isRotating)
			{
				TouchRotate(touch);
			}
			else
			{
				Ray ray = mainCamera.ScreenPointToRay(touch.position);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.CompareTag("Quad"))
					{
                        if (touch.phase == TouchPhase.Began)
                        {
							isDrawing = true;
						}
						GameObject recipient = hit.transform.gameObject;
						Quad quad1 = recipient.GetComponent<Quad>();
						outSkin.SetPixel(quad1.X, quad1.Y, currentColor);
						recipient.GetComponent<MeshRenderer>().material.color = currentColor;
					}
					else if (isDrawing == false)
					{
						TouchRotate(touch);
					}
				}
				else if (isDrawing == false)
				{
					TouchRotate(touch);
				}
			}
			
		}

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
			startPos = Input.mousePosition;

			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.CompareTag("Quad"))
				{
					isDrawing = true;
					GameObject recipient = hit.transform.gameObject;
					Quad quad1 = recipient.GetComponent<Quad>();
					outSkin.SetPixel(quad1.X, quad1.Y, currentColor);
					recipient.GetComponent<MeshRenderer>().material.color = currentColor;
				}
				else
				{
					isRotating = true;
					direction = Input.mousePosition - startPos;
					startPos = Input.mousePosition;
					transform.Rotate(new Vector3(direction.y, -direction.x, 0) * Time.deltaTime * rotationSpeed, Space.World);
				}
			}
			else
			{
				isRotating = true;
				direction = Input.mousePosition - startPos;
				startPos = Input.mousePosition;
				transform.Rotate(new Vector3(direction.y, -direction.x, 0) * Time.deltaTime * rotationSpeed, Space.World);
			}
		}
        else if (Input.GetMouseButton(0))
        {
			
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
            if (isRotating)
            {
				direction = Input.mousePosition - startPos;
				startPos = Input.mousePosition;
				transform.Rotate(new Vector3(direction.y, -direction.x, 0) * Time.deltaTime * rotationSpeed, Space.World);
			}
			else if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.CompareTag("Quad"))
				{
					GameObject recipient = hit.transform.gameObject;
					Quad quad1 = recipient.GetComponent<Quad>();
					outSkin.SetPixel(quad1.X, quad1.Y, currentColor);
					recipient.GetComponent<MeshRenderer>().material.color = currentColor;
				}
			}
		}
        else if (Input.GetMouseButtonUp(0))
        {
			isRotating = false;
			isDrawing = false;
        }
#endif
	}

	private void TouchRotate(Touch touch)
    {
		switch (touch.phase)
		{
			case TouchPhase.Began:
				startPos = touch.position;
				isRotating = true;
				break;
			case TouchPhase.Moved:
				Vector3 touchPosition = touch.position;
				direction = touchPosition - startPos;
				startPos = touchPosition;
				transform.Rotate(new Vector3(direction.y, -direction.x, 0) * Time.deltaTime * rotationSpeed, Space.World);
				break;
		}
	}

	public void ChangeColor(Image image)
	{
		currentColor = image.color;
	}
	public void Refresh()
	{
		StartCoroutine(SetTexture());
	}

	public void SaveSkin()
    {
		outSkin.Apply();
		NativeGallery.SaveImageToGallery(outSkin, "ProjectX", "skin.png");
	}
	public void ChangeLayer()
    {
        if (layer1.activeSelf)
        {
			layer1.SetActive(false);
			layer2.SetActive(true);
        }
        else
        {
			layer2.SetActive(false);
			layer1.SetActive(true);
		}
    }

    

	public void OnOfBodyParts(string part)
    {
        switch (part)
        {
			case "head":
                if (head1.activeSelf)
                {
					head1.SetActive(false);
					head2.SetActive(false);
                }
                else
                {
					head1.SetActive(true);
					head2.SetActive(true);
				}
				break;
			case "torso":
                if (torso1.activeSelf)
                {
					torso1.SetActive(false);
					torso2.SetActive(false);
                }
                else
                {
					torso1.SetActive(true);
					torso2.SetActive(true);
                }
				break;
			case "rightArm":
                if (rightArm1.activeSelf)
                {
					rightArm1.SetActive(false);
					rightArm2.SetActive(false);
                }
                else
                {
					rightArm1.SetActive(true);
					rightArm2.SetActive(true);
                }
				break;
			case "leftArm":
                if (leftArm1.activeSelf)
                {
					leftArm1.SetActive(false);
					leftArm2.SetActive(false);
                }
                else
                {
					leftArm1.SetActive(true);
					leftArm2.SetActive(true);
                }
				break;
			case "rightLeg":
                if (rightLeg1.activeSelf)
                {
					rightLeg1.SetActive(false);
					rihgtLeg2.SetActive(false);
                }
                else
                {
					rightLeg1.SetActive(true);
					rihgtLeg2.SetActive(true);
                }
				break;
			case "leftLeg":
                if (leftLeg1.activeSelf)
                {
					leftLeg1.SetActive(false);
					leftLeg2.SetActive(false);
                }
                else
                {
					leftLeg1.SetActive(true);
					leftLeg2.SetActive(true);
                }
				break;
		}
    }

	IEnumerator SetTexture()
	{
		yield return new WaitForEndOfFrame();
		skin.filterMode = FilterMode.Point;

        foreach (var tf in transforms)
        {
            if (tf != null)
            {
				int x1 = Mathf.Abs(parts[tf.name][0]);
				int y1 = Mathf.Abs(parts[tf.name][1]);
				int x = Mathf.Abs(x1 - parts[tf.name][2]);
				int y = Mathf.Abs(y1 - parts[tf.name][3]);

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
						
						GameObject quad = Instantiate(quadPrefab);
						quad.transform.parent = tf;
						quad.transform.SetPositionAndRotation(tf.position + tf.right * quadDiameter * j + tf.up * quadDiameter * i, tf.rotation);
						Color color = skin.GetPixel(x1 + j, y1 + i);
						quad.GetComponent<MeshRenderer>().material.color = color;
						Quad quad1 = quad.GetComponent<Quad>();
						quad1.X = x1 + j;
						quad1.Y = y1 + i;
						outSkin.SetPixel(x1 + j, y1 + i, color);
					}
                }
			}
        }
		layer2.SetActive(false);
	}
}