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

using UnityEngine;
using UnityEditor;

namespace AsyncTweens
{
	[CustomPropertyDrawer(typeof(Easing))]
	public class EasingEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, GUIContent.none, property);
			position = EditorGUI.PrefixLabel(position, label);

			var type = property.FindPropertyRelative("type");
			EditorGUI.BeginProperty(position, GUIContent.none, type);
			EditorGUI.PropertyField(position, type, GUIContent.none, true);
			EditorGUI.EndProperty();

			if (type.intValue == (int)Easing.Type.AnimationCurve)
			{
				var curve = property.FindPropertyRelative("curve");
				position.y += EditorGUI.GetPropertyHeight(type, true);
				EditorGUI.BeginProperty(position, GUIContent.none, curve);
				EditorGUI.PropertyField(position, curve, GUIContent.none, true);
				EditorGUI.EndProperty();
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			var type = property.FindPropertyRelative("type");
			var height = EditorGUI.GetPropertyHeight(type, true);
			if (type.intValue == (int)Easing.Type.AnimationCurve)
			{
				var curve = property.FindPropertyRelative("curve");
				height += EditorGUI.GetPropertyHeight(curve, true);
			}
			return height;
		}
	}
}
