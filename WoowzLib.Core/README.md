<div align="center">

![Иконка](Icon.png)

![Typing SVG](https://readme-typing-svg.demolab.com?font=Science+Gothic+&weight=900&size=70&duration=1&pause=100000&color=E6E6E6&center=true&vCenter=true&width=700&height=70&lines=WoowzLib.Core)

[![License](https://img.shields.io/badge/License-CC_BY_SA_4.0-blue)](https://creativecommons.org/licenses/by-sa/4.0/)

## Информация

</div>

``WoowzLib.Core`` — Основа библиотеки WoowzLib, на ней основываются модули библиотеки.

> [!WARNING]
> Библиотека работат только на Windows, и поддерживает только Русский язык!

### Детали
* Net 8.0
* Только Windows
* Всё на русском, комментарии, ошибки
* Много try/catch

### Ссылки
[NuGet](https://www.nuget.org/packages/Woowz11.WoowzLib) • [GitHub](https://github.com/Woowz11/WoowzLib/tree/main/WoowzLib.Core)

<div align="center">

## Под-Модули

|Название             |Информация |
|:--------------------|:----------|
|WoowzLib             | Содержит в себе запуск WoowzLib |
|Math                 | Различные матиматические функции |
|Math.Byte            | Работа с байтами |
|Math.Random          | Работа со случайностями (рандомом) |
|Math.Time            | Работа со временем |
|String               | Работа со строками |
|System               | Работа со системой и различные другие функции (Пока-что только Windows!) |
|System.Console       | Работа с консолью |
|System.Tick          | Работа с тиками и потоком (тут есть ограничения по DeltaTime) |
|System.HDC           | Работа с HDC рисованием |
|System.Native        | Работа с Native кодом, загрузка DLL файлов и т.д |
|System.Native.Windows| Функции из ``kernel32.dll``, ``user32.dll``, ``gdi32.dll``, ``msimg32.dll`` |
|Input.Mouse          | Работа с мышью |
|Input.Keyboard       | Работа с клавиатурой |
|Explorer.File        | Работа с файлами |
|Explorer.Folder      | Работа с папками |
|Explorer.Temp        | Работа с временными файлами |

## Больше информации

### WoowzLib
</div>
<details><summary>Смотреть...</summary>

Прежде чем работать с WoowzLib, его нужно запустить, вот пример кода:
```csharp
public static int Main(string[] Args){
	try{
		WL.WoowzLib.Start(new WoowzLibInfo( // WoowzLibInfo — это информация об проекте, она не обязательная, пока-что используется только для отладки и для Vulkan
			Name         : "New Project", // Название проекта
			Version      : 0,             // Версия проекта
			Engine       : "WoowzLib",    // Движок проекта
			EngineVersion: 0,             // Версия движка проекта
			Author       : "Anonymous",   // Автор проекта
			License      : "MIT"          // Лицензия проекта
		));
		
		// Тут сам код, уже можно писать
		
		// Если приложение живёт больше 1 раза
		/*
		
		while(...){
			WL.WoowzLib.Update();
		}
		
		*/
		
	}catch(Exception e){
		Logger.Fatal("Произошла фатальная ошибка внутри самого приложения!", e);
		return 1;
	}
	
	return 0;
}
```

Останавливать WoowzLib не нужно, он сам остановится после закрытия приложения, если вызвать несколько раз ``WL.WoowzLib.Start``, будет ошибка

Доступ к модулям происходит через префикс ``WL.``, к примеру для Math будет ``WL.Math``

Публичные функции и переменные начинающиеся на ``__`` используйте если разбираетесь в том что делаете, это функции библиотеки *(их вызов и изменение может всё сломать!)*

Так же есть ивенты, на остановку, или запуск, и т.д

Если что переделывает сообщения в консоли на свой формат, лучше выводить сообщения в консоли так:
```csharp
/*
Выводит сообщения в консоль
Info  - Информационное сообщение, белого цвета
Warn  - Предупреждение, жёлтого цвета
Error - Ошибка, красного цвета,
Fatal - Фатальная ошибка, пурпурного цвета
Debug - Отладочное сообщение, зелёного цвета

Сами по себе функции ничего полезного не дают, только префикс в начале, типо [INFO]

Что-бы были цвета и к примеру timestamp каждое сообщение и другая информация, нужно установить WoowzLib.Logger (https://github.com/Woowz11/WoowzLib/blob/main/WoowzLib.Logger/README.md)
*/

Logger.Info ("INFO");
Logger.Warn ("WARN", true, false, null);
Logger.Error("ERROR", new Exception("Пример ошибки"));
Logger.Fatal("FATAL", "Another string", 320);
Logger.Debug("DEBUG", new Vector2F(120, -120));
```

</details>
<div align="center">

### Math
</div>
<details><summary>Смотреть...</summary>

Содержит обычные математические формулы, начиная с простых, заканчивая сложными

``Add``, ``Max``, ``Clamp``, ``Lerp``, ``Sign``, ``MulExact``, ``Root``, ``Truncate``, ``Above``, ``Average``, ``DSin``, ``IsNear``, ``Fma``, ``Evan`` и т.д

По дефолту он работает с ``float`` значениями, если нужны определённые значения, то в конце функции надо добавить:

|Тип значения|Буква|
|-----:|-|
|double|D|
|int   |I|
|byte  |B|
|uint  |U|

Содержит комментарии с информацией об формулах и результатах

Так же он добавляет собственные Vector, Color, Rect, Massive, они все struct

Они генерируются через [GeneratorWoowzLib](https://github.com/Woowz11/WoowzLib/tree/main/Other/Generator/GeneratorWoowzLib), для уменьшение человеческого фактора

Есть ``2``, ``3``, ``4`` и ``float``, ``double``, ``int``, ``uint`` вектора

Есть ``float``, ``double``, ``int``, ``byte`` цвета

Есть ``float``, ``double``, ``int`` Rect

> [!WARNING]
> Массивы в разработке

Есть ``byte``, ``char``, ``double``, ``float``, ``int``, ``long``, ``short``, ``uint``, ``ulong``, ``ushort``, ``<T>`` массивы

</details>
<div align="center">

### Math.Byte
</div>
<details><summary>Смотреть...</summary>

Можно объеденять байты в число и наоборот, разбор цветов uint, определение размера и т.д

</details>
<div align="center">

### Math.Random
</div>
<details><summary>Смотреть...</summary>

Получение случайных чисел разными методами, есть и быстрые варианты, и качественные (WIP)

</details>
<div align="center">

### Math.Time
</div>
<details><summary>Смотреть...</summary>

Можно узнать текущее время, год, месяц, неделя, день, час, минута, секунда, миллисекунда, сколько программа кадров существует, или секунд и т.д

</details>
<div align="center">

### String
</div>
<details><summary>Смотреть...</summary>

Содержит функции связанные со строками

Объеденение строк по различным формулам, форматирование и т.д

</details>
<div align="center">

### System
</div>
<details><summary>Смотреть...</summary>

Можно узнать папку в которой запущено приложение, тип приложения, тип ОС

</details>
<div align="center">

### System.Console
</div>
<details><summary>Смотреть...</summary>

Поменять название консоли, кодировка, видимость

</details>
<div align="center">

### System.Tick
</div>
<details><summary>Смотреть...</summary>

Позволяет ограничивать поток, к примеру:

```csharp
while(...){
	// Главное что-бы был уникальный ID у функции (это где цифра 1), нужно для работы
	WL.System.Tick.LimitFPS(1, 300, TD => {
		// Это будет вызываться как при 300 FPS, TD содержит в себе DeltaTime и другую информацию
	});
	
	// Это будет вызываться каждый раз
}
```

Конветрация FPS в DeltaTime и наоборот, или высчитывать сколько прошло времени через Start и Stop

</details>
<div align="center">

### System.HDC
</div>
<details><summary>Смотреть...</summary>

Рисование через HDC GDI Windows

Создание кистей, заполнение области, рисование изображений, текста, обрезка

</details>
<div align="center">

### System.Native
</div>
<details><summary>Смотреть...</summary>

Работа с Native

Загрузка и разгрузка DLL файлов

Получение функций из DLL файлов и делегация, сохранение данных в памяти, и освобождение

</details>
<div align="center">

### System.Native.Windows
</div>
<details><summary>Смотреть...</summary>

Большая часть функций из DLL ``kernel32.dll``, ``user32.dll``, ``gdi32.dll``, ``msimg32.dll``

</details>
<div align="center">

### Input.Mouse
</div>
<details><summary>Смотреть...</summary>

Пока-что можно только получить позицию мыши

</details>
<div align="center">

### Input.Keyboard
</div>
<details><summary>Смотреть...</summary>

Узнать какие клавиши сейчас нажимаются

```csharp
WL.Input.Keyboard.OnDown += (Key, Code) => {
	Logger.Info("DOWN", Key, Code);
};

WL.Input.Keyboard.OnUp += (Key, Code) => {
	Logger.Info("UP", Key, Code);
};
```

</details>
<div align="center">

### Explorer.File
</div>
<details><summary>Смотреть...</summary>

Взаимодействие с файлами

</details>
<div align="center">

### Explorer.Folder
</div>
<details><summary>Смотреть...</summary>

Взаимодействие с папками

</details>
<div align="center">

### Explorer.Temp
</div>
<details><summary>Смотреть...</summary>

Взаимодействие с временными файлами

</details>