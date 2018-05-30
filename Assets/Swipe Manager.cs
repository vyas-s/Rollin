﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection{
	None = 0,
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8, 
}

public class SwipeManager : MonoBehaviour {

	private static SwipeManager instance;
	public static SwipeManager Instance{get {return instance;}}
	public SwipeDirection Direction{set;get;}
	private Vector3 touchPosition;
	private float swipeResistanceX = 50.0f;
	private float swipeResistanceY = 100.0f;

	private void Start(){
		instance = this;
	}

	private void Update(){
		Direction = SwipeDirection.None;

		if(Input.GetMouseButtonDown (0))
		{
			touchPosition = Input.mousePosition;
		}

		if(Input.GetMouseButtonUp (0))
		{
			Vector2 deltaSwipe = touchPosition - Input.mousePosition;
			if(Mathf.Abs (deltaSwipe.x) > swipeResistanceX)
			{
				//x swipe
				Direction |= (deltaSwipe.x<0) ? SwipeDirection.Right :  SwipeDirection.Left;
			}
			if(Mathf.Abs (deltaSwipe.y) > swipeResistanceY)
			{
				//x swipe
				Direction |= (deltaSwipe.y<0) ? SwipeDirection.Up :  SwipeDirection.Down;
			}

		}
	}

	public bool IsSwiping(SwipeDirection dir) {
        if (dir == Direction) {
            return true;
        }
        else return false;
    }﻿

}
