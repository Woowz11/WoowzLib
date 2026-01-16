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

GIT_ROOT=$(git rev-parse --show-toplevel)
cd "$GIT_ROOT" || exit 1

# Массив для хранения результатов
WOOWZLIB_DIRS=()

for dir in */; do
    if [ -d "$dir" ] && [[ "$(basename "$dir")" == WoowzLib.* ]]; then
        # Проверяем изменения через git по относительному пути
        REL_PATH="$dir"
        if git diff --quiet HEAD -- "$REL_PATH"; then
            echo "Изменений в $dir нет, пропускаем..."
            continue
        else
            echo "Обнаружены изменения в $dir — добавляем в массив..."
            WOOWZLIB_DIRS+=("${dir%/}")
        fi
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

# --------------------------------------------------------------------

for file in "${WL_MODULE_FILES[@]}"; do
    echo "Обрабатываю $file..."

    # Сохраняем любые пробелы в начале, первый аргумент — любая последовательность до запятой, второе — число
    sed -E -i.bak 's/^([[:space:]]*)\[WLModule\(([^,]+),[[:space:]]*(-?[0-9]+)\)\]/echo "\1[WLModule(\2, $((\3+1)))]"/ge' "$file"

    # Удаляем резервную копию
    rm -f "$file.bak"
done