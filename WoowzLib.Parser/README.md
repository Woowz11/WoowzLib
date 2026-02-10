<div align="center">

![Иконка](Icon.png)

![Typing SVG](https://readme-typing-svg.demolab.com?font=Science+Gothic+&weight=900&size=70&duration=1&pause=100000&color=E6E6E6&center=true&vCenter=true&width=700&height=70&lines=WoowzLib.Parser)

[![License](https://img.shields.io/badge/License-CC_BY_SA_4.0-blue)](https://creativecommons.org/licenses/by-sa/4.0/)

## Информация

</div>

``WoowzLib.Parser`` — Получение информации из разних типов файлов (пока-что только BMP!)

### Детали
* Нужен [WoowzLib](https://github.com/Woowz11/WoowzLib/tree/main/WoowzLib.Core)

### Ссылки
[NuGet](https://www.nuget.org/packages/Woowz11.WoowzLib.Parser) • [GitHub](https://github.com/Woowz11/WoowzLib/tree/main/WoowzLib.Parser)

<div align="center">

## Под-Модули

|Название |Информация |
|:--------|:----------|
|Parser   | Функции для парсинга |

## Больше информации

### Parser
</div>
<details><summary>Смотреть...</summary>

Пример получения информации из ``BMP`` файла:

```csharp
File F = new File(... "/Image.bmp"); // Получаем сам файл
byte[] F_Content = F.ReadByte(); // Содержимое файла
ParsedContainer_BMP Content = WL.Parser.ParseBMP(F_Content); // Парсинг содержимого в BMP
Image I = Content.ToImage(); // Конвертация в изображения для примера

ParsedContainer_Image Content2 = (ParsedContainer_Image)WL.Parser.Parse(F_Content); // Если не известно что это BMP
Image I2 = Content2.ToImage();
```

Остальные файлы пока-что в разработке!!!

|Файл|Инфо|
|:---|:--:|
|BMP |1, 8, 24, 32 бит будет конвертировать в 32 автоматически (RGBA)|

</details>