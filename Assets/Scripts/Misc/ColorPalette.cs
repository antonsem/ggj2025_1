using UnityEngine;

namespace BubbleHell.Misc
{
	[CreateAssetMenu(fileName = "ColorPalette", menuName = "The Bubble Hell/Color Palette")]
	public class ColorPalette : ScriptableObject
	{
		[field: SerializeField] public Color[] Colors { get; private set; }
	}
}
