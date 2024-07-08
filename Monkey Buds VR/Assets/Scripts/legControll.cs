using System.Collections;
using UnityEngine;

public class legControll : MonoBehaviour
{
	public Transform leftTarget;

	public Transform rightTarget;

	public Transform Lray;

	public Transform Rray;

	public Transform Hips;

	public float LegStepUp;

	private Vector3 ShouldBeL;

	private Vector3 ShouldBeR;

	private Vector3 ShouldReallyBeL;

	private Vector3 ShouldReallyBeR;

	public float DistL;

	public float DistR;

	private bool LStepping;

	private bool RStepping;

	private string LastStep;

	public float wantStepAt;

	public float legSpeed;

	private void Start()
	{
		ShouldBeL = leftTarget.position;
		ShouldBeR = rightTarget.position;
		LastStep = "R";
	}

	private void Update()
	{
		if (Physics.Raycast(Lray.position, Vector3.down, out var hitInfo))
		{
			ShouldReallyBeL = hitInfo.point;
		}
		if (Physics.Raycast(Rray.position, Vector3.down, out var hitInfo2))
		{
			ShouldReallyBeR = hitInfo2.point;
		}
		DistL = Vector3.Distance(ShouldBeL, ShouldReallyBeL);
		DistR = Vector3.Distance(ShouldBeR, ShouldReallyBeR);
		leftTarget.position = Vector3.Lerp(leftTarget.position, ShouldBeL, legSpeed * Time.deltaTime);
		rightTarget.position = Vector3.Lerp(rightTarget.position, ShouldBeR, legSpeed * Time.deltaTime);
		if (!LStepping && !RStepping)
		{
			if (LastStep == "R")
			{
				if (DistL > wantStepAt)
				{
					StartCoroutine(stepL());
					LastStep = "L";
				}
			}
			else if (LastStep == "L" && DistR > wantStepAt)
			{
				StartCoroutine(stepR());
				LastStep = "R";
			}
		}
		leftTarget.transform.eulerAngles = new Vector3(0f, Hips.eulerAngles.y, 0f);
		rightTarget.transform.eulerAngles = new Vector3(0f, Hips.eulerAngles.y, 0f);
	}

	private IEnumerator stepL()
	{
		LStepping = true;
		ShouldBeL = new Vector3(ShouldBeL.x, ShouldReallyBeL.y + LegStepUp, ShouldBeL.z);
		yield return new WaitForSeconds(0.3f);
		ShouldBeL = ShouldReallyBeL;
		yield return new WaitForSeconds(0.2f);
		LStepping = false;
	}

	private IEnumerator stepR()
	{
		RStepping = true;
		ShouldBeR = new Vector3(ShouldBeR.x, ShouldReallyBeR.y + LegStepUp, ShouldBeR.z);
		yield return new WaitForSeconds(0.3f);
		ShouldBeR = ShouldReallyBeR;
		yield return new WaitForSeconds(0.2f);
		RStepping = false;
	}
}
