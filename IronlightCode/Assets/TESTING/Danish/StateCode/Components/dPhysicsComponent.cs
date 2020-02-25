﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components
{
    public class dPhysicsComponent
    {
		#region Wasiq's Input
		//Wasiq's Input
		public bool LillyPad;
		public bool Ground;
		public bool FallingLeaf;
		public LayerMask layerMaskGround;
		public LayerMask layerMaskLillyPad;
		public LayerMask layerMaskFallingLeaf;
		Transform player;
		Vector3 startPoint;
		Vector3 endPoint;
		Color rayColor;
		RaycastHit hit;
		public float addedDist = 0.5f;
		//End of Wasiq's input

		private Rigidbody m_Rigid;
		Vector3 velocity = Vector3.zero;
		float GravityModifier = 1;

		public bool isGrounded = false;

		public void Init(Rigidbody rigidbody, float gravityLevel)
		{
			m_Rigid = rigidbody;
			GravityModifier = gravityLevel;
		}

		public void Tick()
		{
			isGrounded = GroundCheck();
		}

		public void FixedTick()
		{
			if (!isGrounded)
			{
				ApplyGravity();
			}
		}


		void ApplyGravity()
		{
			velocity += GravityModifier * Physics.gravity * Time.deltaTime;


			m_Rigid.velocity = velocity;
		}

		public bool GroundCheck()
		{
			startPoint = m_Rigid.position;
			endPoint = new Vector3(startPoint.x, startPoint.y - addedDist, startPoint.z);
			Debug.DrawLine(startPoint, endPoint, rayColor);
			LillyPad = Physics.Linecast(startPoint, endPoint, out hit, layerMaskLillyPad);
			Ground = Physics.Linecast(startPoint, endPoint, out hit, layerMaskGround);
			FallingLeaf = Physics.Linecast(startPoint, endPoint, out hit, layerMaskFallingLeaf);

			if (LillyPad || FallingLeaf || Ground)
			{
				Debug.Log("LineCast hit!");
				rayColor = Color.green;
				return true;
				//hit.collider.gameObject.GetComponent<LillyPad>().falllillypad();
			}
			else
			{
				rayColor = Color.red;
				return false;
				// NOT GROUNDED HERE
			}
		}













			#endregion


			#region version1
			//private Rigidbody m_Rigid;
			//      Vector3 velocity = Vector3.zero;
			//      float GravityModifier = 1;

			//      public bool isGrounded = false;

			//      public void Init(Rigidbody rigidbody, float gravityLevel) 
			//      {
			//          m_Rigid = rigidbody;
			//          GravityModifier = gravityLevel;
			//      }

			//      public void Tick() 
			//      {
			//          isGrounded = GroundCheck();
			//      }

			//      public void FixedTick() 
			//      {
			//          if (!isGrounded)
			//          {
			//              ApplyGravity();
			//          }
			//      }


			//      void ApplyGravity()
			//      {
			//          velocity += GravityModifier * Physics.gravity * Time.deltaTime;


			//          m_Rigid.velocity = velocity;
			//      }

			//      public bool GroundCheck()
			//      {
			//          RaycastHit hit;
			//          if (m_Rigid.SweepTest(Vector3.down, out hit, 0.1f))
			//          {
			//              if (hit.collider.gameObject.layer == 10)
			//              {
			//                  Debug.Log(hit.distance + " Ground check = " + isGrounded);
			//                  if (hit.distance > 0.04f)
			//                  {
			//                      return false;
			//                  }
			//                  else
			//                  {
			//                      return true;
			//                  }
			//              }
			//              else
			//              {
			//                  return false;
			//              }
			//          }

			//          else
			//          {
			//              return false;
			//          }
			//      }
			#endregion
		}


}