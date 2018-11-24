//MIT License
//
//Copyright (c) 2018 Tom Blind
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using UnityEngine;

namespace AsyncTweens
{
	public interface IInterpolator<T>
	{
		T Interpolate(T startValue, T endValue, float t);
	}

	public struct FloatInterpolator : IInterpolator<float>
	{
		private Func<float, float> easing;

		public FloatInterpolator(Func<float, float> easing)
		{
			this.easing = easing;
		}

		public float Interpolate(float startValue, float endValue, float t)
		{
			return Mathf.LerpUnclamped(startValue, endValue, easing(t));
		}
	}

	public struct Vector2Interpolator : IInterpolator<Vector2>
	{
		private Func<float, float> easingX;
		private Func<float, float> easingY;

		public Vector2Interpolator(Func<float, float> easingX, Func<float, float> easingY)
		{
			this.easingX = easingX;
			this.easingY = easingY;
		}

		public Vector2Interpolator(Func<float, float> easing)
		{
			this.easingX = easing;
			this.easingY = easing;
		}

		public Vector2 Interpolate(Vector2 startValue, Vector2 endValue, float t)
		{
			return new Vector2(
				Mathf.LerpUnclamped(startValue.x, endValue.x, easingX(t)),
				Mathf.LerpUnclamped(startValue.y, endValue.y, easingY(t))
			);
		}
	}

	public struct Vector3Interpolator : IInterpolator<Vector3>
	{
		private Func<float, float> easingX;
		private Func<float, float> easingY;
		private Func<float, float> easingZ;

		public Vector3Interpolator(Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			this.easingX = easingX;
			this.easingY = easingY;
			this.easingZ = easingZ;
		}

		public Vector3Interpolator(Func<float, float> easing)
		{
			this.easingX = easing;
			this.easingY = easing;
			this.easingZ = easing;
		}

		public Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float t)
		{
			return new Vector3(
				Mathf.LerpUnclamped(startValue.x, endValue.x, easingX(t)),
				Mathf.LerpUnclamped(startValue.y, endValue.y, easingY(t)),
				Mathf.LerpUnclamped(startValue.z, endValue.z, easingZ(t))
			);
		}
	}

	public struct ColorInterpolator : IInterpolator<Color>
	{
		private Func<float, float> easingR;
		private Func<float, float> easingG;
		private Func<float, float> easingB;
		private Func<float, float> easingA;

		public ColorInterpolator(Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			this.easingR = easingR;
			this.easingG = easingG;
			this.easingB = easingB;
			this.easingA = easingA;
		}

		public ColorInterpolator(Func<float, float> easingRGB, Func<float, float> easingA)
		{
			this.easingR = easingRGB;
			this.easingG = easingRGB;
			this.easingB = easingRGB;
			this.easingA = easingA;
		}

		public ColorInterpolator(Func<float, float> easing)
		{
			this.easingR = easing;
			this.easingG = easing;
			this.easingB = easing;
			this.easingA = easing;
		}

		public Color Interpolate(Color startValue, Color endValue, float t)
		{
			return new Color(
				Mathf.LerpUnclamped(startValue.r, endValue.r, easingR(t)),
				Mathf.LerpUnclamped(startValue.g, endValue.g, easingG(t)),
				Mathf.LerpUnclamped(startValue.b, endValue.b, easingB(t)),
				Mathf.LerpUnclamped(startValue.a, endValue.a, easingA(t))
			);
		}
	}
}
