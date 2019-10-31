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
	[Serializable]
	public class Easing
	{
		public enum Type
		{
			Linear = 0,
			AnimationCurve,
			SineIn,
			SineOut,
			SineInOut,
			QuadIn,
			QuadOut,
			QuadInOut,
			CubicIn,
			CubicOut,
			CubicInOut,
			QuartIn,
			QuartOut,
			QuartInOut,
			QuintIn,
			QuintOut,
			QuintInOut,
			ExpoIn,
			ExpoOut,
			ExpoInOut,
			CircIn,
			CircOut,
			CircInOut,
			BackIn,
			BackOut,
			BackInOut,
			ElasticIn,
			ElasticOut,
			ElasticInOut,
			BounceIn,
			BounceOut,
			BounceInOut,
		}

		public Type type;
		public AnimationCurve curve;

		public static implicit operator Func<float, float>(Easing easing)
		{
			return (easing.type == Type.AnimationCurve) ? easing.curve.Evaluate : func[(int)easing.type];
		}

		public static float Linear(float t)
		{
			return t;
		}

		public static float SineIn(float t)
		{
			return -1 * Mathf.Cos(t * (Mathf.PI / 2)) + 1;
		}

		public static float SineOut(float t)
		{
			return Mathf.Sin(t * (Mathf.PI / 2));
		}

		public static float SineInOut(float t)
		{
			return -0.5f * (Mathf.Cos(Mathf.PI * t) - 1);
		}

		public static float QuadIn(float t)
		{
			return t * t;
		}

		public static float QuadOut(float t)
		{
			return -1 * t * (t - 2);
		}

		public static float QuadInOut(float t)
		{
			if ((t *= 2) < 1)
			{
				return 0.5f * t * t;
			}
			return -0.5f * ((--t) * (t - 2) - 1);
		}

		public static float CubicIn(float t)
		{
			return t * t * t;
		}

		public static float CubicOut(float t)
		{
			return ((t = t - 1) * t * t + 1);
		}

		public static float CubicInOut(float t)
		{
			if ((t *= 2) < 1)
			{
				return 0.5f * t * t * t;
			}
			return 0.5f * ((t -= 2) * t * t + 2);
		}

		public static float QuartIn(float t)
		{
			return t * t * t * t;
		}

		public static float QuartOut(float t)
		{
			return -1 * ((t = t - 1) * t * t * t - 1);
		}

		public static float QuartInOut(float t)
		{
			if ((t *= 2) < 1)
			{
				return 0.5f * t * t * t * t;
			}
			return -0.5f * ((t -= 2) * t * t * t - 2);
		}

		public static float QuintIn(float t)
		{
			return t * t * t * t * t;
		}

		public static float QuintOut(float t)
		{
			return ((t = t - 1) * t * t * t * t + 1);
		}

		public static float QuintInOut(float t)
		{
			if ((t *= 2) < 1)
			{
				return 0.5f * t * t * t * t * t;
			}
			return 0.5f * ((t -= 2) * t * t * t * t + 2);
		}

		public static float ExpoIn(float t)
		{
			return (t == 0) ? 0 : Mathf.Pow(2, 10 * (t - 1));
		}

		public static float ExpoOut(float t)
		{
			return (t == 1) ? 1 : (-Mathf.Pow(2, -10 * t) + 1);
		}

		public static float ExpoInOut(float t)
		{
			if (t == 0)
			{
				return 0;
			}
			if (t == 1)
			{
				return 1;
			}
			if ((t *= 2) < 1)
			{
				return 0.5f * Mathf.Pow(2, 10 * (t - 1));
			}
			return 0.5f * (-Mathf.Pow(2, -10 * --t) + 2);
		}

		public static float CircIn(float t)
		{
			return -1 * (Mathf.Sqrt(1 - t * t) - 1);
		}

		public static float CircOut(float t)
		{
			return Mathf.Sqrt(1 - (t = t - 1) * t);
		}

		public static float CircInOut(float t)
		{
			if ((t *= 2) < 1)
			{
				return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
			}
			return 0.5f * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
		}

		public static float BackIn(float t)
		{
			var s = 1.70158f;
			return t * t * ((s + 1) * t - s);
		}

		public static float BackOut(float t)
		{
			var s = 1.70158f;
			return ((t = t - 1) * t * ((s + 1) * t + s) + 1);
		}

		public static float BackInOut(float t)
		{
			var s = 1.70158f;
			if ((t *= 2) < 1)
			{
				return 0.5f * (t * t * (((s *= (1.525f)) + 1) * t - s));
			}
			return 0.5f *(( t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2);
		}

		public static float ElasticIn(float t)
		{
			if (t == 0)
			{
				return 0;
			}
			if (t == 1)
			{
				return 1;
			}
			var p = 0.3f;
			var s = p / 4;
			var postFix = Mathf.Pow(2, 10 * (t -= 1));
			return -(postFix * Mathf.Sin((t * 1 - s) * (2 * Mathf.PI) / p));
		}

		public static float ElasticOut(float t)
		{
			if (t == 0)
			{
				return 0;
			}
			if (t == 1)
			{
				return 1;
			}
			var p = 0.3f;
			var s = p / 4;
			return (Mathf.Pow(2, -10 * t) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p) + 1);
		}

		public static float ElasticInOut(float t)
		{
			var p = (0.3f * 1.5f);
			if (t == 0)
			{
				return 0;
			}
			if ((t *= 2) == 2)
			{
				return 1;
			}
			var s = p / (2 * Mathf.PI) * Mathf.Asin(1);
			if (t < 1)
			{
				return -0.5f * (Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) / p));
			}
			return Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t - s) * (2 * Mathf.PI) /p) * 0.5f + 1;
		}

		public static float BounceIn(float t)
		{
			return EaseInBounce(t);
		}

		public static float BounceOut(float t)
		{
			return EaseOutBounce(t);
		}

		public static float BounceInOut(float t)
		{
			if (t < 0.5f)
			{
				return EaseInBounce(t * 2) * 0.5f;
			}
			return EaseOutBounce(t * 2 - 1) * 0.5f + 0.5f;
		}

		private static float EaseOutBounce(float t)
		{
			if (t < (1 / 2.75f))
			{
				return (7.5625f * t * t);
			}
			else if (t < (2 / 2.75f))
			{
				float postFix = t -= (1.5f / 2.75f);
				return (7.5625f * (postFix) * t + 0.75f);
			}
			else if (t < (2.5f / 2.75f))
			{
				float postFix = t -= (2.25f / 2.75f);
				return (7.5625f * (postFix) * t + 0.9375f);
			}
			else
			{
				float postFix = t -= (2.625f / 2.75f);
				return (7.5625f * (postFix) * t + 0.984375f);
			}
		}

		private static float EaseInBounce(float t)
		{
			return 1 - EaseOutBounce(1 - t);
		}

		private static readonly Func<float, float>[] func = new Func<float, float>[]
		{
			Linear,
			null,
			SineIn,
			SineOut,
			SineInOut,
			QuadIn,
			QuadOut,
			QuadInOut,
			CubicIn,
			CubicOut,
			CubicInOut,
			QuartIn,
			QuartOut,
			QuartInOut,
			QuintIn,
			QuintOut,
			QuintInOut,
			ExpoIn,
			ExpoOut,
			ExpoInOut,
			CircIn,
			CircOut,
			CircInOut,
			BackIn,
			BackOut,
			BackInOut,
			ElasticIn,
			ElasticOut,
			ElasticInOut,
			BounceIn,
			BounceOut,
			BounceInOut,
		};
	}
}
