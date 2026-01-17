<div align="center">

![Typing SVG](https://readme-typing-svg.demolab.com?font=Science+Gothic+&weight=900&size=70&duration=1&pause=100000&color=E6E6E6&center=true&vCenter=true&width=500&height=60&lines=WoowzLib)

[![License](https://img.shields.io/badge/License-CC_BY_SA_4.0-blue)](https://creativecommons.org/licenses/by-sa/4.0/)

## Информация

</div>

``WoowzLib`` — Это модульная библиотека для C#

Упрощает работу с GLFW, GL и т.д, добавляет кеширование, и предупреждает если где-то что-то не так, к примеру имеет авто-очистки, содержит комментарии и try/catch и всё на Русском языке

Использует Net 8.0

<div align="center">

## Модули

|Название   |Что делает?|
|:---------:|:---------|
|Core       |Основа для всех модулей, работа с Console, Kernel и т.д|
|Math       |Математика, Random, время, вектора, цвета, и т.д|
|String     |Работа со строками|
|Explorer   |Работа с файлами и папками|
|Byte       |Работа с байтами, собственные массивы|
|Logger     |Собственный обработчик сообщений (с цветами, временем и т.д)|
|Native     |Загрузка DLL, работа с памятью|
|WindowsForm|Создание WinForm окнон|
|GLFW       |Создание GLFW окнон|
|GL         |OpenGL рендер (> 4.6)|

## Пример

</div>

В начале кода нужно написать ``WL.WoowzLib.Start();``, что-бы модули правильно инициализировались

<div align="center">

### Создание окна WinForm

</div>

```cs
...
public static class Program{
	public static int Main(string[] Args){
		WL.WoowzLib.Start();
		
		Window W = new Window(); // Прежде чем создавать, прочитайте что требуется
		
		while(!W.ShouldDestroy){
			WL.Windows.Form.Tick();
		}
	}
}
```

<div align="center">

### Создание окна GLFW

</div>

```cs
...
public static class Program{
	public static int Main(string[] Args){
		WL.WoowzLib.Start();
		
		WL.GLFW.Start();
		
		Window<GL> W = new Window<GL>();
		
		while(!W.ShouldDestroy){
			W.Render.BackgroundColor = ColorF.Red;
			
			W.Render.Clear();
			
			W.FinishRender();
			
			WL.GLFW.Tick();
		}
		
		WL.GLFW.Stop();
	}
}
```

<div align="center">

## Установка

</div>

пока-что не придумал...

<div align="center">

## Предупреждение

</div>

Библиотека находится в разработке