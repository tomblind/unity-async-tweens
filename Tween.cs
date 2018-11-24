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
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using AsyncRoutines;

namespace AsyncTweens
{
	public struct Tweener<T, C>
	{
		private Func<C, T> getter;
		private Action<C, T> setter;

		public Tweener(Func<C, T> getter, Action<C, T> setter)
		{
			this.getter = getter;
			this.setter = setter;
		}

		public Routine To<I>(C context, T to, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			return Full(context, getter(context), to, duration, interpolator);
		}

		public Routine From<I>(C context, T from, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			return Full(context, from, getter(context), duration, interpolator);
		}

		public async Routine Full<I>(C context, T from, T to, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			setter(context, interpolator.Interpolate(from, to, 0));
			var currentTime = 0.0f;
			while (currentTime < duration)
			{
				await Routine.WaitForNextFrame();
				currentTime += Time.deltaTime;
				setter(context, interpolator.Interpolate(from, to, Mathf.Min(currentTime / duration, 1.0f)));
			}
		}
	}

	public static class Tween
	{
		//Float extensions
		public static Routine To<C>(this Tweener<float, C> tweener, C context, float to, float duration)
		{
			return tweener.To(context, to, duration, new FloatInterpolator(Easing.Linear));
		}

		public static Routine To<C>(this Tweener<float, C> tweener, C context, float to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new FloatInterpolator(easing));
		}

		public static Routine From<C>(this Tweener<float, C> tweener, C context, float from, float duration)
		{
			return tweener.From(context, from, duration, new FloatInterpolator(Easing.Linear));
		}

		public static Routine From<C>(this Tweener<float, C> tweener, C context, float from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new FloatInterpolator(easing));
		}

		public static Routine Full<C>(this Tweener<float, C> tweener, C context, float from, float to, float duration)
		{
			return tweener.Full(context, from, to, duration, new FloatInterpolator(Easing.Linear));
		}

		public static Routine Full<C>(this Tweener<float, C> tweener, C context, float from, float to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new FloatInterpolator(easing));
		}

		//Vector2 extensions
		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(Easing.Linear));
		}

		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(easing));
		}

		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(easingX, easingY));
		}

		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(Easing.Linear));
		}

		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(easing));
		}

		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(easingX, easingY));
		}

		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(Easing.Linear));
		}

		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(easing));
		}

		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(easingX, easingY));
		}

		//Vector3 extensions
		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(Easing.Linear));
		}

		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(easing));
		}

		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(Easing.Linear));
		}

		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(easing));
		}

		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(Easing.Linear));
		}

		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(easing));
		}

		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		//Color extensions
		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(Easing.Linear));
		}

		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easing));
		}

		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easingRGB, easingA));
		}

		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(Easing.Linear));
		}

		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easing));
		}

		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easingRGB, easingA));
		}

		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(Easing.Linear));
		}

		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easing));
		}

		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easingRGB, easingA));
		}

		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		//Built-in tweeners
		public static Tweener<Vector3, Transform> Position { get; } = new Tweener<Vector3, Transform>((o) => o.position, (o, v) => o.position = v);
		public static Tweener<Vector3, Transform> EulerAngles { get; } = new Tweener<Vector3, Transform>((o) => o.eulerAngles, (o, v) => o.eulerAngles = v);
		public static Tweener<Vector3, Transform> LocalPosition { get; } = new Tweener<Vector3, Transform>((o) => o.localPosition, (o, v) => o.localPosition = v);
		public static Tweener<Vector3, Transform> LocalEulerAngles { get; } = new Tweener<Vector3, Transform>((o) => o.localEulerAngles, (o, v) => o.localEulerAngles = v);
		public static Tweener<Vector3, Transform> LocalScale { get; } = new Tweener<Vector3, Transform>((o) => o.localScale, (o, v) => o.localScale = v);

		public static Tweener<Vector2, RectTransform> AnchoredPosition { get; } = new Tweener<Vector2, RectTransform>((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v);
		public static Tweener<Vector2, RectTransform> SizeDelta { get; } = new Tweener<Vector2, RectTransform>((o) => o.sizeDelta, (o, v) => o.sizeDelta = v);

		public static Tweener<Color, SpriteRenderer> SpriteColor { get; } = new Tweener<Color, SpriteRenderer>((o) => o.color, (o, v) => o.color = v);
		public static Tweener<Color, Text> TextColor { get; } = new Tweener<Color, Text>((o) => o.color, (o, v) => o.color = v);
		public static Tweener<Color, Image> ImageColor { get; } = new Tweener<Color, Image>((o) => o.color, (o, v) => o.color = v);
		public static Tweener<Color, Tilemap> TilemapColor { get; } = new Tweener<Color, Tilemap>((o) => o.color, (o, v) => o.color = v);
	}
}
