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
		public Func<C, T> GetValue { get; }
		public Action<C, T> SetValue { get; }

		public Tweener(Func<C, T> getter, Action<C, T> setter)
		{
			this.GetValue = getter;
			this.SetValue = setter;
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public Routine To<I>(C context, T to, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			return Full(context, GetValue(context), to, duration, interpolator);
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public Routine From<I>(C context, T from, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			return Full(context, from, GetValue(context), duration, interpolator);
		}

		/// <summary> Tween from the specified value to another. </summary>
		public async Routine Full<I>(C context, T from, T to, float duration, I interpolator) where I : struct, IInterpolator<T>
		{
			SetValue(context, interpolator.Interpolate(from, to, 0));
			var currentTime = 0.0f;
			while (currentTime < duration)
			{
				await Routine.WaitForNextFrame();
				currentTime += Time.deltaTime;
				SetValue(context, interpolator.Interpolate(from, to, Mathf.Min(currentTime / duration, 1.0f)));
			}
		}
	}

	public static class Tween
	{
		//Float extensions

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<float, C> tweener, C context, float to, float duration)
		{
			return tweener.To(context, to, duration, new FloatInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<float, C> tweener, C context, float to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new FloatInterpolator(easing));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<float, C> tweener, C context, float to, float duration)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new FloatInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<float, C> tweener, C context, float to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new FloatInterpolator(easing));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<float, C> tweener, C context, float from, float duration)
		{
			return tweener.From(context, from, duration, new FloatInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<float, C> tweener, C context, float from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new FloatInterpolator(easing));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<float, C> tweener, C context, float from, float duration)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new FloatInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<float, C> tweener, C context, float from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new FloatInterpolator(easing));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<float, C> tweener, C context, float from, float to, float duration)
		{
			return tweener.Full(context, from, to, duration, new FloatInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<float, C> tweener, C context, float from, float to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new FloatInterpolator(easing));
		}

		//Vector2 extensions

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(easing));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.To(context, to, duration, new Vector2Interpolator(easingX, easingY));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector2Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector2Interpolator(easing));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 to, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector2Interpolator(easingX, easingY));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(easing));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.From(context, from, duration, new Vector2Interpolator(easingX, easingY));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector2Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector2Interpolator(easing));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector2Interpolator(easingX, easingY));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(easing));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector2, C> tweener, C context, Vector2 from, Vector2 to, float duration, Func<float, float> easingX, Func<float, float> easingY)
		{
			return tweener.Full(context, from, to, duration, new Vector2Interpolator(easingX, easingY));
		}

		//Vector3 extensions

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(easing));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.To(context, to, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector3Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector3Interpolator(easing));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 to, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(easing));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.From(context, from, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector3Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector3Interpolator(easing));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(easing));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Vector3, C> tweener, C context, Vector3 from, Vector3 to, float duration, Func<float, float> easingX, Func<float, float> easingY, Func<float, float> easingZ)
		{
			return tweener.Full(context, from, to, duration, new Vector3Interpolator(easingX, easingY, easingZ));
		}

		//Color extensions

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easing));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easingRGB, easingA));
		}

		/// <summary> Tween from the current value to the specified value. </summary>
		public static Routine To<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.To(context, to, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Color, C> tweener, C context, Color to, float duration)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new ColorInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easing)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new ColorInterpolator(easing));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new ColorInterpolator(easingRGB, easingA));
		}

		/// <summary> Tween from the current value to a specified offset from that value. </summary>
		public static Routine ToOffset<C>(this Tweener<Color, C> tweener, C context, Color to, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.To(context, tweener.GetValue(context) + to, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easing));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easingRGB, easingA));
		}

		/// <summary> Tween from the specified value back to the current value. </summary>
		public static Routine From<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.From(context, from, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Color, C> tweener, C context, Color from, float duration)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new ColorInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easing)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new ColorInterpolator(easing));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new ColorInterpolator(easingRGB, easingA));
		}

		/// <summary> Tween from the specified offset back to the current value. </summary>
		public static Routine FromOffset<C>(this Tweener<Color, C> tweener, C context, Color from, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.From(context, tweener.GetValue(context) + from, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(Easing.Linear));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easing)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easing));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easingRGB, Func<float, float> easingA)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easingRGB, easingA));
		}

		/// <summary> Tween from the specified value to another. </summary>
		public static Routine Full<C>(this Tweener<Color, C> tweener, C context, Color from, Color to, float duration, Func<float, float> easingR, Func<float, float> easingG, Func<float, float> easingB, Func<float, float> easingA)
		{
			return tweener.Full(context, from, to, duration, new ColorInterpolator(easingR, easingG, easingB, easingA));
		}

		//Built-in tweeners

		/// <summary> Transform.position tweener </summary>
		public static Tweener<Vector3, Transform> Position { get; } = new Tweener<Vector3, Transform>((o) => o.position, (o, v) => o.position = v);

		/// <summary> Transform.eulerAngles tweener </summary>
		public static Tweener<Vector3, Transform> EulerAngles { get; } = new Tweener<Vector3, Transform>((o) => o.eulerAngles, (o, v) => o.eulerAngles = v);

		/// <summary> Transform.localPosition tweener </summary>
		public static Tweener<Vector3, Transform> LocalPosition { get; } = new Tweener<Vector3, Transform>((o) => o.localPosition, (o, v) => o.localPosition = v);

		/// <summary> Transform.localEulerAngles tweener </summary>
		public static Tweener<Vector3, Transform> LocalEulerAngles { get; } = new Tweener<Vector3, Transform>((o) => o.localEulerAngles, (o, v) => o.localEulerAngles = v);

		/// <summary> Transform.localScale tweener </summary>
		public static Tweener<Vector3, Transform> LocalScale { get; } = new Tweener<Vector3, Transform>((o) => o.localScale, (o, v) => o.localScale = v);

		/// <summary> RectTransform.anchoredPosition tweener </summary>
		public static Tweener<Vector2, RectTransform> AnchoredPosition { get; } = new Tweener<Vector2, RectTransform>((o) => o.anchoredPosition, (o, v) => o.anchoredPosition = v);

		/// <summary> RectTransform.sizeDelta tweener </summary>
		public static Tweener<Vector2, RectTransform> SizeDelta { get; } = new Tweener<Vector2, RectTransform>((o) => o.sizeDelta, (o, v) => o.sizeDelta = v);

		/// <summary> SpriteRenderer.color tweener </summary>
		public static Tweener<Color, SpriteRenderer> SpriteColor { get; } = new Tweener<Color, SpriteRenderer>((o) => o.color, (o, v) => o.color = v);

		/// <summary> UI.Text.color tweener </summary>
		public static Tweener<Color, Text> TextColor { get; } = new Tweener<Color, Text>((o) => o.color, (o, v) => o.color = v);

		/// <summary> UI.Image.color tweener </summary>
		public static Tweener<Color, Image> ImageColor { get; } = new Tweener<Color, Image>((o) => o.color, (o, v) => o.color = v);

		/// <summary> Tilemaps.Tilemap.color tweener </summary>
		public static Tweener<Color, Tilemap> TilemapColor { get; } = new Tweener<Color, Tilemap>((o) => o.color, (o, v) => o.color = v);
	}
}
