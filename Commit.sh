#!/bin/bash

ORIGINAL_DIR="$(pwd)"

# Сначала вызываем pre-commit.sh
sh "Other/Other/GitHook/pre-commit.sh"

# Константное сообщение коммита
COMMIT_MSG="Auto commit by script"

cd "$ORIGINAL_DIR" || exit 1

# Добавляем все изменения
git add -A

# Делаем коммит
git commit -m "$COMMIT_MSG"

echo "Коммит выполнен: $COMMIT_MSG"
