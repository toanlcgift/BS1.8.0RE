using System;
using UnityEngine;

namespace BeatmapEditor3D
{
	// Token: 0x02000508 RID: 1288
	[Serializable]
	public class UserInputKeyBinding
	{
		// Token: 0x040017CF RID: 6095
		public KeyboardShortcut play = new KeyboardShortcut(KeyCode.Space, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D0 RID: 6096
		public KeyboardShortcut cameraToPreviousLane = new KeyboardShortcut(KeyCode.Q, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D1 RID: 6097
		public KeyboardShortcut cameraToNextLane = new KeyboardShortcut(KeyCode.E, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D2 RID: 6098
		public KeyboardShortcut switchVariablePlayheadSpeed = new KeyboardShortcut(KeyCode.V, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D3 RID: 6099
		public KeyboardShortcut actionMouseButton = new KeyboardShortcut(KeyCode.Mouse0, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D4 RID: 6100
		public KeyboardShortcut changeNoteType = new KeyboardShortcut(KeyCode.Mouse2, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D5 RID: 6101
		public KeyboardShortcut preselectObjectTypeNoteA = new KeyboardShortcut(KeyCode.Alpha1, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D6 RID: 6102
		public KeyboardShortcut preselectObjectTypeNoteB = new KeyboardShortcut(KeyCode.Alpha2, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D7 RID: 6103
		public KeyboardShortcut preselectObjectTypeBomb = new KeyboardShortcut(KeyCode.Alpha3, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D8 RID: 6104
		public KeyboardShortcut preselectObjectTypeObstacle = new KeyboardShortcut(KeyCode.Alpha4, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017D9 RID: 6105
		public KeyboardShortcut activateDeleteMode = new KeyboardShortcut(KeyCode.Alpha5, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DA RID: 6106
		public KeyboardShortcut preselectNoteDirectionAny = new KeyboardShortcut(KeyCode.F, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DB RID: 6107
		public KeyboardShortcut preselectNoteDirectionUp = new KeyboardShortcut(KeyCode.W, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DC RID: 6108
		public KeyboardShortcut preselectNoteDirectionDown = new KeyboardShortcut(KeyCode.S, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DD RID: 6109
		public KeyboardShortcut preselectNoteDirectionLeft = new KeyboardShortcut(KeyCode.A, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DE RID: 6110
		public KeyboardShortcut preselectNoteDirectionRight = new KeyboardShortcut(KeyCode.D, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017DF RID: 6111
		public KeyboardShortcut preselectNoteDirectionUpLeft = new KeyboardShortcut(KeyCode.W, KeyCode.A, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E0 RID: 6112
		public KeyboardShortcut preselectNoteDirectionUpRight = new KeyboardShortcut(KeyCode.W, KeyCode.D, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E1 RID: 6113
		public KeyboardShortcut preselectNoteDirectionDownLeft = new KeyboardShortcut(KeyCode.S, KeyCode.A, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E2 RID: 6114
		public KeyboardShortcut preselectNoteDirectionDownRight = new KeyboardShortcut(KeyCode.S, KeyCode.D, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E3 RID: 6115
		public KeyboardShortcut setNoteDirectionUp = new KeyboardShortcut(KeyCode.W, KeyCode.None, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E4 RID: 6116
		public KeyboardShortcut setNoteDirectionDown = new KeyboardShortcut(KeyCode.S, KeyCode.None, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E5 RID: 6117
		public KeyboardShortcut setNoteDirectionLeft = new KeyboardShortcut(KeyCode.A, KeyCode.None, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E6 RID: 6118
		public KeyboardShortcut setNoteDirectionRight = new KeyboardShortcut(KeyCode.D, KeyCode.None, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E7 RID: 6119
		public KeyboardShortcut setNoteDirectionUpLeft = new KeyboardShortcut(KeyCode.W, KeyCode.A, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E8 RID: 6120
		public KeyboardShortcut setNoteDirectionUpRight = new KeyboardShortcut(KeyCode.W, KeyCode.D, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017E9 RID: 6121
		public KeyboardShortcut setNoteDirectionDownLeft = new KeyboardShortcut(KeyCode.S, KeyCode.A, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017EA RID: 6122
		public KeyboardShortcut setNoteDirectionDownRight = new KeyboardShortcut(KeyCode.S, KeyCode.D, KeyCode.LeftAlt, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017EB RID: 6123
		public KeyCode cameraMovementActivation = KeyCode.Mouse1;

		// Token: 0x040017EC RID: 6124
		public KeyCode fasterCameraMovement = KeyCode.LeftShift;

		// Token: 0x040017ED RID: 6125
		public KeyCode slowerCameraMovement = KeyCode.LeftAlt;

		// Token: 0x040017EE RID: 6126
		public KeyboardShortcut cameraForward = new KeyboardShortcut(KeyCode.W, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017EF RID: 6127
		public KeyboardShortcut cameraBackwards = new KeyboardShortcut(KeyCode.S, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F0 RID: 6128
		public KeyboardShortcut cameraLeft = new KeyboardShortcut(KeyCode.A, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F1 RID: 6129
		public KeyboardShortcut cameraRight = new KeyboardShortcut(KeyCode.D, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F2 RID: 6130
		public KeyboardShortcut cameraUp = new KeyboardShortcut(KeyCode.E, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F3 RID: 6131
		public KeyboardShortcut cameraDown = new KeyboardShortcut(KeyCode.Q, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F4 RID: 6132
		public KeyboardShortcut cameraUp2 = new KeyboardShortcut(KeyCode.Space, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F5 RID: 6133
		public KeyboardShortcut cameraDown2 = new KeyboardShortcut(KeyCode.LeftControl, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Holding);

		// Token: 0x040017F6 RID: 6134
		public KeyboardShortcut resetCameraPosition = new KeyboardShortcut(KeyCode.R, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017F7 RID: 6135
		public KeyboardShortcut cameraPosition1 = new KeyboardShortcut(KeyCode.Alpha1, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017F8 RID: 6136
		public KeyboardShortcut cameraPosition2 = new KeyboardShortcut(KeyCode.Alpha2, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);

		// Token: 0x040017F9 RID: 6137
		public KeyboardShortcut cameraPosition3 = new KeyboardShortcut(KeyCode.Alpha3, KeyCode.None, KeyCode.None, KeyCode.None, KeyPressType.Down);
	}
}
