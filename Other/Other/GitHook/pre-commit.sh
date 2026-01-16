#!/bin/bash

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

IS_GITHOOK=false
if [ "$(basename "$SCRIPT_DIR")" == "GitHook" ]; then
    IS_GITHOOK=true
fi

echo "Исполняется в GitHook? $IS_GITHOOK"

# --------------------------------------------------------------------

if [ "$IS_GITHOOK" = true ]; then
    TARGET_DIR="$SCRIPT_DIR/../../../"
else
    TARGET_DIR="$SCRIPT_DIR/../../"
fi

# Перейти в целевую папку
cd "$TARGET_DIR" || { echo "Не удалось перейти в $TARGET_DIR"; exit 1; }

echo "Текущая рабочая директория: $(pwd)"

# --------------------------------------------------------------------

# Массив для хранения результатов
WOOWZLIB_DIRS=()

# Перебор всех элементов в текущей директории
for dir in */; do
    # Проверяем, что это папка и её имя начинается с "WoowzLib."
    if [ -d "$dir" ] && [[ "$(basename "$dir")" == WoowzLib.* ]]; then
        WOOWZLIB_DIRS+=("${dir%/}")  # убираем завершающий /
    fi
done

# Вывод массива
echo "Найденные папки WoowzLib.*:"
for d in "${WOOWZLIB_DIRS[@]}"; do
    echo " - $d"
done

# --------------------------------------------------------------------

# Массив для хранения файлов с [WLModule(
WL_MODULE_FILES=()

# Перебираем найденные папки WoowzLib.*
for lib_dir in "${WOOWZLIB_DIRS[@]}"; do
    # Переходим в папку
    cd "$TARGET_DIR/$lib_dir" || continue

    # Перебираем все файлы на первом уровне
    for file in *; do
        if [ -f "$file" ]; then
            # Проверяем, есть ли строка [WLModule(
            if grep -q "\[WLModule(" "$file"; then
                WL_MODULE_FILES+=("$TARGET_DIR/$lib_dir/$file")
            fi
        fi
    done
done

# Вывод результатов
echo "Файлы с [WLModule( :"
for f in "${WL_MODULE_FILES[@]}"; do
    echo " - $f"
done

read -p "Нажмите Enter для продолжения..."
