/* Сгенерировано с помощью WoowzLibGenerator 0.0.1.386, внутри класса "Tests.cs" */
using WLO.Transform;
using WLO.Vector;
using WLO;
namespace WoowzLibTest.Tests;
public static class Test_Transform{
	public static void Run(){
		Test.Run("Transform2I (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform2I();
				Test.CheckResult(t.Position.Value, Vector2I.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector2I.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform2I();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform2I();
				t.Type = TransformType.Position;
				t.Position.Value = Vector2I.Left;
				Test.CheckResult(t.Position.Value, Vector2I.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform2I();
				t.Type = TransformType.Size;
				t.Size.Value = Vector2I.Left;
				Test.CheckResult(t.Size.Value, Vector2I.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform2I();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform2I();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector2I.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
		Test.Run("Transform3I (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform3I();
				Test.CheckResult(t.Position.Value, Vector3I.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector3I.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform3I();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform3I();
				t.Type = TransformType.Position;
				t.Position.Value = Vector3I.Left;
				Test.CheckResult(t.Position.Value, Vector3I.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform3I();
				t.Type = TransformType.Size;
				t.Size.Value = Vector3I.Left;
				Test.CheckResult(t.Size.Value, Vector3I.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform3I();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform3I();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector3I.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
		Test.Run("Transform2F (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform2F();
				Test.CheckResult(t.Position.Value, Vector2F.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector2F.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform2F();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform2F();
				t.Type = TransformType.Position;
				t.Position.Value = Vector2F.Left;
				Test.CheckResult(t.Position.Value, Vector2F.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform2F();
				t.Type = TransformType.Size;
				t.Size.Value = Vector2F.Left;
				Test.CheckResult(t.Size.Value, Vector2F.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform2F();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform2F();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector2F.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
		Test.Run("Transform3F (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform3F();
				Test.CheckResult(t.Position.Value, Vector3F.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector3F.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform3F();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform3F();
				t.Type = TransformType.Position;
				t.Position.Value = Vector3F.Left;
				Test.CheckResult(t.Position.Value, Vector3F.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform3F();
				t.Type = TransformType.Size;
				t.Size.Value = Vector3F.Left;
				Test.CheckResult(t.Size.Value, Vector3F.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform3F();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform3F();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector3F.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
		Test.Run("Transform2D (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform2D();
				Test.CheckResult(t.Position.Value, Vector2D.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector2D.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform2D();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform2D();
				t.Type = TransformType.Position;
				t.Position.Value = Vector2D.Left;
				Test.CheckResult(t.Position.Value, Vector2D.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform2D();
				t.Type = TransformType.Size;
				t.Size.Value = Vector2D.Left;
				Test.CheckResult(t.Size.Value, Vector2D.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform2D();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform2D();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector2D.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
		Test.Run("Transform3D (GENERATED)", () => {
			Test.F("Создание", () => {
				var t = new Transform3D();
				Test.CheckResult(t.Position.Value, Vector3D.Zero, "Position default неверный!");
				Test.CheckResult(t.Size.Value, Vector3D.One, "Size default неверный!");
				Test.CheckResult(t.Rotation.Value, false, "Rotation default неверный!");
			});
			Test.F("Flags", () => {
				var t = new Transform3D();
				t.Type = TransformType.None;
				Test.CheckResult(t.SupportPosition, false, "SupportPosition не работает!");
				Test.CheckResult(t.SupportSize, false, "SupportSize не работает!");
				Test.CheckResult(t.SupportRotation, false, "SupportRotation не работает!");
				t.Type = TransformType.All;
				Test.CheckResult(t.SupportPosition, true, "SupportPosition не работает! 2");
				Test.CheckResult(t.SupportSize, true, "SupportSize не работает! 2");
				Test.CheckResult(t.SupportRotation, true, "SupportRotation не работает! 2");
			});
			Test.F("Изменение Position", () => {
				var t = new Transform3D();
				t.Type = TransformType.Position;
				t.Position.Value = Vector3D.Left;
				Test.CheckResult(t.Position.Value, Vector3D.Left, "Position установка не работает!");
			});
			Test.F("Изменение Size", () => {
				var t = new Transform3D();
				t.Type = TransformType.Size;
				t.Size.Value = Vector3D.Left;
				Test.CheckResult(t.Size.Value, Vector3D.Left, "Size установка не работает!");
			});
			Test.F("Изменение Rotation", () => {
				var t = new Transform3D();
				t.Type = TransformType.Rotation;
				t.Rotation.Value = true;
				Test.CheckResult(t.Rotation.Value, true, "Rotation установка не работает!");
			});
			Test.F("OnChanged событие", () => {
				var t = new Transform3D();
				bool Called = false;
				t.OnChanged += (_, _, _) => {
					Called = true;
				};
				t.Position.Value = Vector3D.Left;
				Test.CheckResult(Called, true, "OnChanged не работает!");
			});
		});
	}
}